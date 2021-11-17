using Olapline.Interface.PlanningAnalytics.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olapline.Interface.PlanningAnalytics.Model;

namespace Olapline.Interface.PlanningAnalytics.Repositories
{
    public class PlanningAnalyticsDimensionHierarchyElementRepository
    {
        private IPlanningAnalyticsRestService _planningAnalyticsRestService;
        private string _Dimension;
        private string _Hierarchy;

        public PlanningAnalyticsDimensionHierarchyElementRepository(IPlanningAnalyticsRestService planningAnalyticsRestService, string Dimension, string Hierarchy)
        {
            _planningAnalyticsRestService = planningAnalyticsRestService;
            _Dimension = Dimension;
            _Hierarchy = Hierarchy;
        }

        public List<PlanningAnalyticsDimensionHierarchyElement> GetElements(int Top=0,int skip = 0)
        {
            return this._planningAnalyticsRestService.Get<PlanningAnalyticsList<PlanningAnalyticsDimensionHierarchyElement>>("Dimensions('"+ _Dimension + "')/Hierarchies('" + _Hierarchy + "')/Elements?$top=" + Top + "&$skip=" + skip + "&$expand=Parents").value;
        }


    }
}
