using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olapline.Interface.PlanningAnalytics.Model
{
    public class PlanningAnalyticsTM1Project
    {
        public double Version = 1.0;
        public List<string> Ignore { get; set; }

        public List<string> Files { get; set; }

        public PlanningAnalyticsTM1ProjectSettings Settings { get; set; }

        public Dictionary<string, PlanningAnalyticsTM1ProjectTask> Tasks { get; set; }
        public Dictionary<string, PlanningAnalyticsTM1ProjectObject> Objects { get; set; }
        public Dictionary<string, PlanningAnalyticsTM1ProjectDeployment> Deployments { get; set; }
        public List<string> PrePush { get; set; }
        public List<string> PrePull { get; set; }

        public List<string> PostPush { get; set; }
        public List<string> PostPull { get; set; }
    }

    public class PlanningAnalyticsTM1ProjectSettings
    {
        public AdministrationSettings Administration { get; set; }
        public AccessSettings Access { get; set; }
    }

    public class PlanningAnalyticsTM1ProjectObject
    {
        public string Link { get; set; }
        public List<string> Dependencies { get; set; }
    }

    public class PlanningAnalyticsTM1ProjectTask
    {
        public string Process { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
        public List<string> Dependencies { get; set; }
    }

    

    public class PlanningAnalyticsTM1ProjectDeployment
    {
        public List<string> Ignore { get; set; }

        public List<string> Files { get; set; }

        public PlanningAnalyticsTM1ProjectSettings Settings { get; set; }

        public dynamic Tasks { get; set; }
        public dynamic Objects { get; set; }
        public List<string> PrePush { get; set; }
        public List<string> PrePull { get; set; }

        public List<string> PostPush { get; set; }
        public List<string> PostPull { get; set; }
    }


}
