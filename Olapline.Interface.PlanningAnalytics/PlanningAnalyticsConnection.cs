using Olapline.Interface.PlanningAnalytics.Model;
using Olapline.Interface.PlanningAnalytics.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olapline.Interface.PlanningAnalytics.Repositories;
using System.Reflection;

namespace Olapline.Interface.PlanningAnalytics
{
    public class PlanningAnalyticsConnection : IDisposable
    {
        private readonly string _host;
        private readonly IPlanningAnalyticsRestService _planningAnalyticsRestService;
        public PlanningAnalyticsProcessRepository Processes;
        public PlanningAnalyticsDimensionsRepository Dimensions;
        public PlanningAnalyticsCubesRepository Cubes;
        public PlanningAnalyticsThreadsRepository Threads;
        public PlanningAnalyticsMessageLogsRepository Logs;
        public PlanningAnalyticsCellSetsRepository CellSets;
        public PlanningAnalyticsChoresRepository Chores;
        public PlanningAnalyticsApplicationsRepository Applications;
        public PlanningAnalyticsGitPlanRepository GitPlans;
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public string InstanceName { get; set; }
        public string AuthMode { get; set; }


        public PlanningAnalyticsConnection(string serviceUrl)
        {
            this._host = serviceUrl;
            log.Info("Creating a Plannning Analytics Connection to:" + serviceUrl);
            _planningAnalyticsRestService = new PlanningAnalyticsRestService(serviceUrl);
            this.AuthMode = _planningAnalyticsRestService.AuthMode();
        }

        public bool Authenticate(string UserName, string Password)
        {

            bool authenticated = _planningAnalyticsRestService.Authenticate(UserName, Password);
            if (authenticated)
            {
                var Config = this.serverConfiguration;
                this.InstanceName = Config.ServerName;

                Processes = new PlanningAnalyticsProcessRepository(_planningAnalyticsRestService);
                Dimensions = new PlanningAnalyticsDimensionsRepository(_planningAnalyticsRestService);
                Cubes = new PlanningAnalyticsCubesRepository(_planningAnalyticsRestService);
                Threads = new PlanningAnalyticsThreadsRepository(_planningAnalyticsRestService);
                Logs = new PlanningAnalyticsMessageLogsRepository(_planningAnalyticsRestService);
                CellSets = new PlanningAnalyticsCellSetsRepository(_planningAnalyticsRestService);
                Chores = new PlanningAnalyticsChoresRepository(_planningAnalyticsRestService);
                Applications = new PlanningAnalyticsApplicationsRepository(_planningAnalyticsRestService);
                GitPlans = new PlanningAnalyticsGitPlanRepository(_planningAnalyticsRestService);
            }
            return authenticated;
        }

        public bool Authenticate()
        {
            bool authenticated = _planningAnalyticsRestService.Authenticate(null, null);
            if (authenticated)
            {
                var Config = this.serverConfiguration;
                this.InstanceName = Config.ServerName;
                Processes = new PlanningAnalyticsProcessRepository(_planningAnalyticsRestService);
                Dimensions = new PlanningAnalyticsDimensionsRepository(_planningAnalyticsRestService);
                Cubes = new PlanningAnalyticsCubesRepository(_planningAnalyticsRestService);
                Threads = new PlanningAnalyticsThreadsRepository(_planningAnalyticsRestService);
                Logs = new PlanningAnalyticsMessageLogsRepository(_planningAnalyticsRestService);
                CellSets = new PlanningAnalyticsCellSetsRepository(_planningAnalyticsRestService);
                Chores = new PlanningAnalyticsChoresRepository(_planningAnalyticsRestService);
                Applications = new PlanningAnalyticsApplicationsRepository(_planningAnalyticsRestService);
                GitPlans = new PlanningAnalyticsGitPlanRepository(_planningAnalyticsRestService);
            }
            return authenticated;
        }

        public PlanningAnalyticsServerConfiguration serverConfiguration { get { return _planningAnalyticsRestService.Get<PlanningAnalyticsServerConfiguration>("Configuration"); } }

        public bool GitInitialized
        {

            get
            {
                try
                {
                    if (this.GitPlans.Count() > 0)
                    {
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


        }

        public dynamic GitExecute(PlanningAnalyticsGitPlan Plan)
        {
            return _planningAnalyticsRestService.Post<dynamic>("GitPlans('" + Plan.ID + "')/tm1.Execute", null);
        }

        public PlanningAnalyticsGit GitInit(string URL, string Username, string Password, string Deployment, bool Force)
        {
            dynamic PostObject = new ExpandoObject();
            PostObject.URL = URL;
            PostObject.Username = Username;
            PostObject.Password = Password;
            PostObject.Deployment = Deployment;
            PostObject.Force = Force;

            return _planningAnalyticsRestService.Post<PlanningAnalyticsGit>("GitInit", PostObject);
        }

        public dynamic GitDeploy(string URL, string Username, string Password, string Branch, string Deployment, bool Force)
        {
            dynamic PostObject = new ExpandoObject();
            PostObject.URL = URL;
            PostObject.Username = Username;
            PostObject.Password = Password;
            PostObject.Branch = Branch;
            PostObject.Deployment = Deployment;
            PostObject.Force = Force;

            return _planningAnalyticsRestService.Post<dynamic>("GitDeploy", PostObject);
        }

        public PlanningAnalyticsGitPullPlan GitPull(string Username, string Password, string Branch, PlanningAnalyticsGitPlanExecutionMode executionMode, bool Force)
        {
            dynamic PostObject = new ExpandoObject();

            PostObject.Username = Username;
            PostObject.Password = Password;
            PostObject.Branch = Branch;
            PostObject.ExecutionMode = executionMode;
            PostObject.Force = Force;

            return _planningAnalyticsRestService.Post<dynamic>("GitPull", PostObject);
        }

        public PlanningAnalyticsGitPushPlan GitPush(string Username, string Password, string Branch, string NewBranch, string Message, string Author, string Email, bool Force)
        {
            dynamic PostObject = new ExpandoObject();

            PostObject.Username = Username;
            PostObject.Password = Password;
            PostObject.Branch = Branch;
            PostObject.NewBranch = NewBranch;
            PostObject.Author = Author;
            PostObject.Message = Message;
            PostObject.Email = Email;

            PostObject.Force = Force;

            return _planningAnalyticsRestService.Post<PlanningAnalyticsGitPushPlan>("GitPush", PostObject);
        }

        public PlanningAnalyticsGit GitStatus(string Username, string Password)
        {
            dynamic PostObject = new ExpandoObject();
            PostObject.Username = Username;
            PostObject.Password = Password;
            return _planningAnalyticsRestService.Post<PlanningAnalyticsGit>("GitStatus", PostObject);
        }

        public dynamic GitUninit(bool Force)
        {
            dynamic PostObject = new ExpandoObject();
            PostObject.Force = Force;
            return _planningAnalyticsRestService.Post<PlanningAnalyticsGit>("GitUninit", PostObject);
        }

        public dynamic SendRequestGet(string request, bool Delta = false)
        {
            return _planningAnalyticsRestService.Get<dynamic>(request, Delta);
        }

        void IDisposable.Dispose()
        {

        }
    }
}
