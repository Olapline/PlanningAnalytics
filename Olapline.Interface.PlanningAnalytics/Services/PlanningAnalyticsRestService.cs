using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Reflection;
using System.Net.Http.Headers;
using System.IO;
using System.Threading;

namespace Olapline.Interface.PlanningAnalytics.Services
{
    public interface IPlanningAnalyticsRestService
    {
        
        bool Authenticate(string UserName, string Password);

        string AuthMode();
        T Get<T>(string Url, bool Delta=false);
        dynamic Post(string Url, dynamic Body, bool IncludeMetaData = true, bool RetrieveResult=true);
        T Post<T>(string Url, dynamic Body, bool IncludeMetaData=true);
        T Put<T>(string Url, dynamic Body);
        T Delete<T>(string Url);

        T Patch<T>(string Url, dynamic Body);
        Task<T> GetAsync<T>(string Url);
    }
    public class PlanningAnalyticsRestService : IPlanningAnalyticsRestService
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _serviceBaseUrl;
        private AuthenticationHeaderValue _authenticationHeader;
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private string Tm1SessionId;
        private string _authenticationMode;

        public PlanningAnalyticsRestService(string Url)
        {
            _httpClient = new HttpClient();
            _serviceBaseUrl = new Uri(Url);
            _httpClient.Timeout = Timeout.InfiniteTimeSpan;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            System.Net.ServicePointManager.ServerCertificateValidationCallback += delegate (object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors sslError)
            {
                bool validationResult = true;
                return validationResult;
            };
            _authenticationMode = DetectAuthenticationMode();
        }

        private string DetectAuthenticationMode()
        {
            var response = _httpClient.GetAsync(_serviceBaseUrl + "Cubes").Result;

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var result = response.Headers.Where(x => x.Key == "WWW-Authenticate").FirstOrDefault();
                string AuthMode = result.Value.ElementAt(0);
                // Read the Cognos SSO URL and try to get CAM Passport
                var Header = response.Headers.Where(h => h.Key == "Set-Cookie").FirstOrDefault().Value.ElementAt(0);

                Tm1SessionId = Header.Split(';')[0].Split('=')[1];
                return AuthMode;
            }
            return "NONE";
        }


        public bool Authenticate(string UserName = null, string Password = null)
        {
            try
            {
                if (_authenticationMode.Contains("CAMPassport"))
                {
                    string gwUrl = _authenticationMode.Replace("CAMPassport ", "");

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(gwUrl);
                    CredentialCache cc = new CredentialCache();
                    string camPassport = "";
                    //else use the "current" credential of the authenticated user (windows authentication)
                    if (UserName != null)
                    {
                        cc.Add(new Uri(gwUrl), "Negotiate", new NetworkCredential(UserName, Password));
                    }
                    else
                    {
                        cc.Add(new Uri(gwUrl), "Negotiate", CredentialCache.DefaultNetworkCredentials);
                    }

                    request.Credentials = cc;
                    request.CookieContainer = new CookieContainer();

                    HttpWebResponse responseauth = (HttpWebResponse)request.GetResponse();
                    foreach (Cookie c in responseauth.Cookies)
                    {
                        if (c.Name.Equals("cam_passport"))
                        {
                            camPassport = c.Value;
                        }
                    }
                    // Read the Cognos SSO URL and try to get CAM Passport
                    var message = new HttpRequestMessage(HttpMethod.Get, _serviceBaseUrl + "Cubes");
                    message.Headers.Authorization = new AuthenticationHeaderValue("CAMPassport", camPassport);

                    var responsesession = _httpClient.SendAsync(message).Result;
                    if (responsesession.IsSuccessStatusCode)
                    {
                        this._authenticationHeader = message.Headers.Authorization;

                        return true;
                    }

                    return false;



                }
                if (_authenticationMode.Contains("Negotiate"))
                {
                    // Windows Integrated

                }

                if (_authenticationMode.Contains("Basic"))
                {
                    var byteArray = Encoding.ASCII.GetBytes(UserName + ":" + Password);
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    try
                    {
                        var authresponse = _httpClient.GetAsync(_serviceBaseUrl + "Cubes").Result;
                        if (authresponse.IsSuccessStatusCode)
                        {
                            if (Tm1SessionId == null)
                            {
                                var Header = authresponse.Headers.Where(h => h.Key == "Set-Cookie").FirstOrDefault().Value.ElementAt(0);

                                Tm1SessionId = Header.Split(';')[0].Split('=')[1];
                            }
                            
                        }

                    }
                    catch (Exception e)
                    {
                        log.Error(e.Message);
                        return false;
                    }




                    return true;

                }

                

            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return false;
            }
            return true;
        }



        public T Get<T>(string Url, bool Delta = false)
        {
            // Replace #
            Url = Url.Replace("#", "%23");


            var message = new HttpRequestMessage(HttpMethod.Get, _serviceBaseUrl + Url);
            message.Headers.Add("Cookie", "TM1SessionId=" + this.Tm1SessionId);
            
            if (this._authenticationHeader != null)
            {
                message.Headers.Authorization = this._authenticationHeader;
            }

            if (Delta)
            {
                message.Headers.Add("Prefer", "odata.track-changes");
            }
            
            var response = _httpClient.SendAsync(message).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<T>(responseString);
                return result;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Try ReAuthenticate
                if (this.Authenticate())
                {
                    return this.Get<T>(Url);
                }
                else
                {
                    string Message = "Authentication Error with Server";
                    log.Error(Message);
                    throw (new Exception(Message));
                }

            }
            else
            {
                string Message = response.Content.ReadAsStringAsync().Result;
                log.Error(Message);
                throw (new Exception(Message));
            }


        }

        public dynamic Post(string Url, dynamic Body, bool IncludeMetadata = true, bool RetrieveResult = true)
        {
            // Replace #
            Url = Url.Replace("#", "%23");


            // Create a new 'HttpWebRequest' object to the mentioned Uri.   
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(_serviceBaseUrl + Url);
            // Set AllowWriteStreamBuffering to 'false'.
            myHttpWebRequest.AllowWriteStreamBuffering = false;
            myHttpWebRequest.Headers.Add("Cookie", "TM1SessionId=" + this.Tm1SessionId);
            if (this._authenticationHeader != null)
            {
                myHttpWebRequest.Headers.Add("Authentication", this._authenticationHeader.Parameter);
            }
            // Set 'Method' property of 'HttpWebRequest' class to POST.
            myHttpWebRequest.Method = "POST";
            string Content = JsonConvert.SerializeObject(Body);
            byte[] byteArray = Encoding.UTF8.GetBytes(Content);
            // Set 'ContentType' property of the 'HttpWebRequest' class to "application/x-www-form-urlencoded".
            myHttpWebRequest.ContentType = "application/json; charset=UTF-8";
            myHttpWebRequest.Accept = "application/json";
            // If the AllowWriteStreamBuffering property of HttpWebRequest is set to false,the contentlength has to be set to length of data to be posted else Exception(411) is raised.
            myHttpWebRequest.ContentLength = byteArray.Length;
            myHttpWebRequest.Timeout = Timeout.Infinite;
            Stream newStream = myHttpWebRequest.GetRequestStream();
            newStream.Write(byteArray, 0, byteArray.Length);
            newStream.Close();
            
            // Assign the response object of 'HttpWebRequest' to a 'HttpWebResponse' variable.
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

            return null;
        }


        public T Post<T>(string Url, dynamic Body, bool IncludeMetadata=true)
        {
            // Replace #
            Url = Url.Replace("#", "%23");


            var message = new HttpRequestMessage(HttpMethod.Post, _serviceBaseUrl + Url);
            
            message.Content = new StringContent(JsonConvert.SerializeObject(Body), Encoding.UTF8, "application/json");
            message.Headers.Add("Cookie", "TM1SessionId=" + this.Tm1SessionId);
            if (!IncludeMetadata)
            {
                message.Headers.Add("Accept", "application/json;odata.metadata=none");
            }
            
            //message.Headers.Add("Content-Type", "application/json");
            if (this._authenticationHeader != null)
            {
                message.Headers.Authorization = this._authenticationHeader;
            }

            var response = _httpClient.SendAsync(message).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<T>(responseString);
                return result;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Try ReAuthenticate
                if (this.Authenticate())
                {
                    return this.Post<T>(Url, Body);
                }
                else
                {
                    string Message = "Authentication Error with Server";
                    log.Error(Message);
                    throw (new Exception(Message));
                }

            }
            else
            {
                string Message = response.Content.ReadAsStringAsync().Result;
                throw (new Exception(Message));
            }


        }


        public T Put<T>(string Url, dynamic Body)
        {
            // Replace #
            Url = Url.Replace("#", "%23");


            var message = new HttpRequestMessage(HttpMethod.Put, _serviceBaseUrl + Url);
            message.Content = new StringContent(JsonConvert.SerializeObject(Body), Encoding.UTF8, "application/json");
            message.Headers.Add("Cookie", "TM1SessionId=" + this.Tm1SessionId);
            if (this._authenticationHeader != null)
            {
                message.Headers.Authorization = this._authenticationHeader;
            }

            var response = _httpClient.SendAsync(message).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<T>(responseString);
                return result;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Try ReAuthenticate
                if (this.Authenticate())
                {
                    return this.Put<T>(Url, Body);
                }
                else
                {
                    string Message = "Authentication Error with Server";
                    log.Error(Message);
                    throw (new Exception(Message));
                }

            }
            else
            {
                string Message = response.Content.ReadAsStringAsync().Result;
                throw (new Exception(Message));
            }


        }

        public T Delete<T>(string Url)
        {
            // Replace #
            Url = Url.Replace("#", "%23");


            var message = new HttpRequestMessage(HttpMethod.Delete, _serviceBaseUrl + Url);

            message.Headers.Add("Cookie", "TM1SessionId=" + this.Tm1SessionId);
            if (this._authenticationHeader != null)
            {
                message.Headers.Authorization = this._authenticationHeader;
            }

            var response = _httpClient.SendAsync(message).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<T>(responseString);
                return result;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Try ReAuthenticate
                if (this.Authenticate())
                {
                    return this.Delete<T>(Url);
                }
                else
                {
                    string Message = "Authentication Error with Server";
                    log.Error(Message);
                    throw (new Exception(Message));
                }

            }
            else
            {
                string Message = response.Content.ReadAsStringAsync().Result;
                throw (new Exception(Message));
            }


        }

        public async Task<T> GetAsync<T>(string Url)
        {
            // Replace #
            Url = Url.Replace("#", "%23");


            var message = new HttpRequestMessage(HttpMethod.Get, _serviceBaseUrl + Url);
            message.Headers.Add("Cookie", "TM1SessionId=" + this.Tm1SessionId);
            if (this._authenticationHeader != null)
            {
                message.Headers.Authorization = this._authenticationHeader;
            }
            var response = _httpClient.SendAsync(message).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<T>(responseString);
                return result;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Try ReAuthenticate
                if (this.Authenticate())
                {
                    return await this.GetAsync<T>(Url);
                }
                else
                {
                    string Message = "Authentication Error with Server";
                    log.Error(Message);
                    throw (new Exception(Message));
                }

            }
            else
            {
                string Message = response.Content.ReadAsStringAsync().Result;
                throw (new Exception(Message));
            }
        }

        public T Patch<T>(string Url, dynamic Body)
        {
            Url = Url.Replace("#", "%23");


            var message = new HttpRequestMessage(new HttpMethod("PATCH"), _serviceBaseUrl + Url);
            message.Content = new StringContent(JsonConvert.SerializeObject(Body), Encoding.UTF8, "application/json");
            message.Headers.Add("Cookie", "TM1SessionId=" + this.Tm1SessionId);

            if (this._authenticationHeader != null)
            {
                message.Headers.Authorization = this._authenticationHeader;
            }

            var response = _httpClient.SendAsync(message).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<T>(responseString);
                return result;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Try ReAuthenticate
                if (this.Authenticate())
                {
                    return this.Get<T>(Url);
                }
                else
                {
                    string Message = "Authentication Error with Server";
                    log.Error(Message);
                    throw (new Exception(Message));
                }

            }
            else
            {
                string Message = response.Content.ReadAsStringAsync().Result;
                log.Error(Message);
                throw (new Exception(Message));
            }


        }

        public string AuthMode()
        {
            return _authenticationMode;
        }
    }
}
