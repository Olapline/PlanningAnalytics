using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olapline.Interface.PlanningAnalytics.Model
{


    public class PlanningAnalyticsChore
    {
        [JsonProperty("@odata.etag")]
        public string OdataEtag { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public bool DSTSensitive { get; set; }
        public bool Active { get; set; }
        public string ExecutionMode { get; set; }
        public string Frequency { get; set; }
        //public Attributes Attributes { get; set; }
        public List<Task> Tasks { get; set; }
    }

    
    public class Parameter
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }



    public class Task
    {
        public int Step { get; set; }
        public List<Parameter> Parameters { get; set; }
        public PlanningAnalyticsProcess Process { get; set; }

        [JsonProperty("Process@odata.bind")]
        public string ProcessBind { get; set; }
    }
}
