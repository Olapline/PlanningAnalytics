using Olapline.Interface.PlanningAnalytics.Model;
using Olapline.Interface.PlanningAnalytics.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olapline.Interface.PlanningAnalytics.Repositories
{


    public class PlanningAnalyticsRepository<T> : IEnumerable<T>
    {
        private readonly IPlanningAnalyticsRestService _planningAnalyticsRestService;
        private List<T> Threads;
        public PlanningAnalyticsRepository(IPlanningAnalyticsRestService planningAnalyticsRestService)
        {
            _planningAnalyticsRestService = planningAnalyticsRestService;
        }




        public T this[string Index]
        {
            get
            {
                var result = _planningAnalyticsRestService.Get<T>("Threads('" + Index + "')");
                //result.SetInterface(_planningAnalyticsRestService);
                return result;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            Threads = _planningAnalyticsRestService.Get<PlanningAnalyticsList<T>>("Threads").value;
            return Threads.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
