using Olapline.Interface.PlanningAnalytics.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olapline.Interface.PlanningAnalytics.Model
{
    public class PlanningAnalyticsCubeList
    {
        public List<PlanningAnalyticsCube> value { get; set; }
    }
    public class PlanningAnalyticsCube : PlanningAnalyticsObject
    {
        private IPlanningAnalyticsRestService _planningAnalyticsRestService;

        public void SetInterface(IPlanningAnalyticsRestService planningAnalyticsRestService)
        {
            _planningAnalyticsRestService = planningAnalyticsRestService;
        }

        public string Rules { get; set; }

        public dynamic SetRule(string Rule)
        {
            dynamic PostContent = new ExpandoObject();
            PostContent.Rules = Rule;
            _planningAnalyticsRestService.Patch<dynamic>("Cubes('" + this.Name + "')/Rules", PostContent);
            dynamic result = _planningAnalyticsRestService.Post<dynamic>("Cubes('" + this.Name + "')/tm1.CheckRules", null);
            return result;
        }

        

        public dynamic UpdateCells(dynamic Body)
        {
            return _planningAnalyticsRestService.Post("Cubes('" + this.Name + "')/tm1.UpdateCells", Body, false, false);
        }

        public string DrillthroughRules { get; set; }

        public string TimeDimension { get; set; }
        public string MeasuresDimension { get; set; }

        public DateTime LastSchemaUpdate { get; set; }

        public DateTime LastDataUpdate { get; set; }

        public List<PlanningAnalyticsDimension> Dimensions { get; set; }
        public List<PlanningAnalyticsObject> Views { get; set; }
    }
}
