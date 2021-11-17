using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olapline.Interface.PlanningAnalytics;
using Olapline.Interface.PlanningAnalytics.Model;

namespace Olapline.Interface.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string BaseUrl = "http://localhost:8091/api/v1/";
            PlanningAnalyticsConnection Connection = new PlanningAnalyticsConnection(BaseUrl);
            
            if ( Connection.Authenticate("admin", "0laplin3"))
            {

                Console.WriteLine("Instance Name:" + Connection.InstanceName);

                foreach(var process in Connection.Processes)
                {
                    Console.WriteLine(process.Name);
                    PlanningAnalytics.Model.PlanningAnalyticsProcess Process = Connection.Processes[process.Name];
                    Console.WriteLine(Process.DataSource.Type);

                }

                PlanningAnalytics.Model.PlanningAnalyticsDimension Dimension = Connection.Dimensions["Projects"];
                var Hier = Dimension.Hierarchies.ElementAt(0);
                foreach(var attr in Hier.ElementAttributes)
                {
                    Console.WriteLine(attr.Name);
                }

                foreach (var level in Hier.Levels)
                {
                    Console.WriteLine(level.Name);
                }
                Console.WriteLine(Dimension.Hierarchies.ElementAt(0).UniqueName + " Number of Elements:" + Hier.Cardinality);

                string MDX = @"Select NON EMPTY({[PF_MeasurePF].[PF_MeasurePF].[Umsatz]}) on 0, NON EMPTY ({[PF_Version].[PF_Version].[Actual]}*{TM1FILTERBYLEVEL( {TM1SUBSETALL( [PF_Projects] )}, 0)}
*{TM1FILTERBYLEVEL( {TM1SUBSETALL( [PF_Worker] )}, 0)}
*{TM1FILTERBYLEVEL( {TM1SUBSETALL( [PF_ActionCategory] )}, 0)}
*{TM1FILTERBYLEVEL( {TM1SUBSETALL( [PF_TimeCategory] )}, 0)}
*{TM1FILTERBYLEVEL( {TM1SUBSETALL( [Months] )}, 0)}

)
on 1
from [01_PF_Projektstunden]
WHERE 
[Years].[YEARS].[2016]";

                PlanningAnalyticsCellSet CellSet =  Connection.CellSets.createMDXCellSet(MDX);
                List<PlanningAnalyticsCell> Cells  = Connection.CellSets.Cells(CellSet.ID, 10000,0);
                foreach(var Cell in Cells)
                {
                    Console.WriteLine(Cell.FormattedValue);
                    foreach(var member in Cell.Members)
                    {
                        Console.WriteLine(member.Name);
                    }
                }


                foreach(var Entry in Connection.Applications)
                {
                    Console.WriteLine(Entry.Name + ":" + Entry.ID + ":" + Entry.ODataType);
                    //var EntryApps = Connection.Applications[Entry.Name];
                    //foreach(var SubEntry in EntryApps.Contents)
                    //{
                        //var SubEntryApps = Connection.Applications[SubEntry.Name];
                        //foreach (var SubSubEntry in SubEntryApps.Contents)
                        //{
                            

                          //  Console.WriteLine(SubSubEntry.Name);
                        //}
                    //    Console.WriteLine(SubEntry.Name);
                    //}
                }


            }

            Console.ReadLine();
        }
    }
}
