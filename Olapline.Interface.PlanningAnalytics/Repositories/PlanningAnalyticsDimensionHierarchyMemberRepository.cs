using Olapline.Interface.PlanningAnalytics.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olapline.Interface.PlanningAnalytics.Model;

namespace Olapline.Interface.PlanningAnalytics.Repositories
{
    public class PlanningAnalyticsDimensionHierarchyMemberRepository
    {
        private IPlanningAnalyticsRestService _planningAnalyticsRestService;
        private string _Dimension;
        private string _Hierarchy;

        public PlanningAnalyticsDimensionHierarchyMemberRepository(IPlanningAnalyticsRestService planningAnalyticsRestService, string Dimension, string Hierarchy)
        {
            _planningAnalyticsRestService = planningAnalyticsRestService;
            _Dimension = Dimension;
            _Hierarchy = Hierarchy;
        }

        public List<PlanningAnalyticsDimensionHierarchyMember> GetMembers(int Top=0,int skip = 0)
        {
            return this._planningAnalyticsRestService.Get<PlanningAnalyticsList<PlanningAnalyticsDimensionHierarchyMember>>("Dimensions('"+ _Dimension + "')/Hierarchies('" + _Hierarchy + "')/Members?$top=" + Top + "&$skip=" + skip).value;
        }


    }
}
