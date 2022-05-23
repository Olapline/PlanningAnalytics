using Olapline.Interface.PlanningAnalytics.Model;
using Olapline.Interface.PlanningAnalytics.Services;
using System.Collections;
using System.Collections.Generic;

namespace Olapline.Interface.PlanningAnalytics.Repositories
{
    public class PlanningAnalyticsCubesRepository : IEnumerable<PlanningAnalyticsCube>
    {
        private readonly IPlanningAnalyticsRestService _planningAnalyticsRestService;
        private List<PlanningAnalyticsCube> Cubes;
        public PlanningAnalyticsCubesRepository(IPlanningAnalyticsRestService planningAnalyticsRestService)
        {
            _planningAnalyticsRestService = planningAnalyticsRestService;
        }




        public PlanningAnalyticsCube this[string Index]
        {
            get
            {
                var result = _planningAnalyticsRestService.Get<PlanningAnalyticsCube>("Cubes('" + Index + "')?$expand=Dimensions");
                result.SetInterface(_planningAnalyticsRestService);
                return result;
            }
        }

        public IEnumerator<PlanningAnalyticsCube> GetEnumerator()
        {
            Cubes = _planningAnalyticsRestService.Get<PlanningAnalyticsCubeList>("Cubes?$expand=Dimensions").value;
            Cubes.ForEach(x => x.SetInterface(_planningAnalyticsRestService));
            return Cubes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
