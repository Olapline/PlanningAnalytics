using Olapline.Interface.PlanningAnalytics.Model;
using Olapline.Interface.PlanningAnalytics.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olapline.Interface.PlanningAnalytics.Repositories
{
    public class PlanningAnalyticsThreadsRepository : IEnumerable<PlanningAnalyticsThread>
    {
        private readonly IPlanningAnalyticsRestService _planningAnalyticsRestService;
        private List<PlanningAnalyticsThread> Threads;
        public PlanningAnalyticsThreadsRepository(IPlanningAnalyticsRestService planningAnalyticsRestService)
        {
            _planningAnalyticsRestService = planningAnalyticsRestService;
        }




        public PlanningAnalyticsThread this[string Index]
        {
            get
            {
                var result = _planningAnalyticsRestService.Get<PlanningAnalyticsThread>("Threads('" + Index + "')");
                result.SetInterface(_planningAnalyticsRestService);
                return result;
            }
        }

        public IEnumerator<PlanningAnalyticsThread> GetEnumerator()
        {
            this.Threads = _planningAnalyticsRestService.Get<PlanningAnalyticsList<PlanningAnalyticsThread>>("Threads").value;
            return this.Threads.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
