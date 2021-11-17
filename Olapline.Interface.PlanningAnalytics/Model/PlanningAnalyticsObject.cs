using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olapline.Interface.PlanningAnalytics.Model
{
    public class PlanningAnalyticsObject
    {
        [JsonProperty("@odata.etag")]
        public string OdataEtag { get; set; }
        public string Name { get; set; }
    }

    public class  PlanningAnalyticsList<T>
    {
        public List<T> value { get; set; }
    }


    public class PlanningAnalyticsMessageLogEntry
    {
        public int ID;
        public int ThreadID;
        public int SessionID;
        public LogLevel Level;
        public DateTime TimeStamp;
        public String Logger;
        public String Message;


    }


    public enum AttributeType
    {
        Numeric,
        String,
        Alias
    }

    public enum ElementType
    {
        Numeric = 1,
        String = 2,
        Consolidated = 3
    }

    public enum MemberType
    {
        Unknown = 0,
        Regular = 1,
        All = 2,
        Measure = 3,
        Formula = 4
    }

    public enum ProcessVariableType
    {
        String = 1,
        Numeric = 2
    }

    public enum CellStatus
    {
        Null,
        Data,
        Error
    }

    enum ConflictResolution
    {
        UsingSource,
        UsingTarget,
        Individually
    }

    public enum UserType
    {
        User = 0,
        SecurityAdmin = 1,
        DataAdmin = 2,
        Admin = 3
    }

    public enum ThreadType
    {
        User,
        Worker,
        Chore,
        System
    }

    public enum LogLevel
    {
        Fatal,
        Error,
        Warning,
        Info,
        Debug,
        Unknown
    }

    enum ChoreExecutionMode
    {
        SingleCommit = 0,
        MultipleCommit = 1
    }

    public enum IPVersion
    {
        IPv4 = 1,
        IPv6 = 2,
        Dual = 3
    }

    public enum SecurityMode
    {
        Basic = 1,
        Integrated = 2,
        Mixed = 3,
        CAM = 4
    }

    public enum FIPSMode
    {
        FIPS_MODE = 1,
        FIPS_APPROVED = 2,
        FIPS_NONE = 3
    }

    public enum SQLFetchType
    {
        ExtendedFetch = 1,
        FetchScroll = 2,
        Fetch = 3
    }

    public enum OracleErrorForceRowStatus
    {
        AutoDetect = 0,
        Default = 1,
        UseSQLULEN = 2
    }

    public enum ViewConsolidationOptimizationMethod
    {
        Tree = 0,
        Array = 1
    }
}
