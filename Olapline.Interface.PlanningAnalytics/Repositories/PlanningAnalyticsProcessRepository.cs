using Olapline.Interface.PlanningAnalytics.Model;
using Olapline.Interface.PlanningAnalytics.Services;
using System.Collections;
using System.Collections.Generic;

namespace Olapline.Interface.PlanningAnalytics.Repositories
{
    public class PlanningAnalyticsProcessRepository : IEnumerable<PlanningAnalyticsProcess>
    {
        private readonly IPlanningAnalyticsRestService _planningAnalyticsRestService;
        private List<PlanningAnalyticsProcess> Processes;
        public PlanningAnalyticsProcessRepository(IPlanningAnalyticsRestService planningAnalyticsRestService)
        {
            _planningAnalyticsRestService = planningAnalyticsRestService;
        }

        public int Count()
        {
            return 0;
        }


        public PlanningAnalyticsProcess this[string Index]
        {
            get
            {
                var result = _planningAnalyticsRestService.Get<PlanningAnalyticsProcess>("Processes('" + Index + "')");
                //result.VariablesUIData = _planningAnalyticsRestService.Get<List<string>>("Processes('" + Index + "')/VariablesUIData");
                result.SetInterface(_planningAnalyticsRestService);
                return result;
            }

            set
            {
                var result = _planningAnalyticsRestService.Put<PlanningAnalyticsProcess>("Processes('" + Index + "')", value);

                
            }
        }

        public PlanningAnalyticsProcess Add(PlanningAnalyticsProcess Process)
        {
            var result = _planningAnalyticsRestService.Post<PlanningAnalyticsProcess>("Processes", Process);
            return result;
        }

        public PlanningAnalyticsProcess Delete(PlanningAnalyticsProcess Process)
        {
            var result = _planningAnalyticsRestService.Delete<dynamic>("Processes('"+Process.Name+"')");
            return result;
        }

        public IEnumerator<PlanningAnalyticsProcess> GetEnumerator()

        {
            this.Processes = _planningAnalyticsRestService.Get<PlanningAnalyticsProcessList>("Processes?$select=Name").value;
            return Processes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
