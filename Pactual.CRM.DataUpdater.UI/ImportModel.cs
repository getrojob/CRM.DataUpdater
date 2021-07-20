using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataUpdater.UI
{
    public class ImportModel
    {
        public string entityname { get; set; }
        public string entityid { get; set; }
        public string attributename { get; set; }
        public string attributetype { get; set; }
        public string lookupentitylogicalname { get; set; }
        public string attributevalue { get; set; }
        public string newrecordkey { get; set; }
    }
}
