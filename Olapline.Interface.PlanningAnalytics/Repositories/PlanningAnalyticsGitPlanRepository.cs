using Olapline.Interface.PlanningAnalytics.Model;
using Olapline.Interface.PlanningAnalytics.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olapline.Interface.PlanningAnalytics.Repositories
{
    public class PlanningAnalyticsGitPlanRepository: IEnumerable<PlanningAnalyticsGitPlan>
    {
        private readonly IPlanningAnalyticsRestService _planningAnalyticsRestService;
        private List<PlanningAnalyticsGitPlan> GitPlans;
        public PlanningAnalyticsGitPlanRepository(IPlanningAnalyticsRestService planningAnalyticsRestService)
        {
            this._planningAnalyticsRestService = planningAnalyticsRestService;
        }


        public PlanningAnalyticsGitPlan this[string Index]
        {
            get
            {
                var result = _planningAnalyticsRestService.Get<PlanningAnalyticsGitPlan>("GitPlans('" + Index + "')");
                //result.SetInterface(_planningAnalyticsRestService);
                return result;
            }
        }

        public PlanningAnalyticsGitPlan this[string Index, bool Push]
        {
            get
            {
                
                if (Push)
                {

                    var result = _planningAnalyticsRestService.Get<PlanningAnalyticsGitPushPlan>("GitPlans('" + Index + "')");
                    return result;
                }
                else
                {
                    var result = _planningAnalyticsRestService.Get<PlanningAnalyticsGitPullPlan>("GitPlans('" + Index + "')");
                    return result;
                }
                
                //result.SetInterface(_planningAnalyticsRestService);
                return null;
            }
        }


        public IEnumerator<PlanningAnalyticsGitPlan> GetEnumerator()
        {
            GitPlans = _planningAnalyticsRestService.Get<PlanningAnalyticsList<PlanningAnalyticsGitPlan>>("GitPlans").value;
            
            return GitPlans.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
