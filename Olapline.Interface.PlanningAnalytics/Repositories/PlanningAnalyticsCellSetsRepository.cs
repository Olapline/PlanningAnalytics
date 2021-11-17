using Olapline.Interface.PlanningAnalytics.Model;
using Olapline.Interface.PlanningAnalytics.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olapline.Interface.PlanningAnalytics.Repositories
{
    public class PlanningAnalyticsCellSetsRepository
    {
        private readonly IPlanningAnalyticsRestService _planningAnalyticsRestService;
        
        public PlanningAnalyticsCellSetsRepository(IPlanningAnalyticsRestService planningAnalyticsRestService)
        {
            _planningAnalyticsRestService = planningAnalyticsRestService;
        }


        public PlanningAnalyticsCellSet createMDXCellSet(string MDX)
        {
            dynamic PostContent = new ExpandoObject();
            PostContent.MDX = MDX;
            return _planningAnalyticsRestService.Post<PlanningAnalyticsCellSet>("ExecuteMDX", PostContent);
        }

        public List<PlanningAnalyticsCell> Cells(string Index, int top = 1000, int skip = 0)
        {
            return _planningAnalyticsRestService.Get<PlanningAnalyticsList<PlanningAnalyticsCell>>("Cellsets('" + Index + "')/Cells?$expand=Members($select=UniqueName, Name)&$count=true&$top=" + top + "&$skip=" + skip).value;
        }

        public List<PlanningAnalyticsCellSetAxis> Axes(string Index, int top = 1000, int skip = 0)
        {
            return _planningAnalyticsRestService.Get<PlanningAnalyticsList<PlanningAnalyticsCellSetAxis>>("Cellsets('" + Index + "')/Axes?$expand=Tuples($expand=Members($select=Name,UniqueName);$top=" + top + ";$skip=" + skip + ")").value;
        }

        public PlanningAnalyticsCube Cube(string Index)
        {
            return _planningAnalyticsRestService.Get<PlanningAnalyticsCube>("Cellsets('" + Index + "')/Cube?$expand=Dimensions($select=Name,UniqueName)&$select=Name");
        }

        public void Delete(string Index)
        {
            _planningAnalyticsRestService.Delete<dynamic>("Cellsets('" + Index + "')");
        }

        public PlanningAnalyticsCellSet this[string Index]
        {
            get
            {
                var result = _planningAnalyticsRestService.Get<PlanningAnalyticsCellSet>("Cellsets('" + Index + "')");
                //result.SetInterface(_planningAnalyticsRestService);
                return result;
            }
        }

    }

}
