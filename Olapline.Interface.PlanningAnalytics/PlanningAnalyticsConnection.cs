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
    public class PlanningAnalyticsConnection
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
            }
            return authenticated;
        }

        public PlanningAnalyticsServerConfiguration serverConfiguration { get { return _planningAnalyticsRestService.Get<PlanningAnalyticsServerConfiguration>("Configuration"); } }

        public dynamic SendRequestGet(string request, bool Delta=false)
        {
            return _planningAnalyticsRestService.Get<dynamic>(request, Delta);
        }

    }
}
