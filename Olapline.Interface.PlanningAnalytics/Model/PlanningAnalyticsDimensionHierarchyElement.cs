using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olapline.Interface.PlanningAnalytics.Model
{
    public class PlanningAnalyticsDimensionHierarchyElement:PlanningAnalyticsObject
    {
        public string UniqueName { get; set; }
        public string Type { get; set; }

        public int Level { get; set; }
        public int Index { get; set; }

        public dynamic Attributes { get; set; }

        public List<PlanningAnalyticsDimensionHierarchyElement> Parents { get; set; }
    }
}
