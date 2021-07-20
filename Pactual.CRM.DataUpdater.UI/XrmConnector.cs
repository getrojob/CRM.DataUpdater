using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataUpdater.UI
{
    public class XrmConnector
    {
        public CrmServiceClient Service { get; set; }
        public bool IsFaulted {
            get
            {
                return Service != null ? !String.IsNullOrEmpty(Service.LastCrmError) : false;
            }
        }
        public string FaultMessage {
            get {
                return Service != null ? (String.IsNullOrEmpty(Service.LastCrmError) ? (Service.LastCrmException != null ? Service.LastCrmException.Message : "") : Service.LastCrmError) : string.Empty;
            }
        }
        public XrmConnector(string userName, string password, string url)
        {
            var connectionString = String.Format("AuthType=Office365;Username={0};Password={1};Url={2};Domain=DOMAIN",
                userName,
                password,
                url);

            Service = new CrmServiceClient(connectionString);
        }
    }
}
