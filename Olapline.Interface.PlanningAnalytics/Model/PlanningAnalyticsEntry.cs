using Newtonsoft.Json;
using Olapline.Interface.PlanningAnalytics.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olapline.Interface.PlanningAnalytics.Model
{


    public class PlanningAnalyticsEntry
    {
        private string Path;

        [JsonProperty("@odata.type")]
        public string ODataType;


        public String ID;
        public String Name;
        public string ParentID;
        
        public String Type;

        public PlanningAnalytics.Repositories.PlanningAnalyticsApplicationsRepository Contents;



        public PlanningAnalyticsEntry()
        {

        }

        private IPlanningAnalyticsRestService _planningAnalyticsRestService;
        public void SetInterface(IPlanningAnalyticsRestService planningAnalyticsRestService)
        {
            _planningAnalyticsRestService = planningAnalyticsRestService;
            Contents = new Repositories.PlanningAnalyticsApplicationsRepository(_planningAnalyticsRestService);
        }

        public void SetInterface(IPlanningAnalyticsRestService planningAnalyticsRestService, string Path)
        {
            _planningAnalyticsRestService = planningAnalyticsRestService;
            if ( this.ODataType == null)
            {
                Contents = new Repositories.PlanningAnalyticsApplicationsRepository(_planningAnalyticsRestService, Path);
            }
            else
            {
                if (this.ODataType == "#ibm.tm1.api.v1.Folder")
                {
                    Contents = new Repositories.PlanningAnalyticsApplicationsRepository(_planningAnalyticsRestService, Path);
                }
                else
                {
                    Contents = null;
                }
            }


            
            this.Path = Path;
        }

        public byte[] Download()
        {
            dynamic File = this._planningAnalyticsRestService.Get<dynamic>(this.ID + "/Document/Content");
            //Stream returnFile = ;
            //byte[] byteArray = Encoding.UTF8.GetBytes(File);
            //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
            MemoryStream stream = new MemoryStream(File);
            return File;
        }

    }
}
