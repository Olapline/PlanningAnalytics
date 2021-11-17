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
    public class PlanningAnalyticsChoresRepository : IEnumerable<PlanningAnalyticsChore>
    {
        private readonly IPlanningAnalyticsRestService _planningAnalyticsRestService;
        private List<PlanningAnalyticsChore> Chores;

        public PlanningAnalyticsChoresRepository(IPlanningAnalyticsRestService planningAnalyticsRestService)
        {
            _planningAnalyticsRestService = planningAnalyticsRestService;
        }

        public int Count()
        {
            return 0;
        }


        public PlanningAnalyticsChore this[string Index]
        {
            get
            {
                var result = _planningAnalyticsRestService.Get<PlanningAnalyticsChore>("Chores('" + Index + "')");

                return result;
            }

            set
            {
                var result = _planningAnalyticsRestService.Put<PlanningAnalyticsProcess>("Chores('" + Index + "')", value);


            }
        }

        public PlanningAnalyticsChore Add(PlanningAnalyticsChore Chore)
        {
            var result = _planningAnalyticsRestService.Post<PlanningAnalyticsChore>("Processes", Chore);
            return result;
        }

        public PlanningAnalyticsChore Delete(PlanningAnalyticsChore Chore)
        {
            var result = _planningAnalyticsRestService.Delete<dynamic>("Chores('" + Chore.Name + "')");
            return result;
        }

        public IEnumerator<PlanningAnalyticsChore> GetEnumerator()

        {
            this.Chores = _planningAnalyticsRestService.Get<PlanningAnalyticsList<PlanningAnalyticsChore>>("Chores").value;
            return Chores.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
