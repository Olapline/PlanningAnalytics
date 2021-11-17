using Olapline.Interface.PlanningAnalytics.Services;
using System;

namespace Olapline.Interface.PlanningAnalytics.Model
{
    public class PlanningAnalyticsThread
    {
        private IPlanningAnalyticsRestService _planningAnalyticsRestService;

        public void SetInterface(IPlanningAnalyticsRestService planningAnalyticsRestService)
        {
            _planningAnalyticsRestService = planningAnalyticsRestService;
        }

        public int ThreadID { get { return (int)ID; } set { this.ID = value; } }

        public Int64 ID { get; set; }

        public ThreadType Type { get; set; }
        public int SessionID { get; set; }
        public String Name { get; set; }
        public String Context { get; set; }
        public String State { get; set; }
        public String Function { get; set; }
        public String ObjectType { get; set; }
        public String ObjectName { get; set; }
        public int RLocks { get; set; }
        public int IXLocks { get; set; }
        public int WLocks { get; set; }
        public String ElapsedTime { get; set; }
        public String WaitTime { get; set; }
        public String Info { get; set; }

        public bool Cancel()
        {
            dynamic result = _planningAnalyticsRestService.Post<dynamic>("Threads('" + this.ThreadID.ToString() + "')/tm1.CancelOperation", null);
            return true;
        }
    }
}
