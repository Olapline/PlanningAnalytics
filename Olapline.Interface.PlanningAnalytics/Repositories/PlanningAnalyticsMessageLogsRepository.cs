using Olapline.Interface.PlanningAnalytics.Model;
using Olapline.Interface.PlanningAnalytics.Services;
using System.Collections;
using System.Collections.Generic;

namespace Olapline.Interface.PlanningAnalytics.Repositories
{
    public class PlanningAnalyticsMessageLogsRepository : IEnumerable<PlanningAnalyticsMessageLogEntry>
    {
        private readonly IPlanningAnalyticsRestService _planningAnalyticsRestService;
        private List<PlanningAnalyticsMessageLogEntry> Entries;
        public PlanningAnalyticsMessageLogsRepository(IPlanningAnalyticsRestService planningAnalyticsRestService)
        {
            _planningAnalyticsRestService = planningAnalyticsRestService;
        }




        public PlanningAnalyticsMessageLogEntry this[string Index]
        {
            get
            {
                var result = _planningAnalyticsRestService.Get<PlanningAnalyticsMessageLogEntry>("MessageLogEntries('" + Index + "')");
                //result.SetInterface(_planningAnalyticsRestService);
                return result;
            }
        }

        public IEnumerator<PlanningAnalyticsMessageLogEntry> GetEnumerator()
        {
            Entries = _planningAnalyticsRestService.Get<PlanningAnalyticsList<PlanningAnalyticsMessageLogEntry>>("MessageLogEntries").value;
            return Entries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
