using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace CRM.DataUpdater
{
    public static class FaultHandler
    {
        public static void HandleFaults(ParameterCollection results, int? batchNumber)
        {
            if ((bool)results["IsFaulted"])
            {
                List<string> messages = new List<string>();

                Console.WriteLine(">> One or more errors occured in this batch.");

                ((ExecuteMultipleResponseItemCollection)results["Responses"]).Where(r => r.Fault != null).ToList().ForEach(r =>
                    messages.Add("Linha " + (((batchNumber ?? 0) + 1 ) + r.RequestIndex).ToString() + 
                    ": " + 
                    r.Fault.Message + 
                    (r.Fault.InnerFault != null ? "\n\tInner Fault: " + r.Fault.InnerFault.Message : ""))
                );

                WriteErrorLogEntry(messages);
            }
        }
        private static void WriteErrorLogEntry(List<string> messages)
        {
            string errorLogFullPath = "";
            try
            {
                errorLogFullPath = ConfigurationManager.AppSettings["ErrorLogFullPath"];
            }
            catch (Exception ex)
            {
                throw (new Exception("Error trying to read the config parameter:  ErrorLogFullPath.", ex));
            }

            System.IO.StreamWriter w = new System.IO.StreamWriter(errorLogFullPath, true);

            foreach (var message in messages)
            {
                w.WriteLine("[" + System.DateTime.Now.ToString() + "] " + message);
            }

            w.Close();
        }
    }
}
