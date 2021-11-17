using Olapline.Interface.PlanningAnalytics.Repositories;
using Olapline.Interface.PlanningAnalytics.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olapline.Interface.PlanningAnalytics.Model;

namespace Olapline.Interface.PlanningAnalytics.Model
{
    public class PlanningAnalyticsDimensionHierarchyList
    {
        public List<PlanningAnalyticsDimensionHierarchy> value { get; set; }
    }


    public class PlanningAnalyticsDimensionHierarchy : PlanningAnalyticsObject
    {
        private IPlanningAnalyticsRestService _planningAnalyticsRestService;
        public string UniqueName { get; set; }
        public int Cardinality { get; set; }
        public int Structure { get; set; }
        public bool Visible { get; set; }
        public PlanningAnalyticsObject DefaultMember { get; set; }

        public List<PlanningAnalyticsObject> Subsets { get; set; }
        public List<PlanningAnalyticsObject> PrivateSubsets { get; set; }

        public List<PlanningAnalyticsObject> SessionSubsets { get; set; }
        public List<PlanningAnalyticsObject> Levels { get; set; }

        public List<PlanningAnalyticsElementAttribute> ElementAttributes { get; set; }

        public void SetInterface(IPlanningAnalyticsRestService planningAnalyticsRestService, string DimensionName)
        {
            _planningAnalyticsRestService = planningAnalyticsRestService;
            this.Subsets= this._planningAnalyticsRestService.Get<PlanningAnalyticsList<PlanningAnalyticsObject>>("Dimensions('" + DimensionName + "')/Hierarchies('" + this.Name + "')/Subsets").value;
            this.Members = new PlanningAnalyticsDimensionHierarchyMemberRepository( planningAnalyticsRestService, DimensionName, this.Name);
            this.Elements = new PlanningAnalyticsDimensionHierarchyElementRepository(planningAnalyticsRestService, DimensionName, this.Name);
        }

        public PlanningAnalyticsDimensionHierarchyMemberRepository Members;

        public PlanningAnalyticsDimensionHierarchyElementRepository Elements;

    }
}
