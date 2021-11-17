using Olapline.Interface.PlanningAnalytics.Model;
using Olapline.Interface.PlanningAnalytics.Services;
using System.Collections;
using System.Collections.Generic;

namespace Olapline.Interface.PlanningAnalytics.Repositories
{
    public class PlanningAnalyticsDimensionsRepository : IEnumerable<PlanningAnalyticsDimension>
    {
        private readonly IPlanningAnalyticsRestService _planningAnalyticsRestService;
        private List<PlanningAnalyticsDimension> Dimensions;
        public PlanningAnalyticsDimensionsRepository(IPlanningAnalyticsRestService planningAnalyticsRestService)
        {
            _planningAnalyticsRestService = planningAnalyticsRestService;
        }




        public PlanningAnalyticsDimension this[string Index]
        {
            get
            {
                var Dimension = _planningAnalyticsRestService.Get<PlanningAnalyticsDimension>("Dimensions('" + Index + "')");
                Dimension.SetInterface(_planningAnalyticsRestService);
                return Dimension;
            }
        }

        public IEnumerator<PlanningAnalyticsDimension> GetEnumerator()
        {
            Dimensions = _planningAnalyticsRestService.Get<PlanningAnalyticsDimensionList>("Dimensions").value;
            Dimensions.ForEach(x => x.SetInterface(_planningAnalyticsRestService));
            return Dimensions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
