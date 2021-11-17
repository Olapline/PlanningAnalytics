using Olapline.Interface.PlanningAnalytics.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olapline.Interface.PlanningAnalytics
{
    public class PlanningAnalyticsAdminConnection
    {
        private readonly string _server;
        public List<PlanningAnalyticsConnection> Instances;
        public PlanningAnalyticsAdminConnection(string server)
        {
            _server = server;
            PlanningAnalyticsRestService planningAnalyticsRestService = new PlanningAnalyticsRestService("http://" + _server + ":5895/api/v1/");
            dynamic Instances = planningAnalyticsRestService.Get<dynamic>("Servers");
            this.Instances = new List<PlanningAnalyticsConnection>();
            foreach(var Instance in Instances.value)
            {
                string ConnUrl = (Instance.UsingSSL == true ? "https://" : "http://") + Instance.IPAddress + ":" + Instance.HTTPPortNumber + "/api/v1/";
                PlanningAnalyticsConnection Conn = new PlanningAnalyticsConnection(ConnUrl) { InstanceName = Instance.Name };
                this.Instances.Add(Conn);
            }
        }
    }
}
