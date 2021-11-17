using Olapline.Interface.PlanningAnalytics.Repositories;
using Olapline.Interface.PlanningAnalytics.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olapline.Interface.PlanningAnalytics.Model
{
    public class PlanningAnalyticsDimensionList
    {
        public List<PlanningAnalyticsDimension> value { get; set; }
    }

    public class PlanningAnalyticsDimension: PlanningAnalyticsObject
    {
        private IPlanningAnalyticsRestService _planningAnalyticsRestService;
        public string UniqueName { get; set; }

        public string AllLeavesHierarchyName { get; set; }


        public PlanningAnalyticsDimensionHierarchyRepository Hierarchies;

        public void SetInterface(IPlanningAnalyticsRestService planningAnalyticsRestService)
        {
            _planningAnalyticsRestService = planningAnalyticsRestService;
            Hierarchies = new PlanningAnalyticsDimensionHierarchyRepository(_planningAnalyticsRestService, this.Name);
        }
    }
}
