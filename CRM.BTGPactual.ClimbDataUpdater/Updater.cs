using BTG.Climb.XRM.Connect;
using LINQtoCSV;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace CRM.DataUpdater
{
    public class Updater
    {
        private int BatchSize
        {
            get
            {
                try
                {
                    return Convert.ToInt32(ConfigurationManager.AppSettings["BatchSize"]);
                }
                catch (Exception ex)
                {
                    throw (new Exception("Error trying to read the config parameter: BatchSize.", ex));
                }
            }
        }
        private IEnumerable<EntityVO> _CsvEntities = null;
        private IEnumerable<EntityVO> CsvEntities
        {
            get {
                if(_CsvEntities != null)
                {
                    return _CsvEntities;
                }
                else
                {
                    _CsvEntities = ReadCsv();
                    return _CsvEntities;
                }
            }
        }
        private Connect365 _Connect365 = null;
        public Connect365 Connect365 {
            get
            {
                if(_Connect365 != null)
                {
                    return _Connect365;
                }
                else
                {
                    _Connect365 = new Connect365();
                    return _Connect365;
                }
            }
        }
        private CrmServiceClient _Service = null;
        public CrmServiceClient Service {
            get
            {
                if (_Service != null)
                {
                    return _Service;
                }
                else
                {
                    _Service = Connect365.Service;
                    return _Service;
                }
            }
        }

        public void Update()
        {
            UpdateEntities(ConvertToEntityCollection(CsvEntities.Where(e => e.EntityId != null && e.EntityId != Guid.Empty && e.AttributeName != "statecode" && e.AttributeName != "statuscode")));
            StatusUpdateEntities(ConvertToEntityCollection(CsvEntities.Where(e => e.EntityId != null && e.EntityId != Guid.Empty && (e.AttributeName == "statecode" || e.AttributeName == "statuscode"))));
        }

        public void Assign()
        {
            AssignEntities(CsvEntities.Where(e => e.AttributeName == "ownerid"));
        }

        public void Share()
        {
            ShareEntities(CsvEntities.Where(e => e.AttributeType == "share"));
        }

        public void Unshare()
        {
            ProcessRequests(CsvEntities.Where(e => e.AttributeType == "unshare"));
        }

        public void Delete()
        {
            ProcessRequests(CsvEntities.Where(e => e.AttributeType == "delete"));
        }

        public void Deactivate()
        {
            ProcessRequests(CsvEntities.Where(e => e.AttributeType == "deactivate"));
        }

        public void Create()
        {
            CreateEntities(ConvertToEntityCollection(CsvEntities.Where(e => e.EntityId == null || e.EntityId == Guid.Empty)));
        }

        public void AddListMember()
        {
            ProcessRequests(CsvEntities.Where(e => e.AttributeType == "addmember"));
        }

        public void RemoveListMember()
        {
            ProcessRequests(CsvEntities.Where(e => e.AttributeType == "removemember"));
        }

        public void Associate()
        {
            ProcessRequests(CsvEntities.Where(e => e.AttributeType == "associate"));
        }

        private IEnumerable<EntityVO> ReadCsv()
        {
            string fullpath = "";
            try
            {
                fullpath = ConfigurationManager.AppSettings["CsvFullPath"];
            }
            catch (Exception ex)
            {
                throw (new Exception("Error trying to read the config parameter: CsvFullPath.", ex));
            }

            CsvFileDescription inputFileDescription = new CsvFileDescription
            {
                FileCultureInfo = new System.Globalization.CultureInfo("pt-BR"),
                FileCultureName = "pt-BR",
                TextEncoding = System.Text.Encoding.UTF8,
                SeparatorChar = ',',
                FirstLineHasColumnNames = true,
                QuoteAllFields = true
            };

            CsvContext cc = new CsvContext();

            return cc.Read<EntityVO>(fullpath, inputFileDescription);
        }

        private void WriteErrorLogEntry(string message)
        {
            string errorLogFullPath = "";
            try
            {
                errorLogFullPath = ConfigurationManager.AppSettings["ErrorLogFullPath"];
            }
            catch (Exception ex)
            {
                throw (new Exception("Error trying to read the config parameter: ErrorLogFullPath.", ex));
            }

            System.IO.StreamWriter w = new System.IO.StreamWriter(errorLogFullPath, true);

            w.WriteLine("[" + System.DateTime.Now.ToString() + "] " + message);
            w.Close();
        }

        private void WriteErrorLogEntry(List<string> messages)
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

        private EntityCollection ConvertToEntityCollection(IEnumerable<EntityVO> csvEntities)
        {
            Dictionary<string, Entity> entities = new Dictionary<string, Entity>();

            foreach (EntityVO e in csvEntities)
            {
                if ((e.AttributeName == "ownerid" && e.EntityId != null && e.EntityId != Guid.Empty) || e.AttributeType == "share" || e.AttributeName == "delete" || e.AttributeType == "deactivate" || e.AttributeType == "removemember")
                    continue;

                string key = (String.IsNullOrEmpty(e.NewRecordKey) || String.IsNullOrWhiteSpace(e.NewRecordKey)) ? e.EntityId.ToString() : e.NewRecordKey.ToString();
                
                if (entities.ContainsKey(key))
                {
                    entities[key].Attributes.Add(e.AttributeName, e.ConvertedAttributeValue);
                }
                else
                {
                    var newEntitity = new Entity(e.EntityName);

                    if (String.IsNullOrEmpty(e.NewRecordKey) || String.IsNullOrWhiteSpace(e.NewRecordKey))
                        newEntitity.Id = e.EntityId;
                    
                    newEntitity.Attributes.Add(e.AttributeName, e.ConvertedAttributeValue);

                    entities.Add(key, newEntitity);
                }
            }

            return new EntityCollection(entities.Values.ToList<Entity>());
        }

        private void UpdateEntities(EntityCollection entities)
        {
            Console.WriteLine("Entities import started...");

            for (int i = 0; i <= entities.Entities.Count; i += BatchSize)
            {
                ExecuteMultipleRequest multReq = new ExecuteMultipleRequest()
                {
                    Requests = new OrganizationRequestCollection(),
                    Settings = new ExecuteMultipleSettings()
                    {
                        ContinueOnError = true,
                        ReturnResponses = true
                    }
                };

                if (i == 0)
                {
                    entities.Entities.Take(BatchSize).ToList<Entity>().ForEach(e =>
                        multReq.Requests.Add(
                            new UpdateRequest()
                            {
                                Target = e
                            }
                        )
                    );
                }
                else
                {
                    entities.Entities.Skip(i).Take(BatchSize).ToList<Entity>().ForEach(e =>
                        multReq.Requests.Add(
                            new UpdateRequest()
                            {
                                Target = e
                            }
                        )
                    );
                }

                OrganizationResponse response = Service.Execute(multReq);

                Console.WriteLine(String.Format("Processed entities {0} to {1}", i + 1, i + multReq.Requests.Count));

                FaultHandler.HandleFaults(response.Results, i);
            }

            Console.WriteLine("Entities import finished...");
        }

        private void StatusUpdateEntities(EntityCollection entities)
        {
            Console.WriteLine("Status update started...");

            for (int i = 0; i <= entities.Entities.Count; i += BatchSize)
            {
                ExecuteMultipleRequest multReq = new ExecuteMultipleRequest()
                {
                    Requests = new OrganizationRequestCollection(),
                    Settings = new ExecuteMultipleSettings()
                    {
                        ContinueOnError = true,
                        ReturnResponses = true
                    }
                };

                if (i == 0)
                {
                    entities.Entities.Take(BatchSize).ToList<Entity>().ForEach(e =>
                        multReq.Requests.Add(
                            new Microsoft.Crm.Sdk.Messages.SetStateRequest()
                            {
                                EntityMoniker = new EntityReference(e.LogicalName, e.Id),
                                State = e.Attributes.Contains("statecode") ? (OptionSetValue)e.Attributes["statecode"] : null,
                                Status = e.Attributes.Contains("statuscode") ? (OptionSetValue)e.Attributes["statuscode"] : null
                            }
                        )
                    );
                }
                else
                {
                    entities.Entities.Skip(i).Take(BatchSize).ToList<Entity>().ForEach(e =>
                        multReq.Requests.Add(
                            new Microsoft.Crm.Sdk.Messages.SetStateRequest()
                            {
                                EntityMoniker = new EntityReference(e.LogicalName, e.Id),
                                State = e.Attributes.Contains("statecode") ? (OptionSetValue)e.Attributes["statecode"] : null,
                                Status = e.Attributes.Contains("statuscode") ? (OptionSetValue)e.Attributes["statuscode"] : null
                            }
                        )
                    );
                }

                OrganizationResponse response = Service.Execute(multReq);

                Console.WriteLine(String.Format("Processed entities {0} to {1}", i + 1, i + multReq.Requests.Count));

                FaultHandler.HandleFaults(response.Results, i);
            }

            Console.WriteLine("Status update finished...");
        }
        
        private void CreateEntities(EntityCollection entities)
        {
            Console.WriteLine("Entities create started...");

            for (int i = 0; i <= entities.Entities.Count; i += BatchSize)
            {
                ExecuteMultipleRequest multReq = new ExecuteMultipleRequest()
                {
                    Requests = new OrganizationRequestCollection(),
                    Settings = new ExecuteMultipleSettings()
                    {
                        ContinueOnError = true,
                        ReturnResponses = true
                    }
                };

                if (i == 0)
                {
                    entities.Entities.Take(BatchSize).ToList<Entity>().ForEach(e =>
                        multReq.Requests.Add(
                            new CreateRequest()
                            {
                                Target = e
                            }
                        )
                    );
                }
                else
                {
                    entities.Entities.Skip(i).Take(BatchSize).ToList<Entity>().ForEach(e =>
                        multReq.Requests.Add(
                            new CreateRequest()
                            {
                                Target = e,
                            }
                        )
                    );
                }

                OrganizationResponse response = Service.Execute(multReq);

                //Coleção de Guids de registros criados e seus respectivos request indexes para recuperação de dados pós-execução
                /*
                var createdRecords = new Dictionary<int, Guid>();
                foreach (var item in ((ExecuteMultipleResponseItemCollection)response.Results["Responses"]).Where(o => o.Fault == null))
                {
                    createdRecords.Add(item.RequestIndex, ((Guid)item.Response.Results["id"]));
                }

                var entitiesToAssign = new List<EntityVO>();
                foreach (var item in createdRecords)
                {
                    if(((Entity)multReq.Requests[item.Key].Parameters["Target"]).Attributes.Contains("ownerid"))
                    {
                        entitiesToAssign.Add(new EntityVO
                        {
                            EntityName = ((Entity)multReq.Requests[item.Key].Parameters["Target"]).LogicalName,
                            EntityId = item.Value,
                            AttributeName = "ownerid",
                            AttributeType = "lookup",
                            AttributeValue = ((EntityReference)((Entity)multReq.Requests[item.Key].Parameters["Target"]).Attributes["ownerid"]).Id.ToString(),
                            LookupEntityLogicalName = ((EntityReference)((Entity)multReq.Requests[item.Key].Parameters["Target"]).Attributes["ownerid"]).LogicalName,
                        });
                    }
                }

                AssignEntities(entitiesToAssign);*/

                Console.WriteLine(String.Format("Processed entities {0} to {1}", i + 1, i + multReq.Requests.Count));

                FaultHandler.HandleFaults(response.Results, i);
            }

            Console.WriteLine("Entities create finished...");
        }

        private void AssignEntities(IEnumerable<EntityVO> entitiesToAssign)
        {
            Console.WriteLine("Entities assign started...");

            for (int i = 0; i <= entitiesToAssign.Count() - 1; i += BatchSize)
            {
                ExecuteMultipleRequest multReq = new ExecuteMultipleRequest()
                {
                    Requests = new OrganizationRequestCollection(),
                    Settings = new ExecuteMultipleSettings()
                    {
                        ContinueOnError = true,
                        ReturnResponses = false
                    }
                };

                if (i == 0)
                {
                    entitiesToAssign.Take(BatchSize).ToList<EntityVO>().ForEach(e =>
                        multReq.Requests.Add(
                            (Microsoft.Crm.Sdk.Messages.AssignRequest)e.ConvertedAttributeValue
                        )
                    );
                }
                else
                {
                    entitiesToAssign.Skip(i).Take(BatchSize).ToList<EntityVO>().ForEach(e =>
                        multReq.Requests.Add(
                            (Microsoft.Crm.Sdk.Messages.AssignRequest)e.ConvertedAttributeValue
                        )
                    );
                }

                OrganizationResponse response = Service.Execute(multReq);

                Console.WriteLine(String.Format("Processed entities {0} to {1}", i + 1, i + multReq.Requests.Count));

                FaultHandler.HandleFaults(response.Results, i);
            }

            Console.WriteLine("Entities assign finished...");
        }

        private void ShareEntities(IEnumerable<EntityVO> entitiesToShare)
        {
            Console.WriteLine(String.Format("Entities share started... ({0})", DateTime.Now));

            for (int i = 0; i <= entitiesToShare.Count() - 1; i += BatchSize)
            {
                ExecuteMultipleRequest multReq = new ExecuteMultipleRequest()
                {
                    Requests = new OrganizationRequestCollection(),
                    Settings = new ExecuteMultipleSettings()
                    {
                        ContinueOnError = true,
                        ReturnResponses = true
                    }
                };

                if (i == 0)
                {
                    entitiesToShare.Take(BatchSize).ToList<EntityVO>().ForEach(e =>
                        multReq.Requests.Add(
                            (Microsoft.Crm.Sdk.Messages.GrantAccessRequest)e.ConvertedAttributeValue
                        )
                    );
                }
                else
                {
                    entitiesToShare.Skip(i).Take(BatchSize).ToList<EntityVO>().ForEach(e =>
                        multReq.Requests.Add(
                            (Microsoft.Crm.Sdk.Messages.GrantAccessRequest)e.ConvertedAttributeValue
                        )
                    );
                }

                OrganizationResponse response = Service.Execute(multReq);

                Console.WriteLine(String.Format("Processed entities {0} to {1}", i + 1, i + multReq.Requests.Count));

                FaultHandler.HandleFaults(response.Results, i);
            }

            Console.WriteLine(String.Format("Entities share finished... ({0})", DateTime.Now));
        }

        private void ProcessRequests(IEnumerable<EntityVO> entitiesToProcesses)
        {
            for (int i = 0; i <= entitiesToProcesses.Count(); i += BatchSize)
            {
                ExecuteMultipleRequest multReq = new ExecuteMultipleRequest()
                {
                    Requests = new OrganizationRequestCollection(),
                    Settings = new ExecuteMultipleSettings()
                    {
                        ContinueOnError = true,
                        ReturnResponses = false
                    }
                };

                if (i == 0)
                {
                    entitiesToProcesses.Take(BatchSize).ToList<EntityVO>().ForEach(e =>
                        multReq.Requests.Add(
                            (Microsoft.Xrm.Sdk.OrganizationRequest)e.ConvertedAttributeValue
                        )
                    );
                }
                else
                {
                    entitiesToProcesses.Skip(i).Take(BatchSize).ToList<EntityVO>().ForEach(e =>
                        multReq.Requests.Add(
                            (Microsoft.Xrm.Sdk.OrganizationRequest)e.ConvertedAttributeValue
                        )
                    );
                }

                OrganizationResponse response = Service.Execute(multReq);

                Console.WriteLine(String.Format("Processed entities {0} to {1}", i + 1, i + multReq.Requests.Count));

                FaultHandler.HandleFaults(response.Results, i);
            }
        }
    }
}
