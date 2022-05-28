using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olapline.Interface.PlanningAnalytics.Model
{
    public class PlanningAnalyticsGit
    {
        public string URL { get; set; }

        public string Deployment { get; set; }

        public PlanningAnalyticsGitCommit DeployedCommit { get; set; }

        public PlanningAnalyticsGitRemote Remote { get; set; }
    }

    public class PlanningAnalyticsGitCommit
    {
        public string ID { get; set; }

        public string Summary { get; set; }

        public string Author { get; set; }

    }

    public class PlanningAnalyticsGitPlan
    {
        public string ID { get; set; }

        public string Branch { get; set; }

        public bool Force { get; set; }

        
    }

    public class PlanningAnalyticsGitRemote
    {
        public bool Connected { get; set; }

        public List<string> Branches { get; set; }
        public List<string> Tags { get; set; }



    }

    public class PlanningAnalyticsGitPullPlan : PlanningAnalyticsGitPlan
    {
        public PlanningAnalyticsGitCommit Commit { get; set; }

        public List<string> Operations { get; set; }

        public string ExecutionMode { get; set; }


    }

    public class PlanningAnalyticsGitPushPlan : PlanningAnalyticsGitPlan
    {
        public string NewBranch { get; set; }

        public PlanningAnalyticsGitCommit NewCommit { get; set; }
        public PlanningAnalyticsGitCommit ParentCommit { get; set; }

        public List<string> SourceFiles { get; set; }

    }

    public enum PlanningAnalyticsGitPlanExecutionMode
    {
        SingleCommit=0,
        MultipleCommit=1
    }
}
