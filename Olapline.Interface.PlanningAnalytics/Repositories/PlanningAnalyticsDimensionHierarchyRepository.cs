using Olapline.Interface.PlanningAnalytics.Model;
using Olapline.Interface.PlanningAnalytics.Services;
using System.Collections;
using System.Collections.Generic;


namespace Olapline.Interface.PlanningAnalytics.Repositories
{
    public class PlanningAnalyticsDimensionHierarchyRepository : IEnumerable<PlanningAnalyticsDimensionHierarchy>
    {
        private readonly IPlanningAnalyticsRestService _planningAnalyticsRestService;
        private string _DimensionName;
        private List<PlanningAnalyticsDimensionHierarchy> Hierarchies;

        public PlanningAnalyticsDimensionHierarchyRepository(IPlanningAnalyticsRestService planningAnalyticsRestService, string DimensionName)
        {
            _planningAnalyticsRestService = planningAnalyticsRestService;
            _DimensionName = DimensionName;
        }

        public PlanningAnalyticsDimensionHierarchy this[string Index]
        {
            get
            {
                var result = _planningAnalyticsRestService.Get<PlanningAnalyticsDimensionHierarchy>("Dimensions('" + _DimensionName + "')/Hierarchies('" + Index + "')");
                result.SetInterface(_planningAnalyticsRestService, _DimensionName);
                return result;
            }
        }

        public IEnumerator<PlanningAnalyticsDimensionHierarchy> GetEnumerator()
        {
            Hierarchies = _planningAnalyticsRestService.Get<PlanningAnalyticsDimensionHierarchyList>("Dimensions('" + _DimensionName + "')/Hierarchies").value;
            Hierarchies.ForEach(x => x.SetInterface(_planningAnalyticsRestService, _DimensionName));
            return Hierarchies.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
