using Olapline.Interface.PlanningAnalytics.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olapline.Interface.PlanningAnalytics.Model
{
    public class PlanningAnalyticsProcessList
    {
        public List<PlanningAnalyticsProcess> value { get; set; }
    }


    public class PlanningAnalyticsProcess: PlanningAnalyticsObject
    {
        private IPlanningAnalyticsRestService _planningAnalyticsRestService;
        public void SetInterface(IPlanningAnalyticsRestService planningAnalyticsRestService)
        {
            _planningAnalyticsRestService = planningAnalyticsRestService;
        }
        
        public string PrologProcedure { get; set; }
        public string MetadataProcedure { get; set; }

        public string DataProcedure { get; set; }
        public string EpilogProcedure { get; set; }

        public bool HasSecurityAccess { get; set; }

        public List<ProcessParameter> Parameters { get; set; }
        public ProcessDataSource DataSource { get; set; }
        public List<ProcessVariable> Variables { get; set; }
        public List<String> VariablesUIData { get; set; }

        public dynamic Run(List<ProcessParameterRun> Parameters)
        {
            return _planningAnalyticsRestService.Post<dynamic>("Processes('" + this.Name + "')/tm1.ExecuteWithReturn", Parameters);
        }


    }


    public struct ProcessParameter
    {
        public String Name;
        public String Prompt;
        public dynamic Value;
        public string Type;
    }

    public struct ProcessParameterRun
    {
        public String Name;

        public dynamic Value;

    }

    public struct ProcessDataSource
    {
        public String Type;
        public String dataSourceNameForClient;
        public String dataSourceNameForServer;
        public String asciiThousandSeparator;
        public String asciiQuoteCharacter;

        //[JsonConverterAttribute(typeof(integerJsonConverter))]
        public int asciiHeaderRecords;

        public String asciiDelimiterType;
        public String asciiDelimiterChar;
        public String asciiDecimalSeparator;
        public String password;
        public String query;
        public String userName;
        public bool usesUnicode;
    }

    public struct ProcessVariable
    {
        public String Name;
        public String Type;
        public int Position;
        public int StartByte;
        public int EndByte;
    }

    
}
