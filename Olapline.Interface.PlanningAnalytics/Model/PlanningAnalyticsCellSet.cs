using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olapline.Interface.PlanningAnalytics.Model
{
    public class PlanningAnalyticsCellSet
    {
        public String ID;
        public List<PlanningAnalyticsCellSetAxis> Axes;
        public List<PlanningAnalyticsCell> Cells;
    }



    public class PlanningAnalyticsCell
    {
        public long Ordinal;
        public CellStatus Status;
        public object Value;
        public String FormatString;
        public String FormattedValue;
        public int Updateable;
        public bool RuleDerived;
        public bool Annotated;
        public bool Consolidated;
        public int Language;
        public bool HasPicklist;
        public List<String> PicklistValues;
        public List<PlanningAnalyticsMember> Members;
    }


    public class PlanningAnalyticsCellSetAxis
    {

        public int Ordinal;
        public int Cardinality;
        public List<PlanningAnalyticsCellsetAxisTuple> Tuples;
    }

    public class PlanningAnalyticsCellsetAxisTuple
    {
        public int Ordinal;
        public List<PlanningAnalyticsMember> Members;
    }


    public class PlanningAnalyticsMember
    {
        public String Name;
        public String UniqueName;
        public MemberType Type;
        public int Ordinal;
        public bool IsPlaceholder;
        public Double Weight;
    }
}
