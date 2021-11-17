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
    public class PlanningAnalyticsApplicationsRepository : IEnumerable<PlanningAnalyticsEntry>
    {
        private readonly IPlanningAnalyticsRestService _planningAnalyticsRestService;
        private List<PlanningAnalyticsEntry> Entries;
        private string Path;
        public PlanningAnalyticsApplicationsRepository(IPlanningAnalyticsRestService planningAnalyticsRestService)
        {
            _planningAnalyticsRestService = planningAnalyticsRestService;
        }

        public PlanningAnalyticsApplicationsRepository(IPlanningAnalyticsRestService planningAnalyticsRestService, string Path)
        {
            _planningAnalyticsRestService = planningAnalyticsRestService;
            this.Path = Path;
        }


        public PlanningAnalyticsEntry this[string Index]
        {
            get
            {
                var result = _planningAnalyticsRestService.Get<PlanningAnalyticsEntry>(Path + "Contents('" + Index + "')");
                Path  += "Contents('" + Index + "')/";
                result.SetInterface(_planningAnalyticsRestService, Path);
                return result;
            }

            set
            {
                var result = _planningAnalyticsRestService.Put<PlanningAnalyticsEntry>("Contents('" + Index + "')", value);


            }
        }

        public PlanningAnalyticsEntry Add(PlanningAnalyticsEntry Chore)
        {
            var result = _planningAnalyticsRestService.Post<PlanningAnalyticsEntry>("Contents", Chore);
            return result;
        }

        public PlanningAnalyticsEntry Delete(PlanningAnalyticsEntry Chore)
        {
            var result = _planningAnalyticsRestService.Delete<dynamic>("Contents('" + Chore.Name + "')");
            return result;
        }

        public IEnumerator<PlanningAnalyticsEntry> GetEnumerator()

        {
            this.Entries = _planningAnalyticsRestService.Get<PlanningAnalyticsList<PlanningAnalyticsEntry>>(Path + "Contents").value;
            return Entries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
