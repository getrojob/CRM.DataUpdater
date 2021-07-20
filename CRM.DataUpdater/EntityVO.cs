using System;
using LINQtoCSV;
using System.ComponentModel;
using Microsoft.Xrm.Sdk;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Messages;

namespace CRM.DataUpdater
{
    class EntityVO
    {
        [CsvColumn(Name = "entityname", FieldIndex = 1)]
        public string EntityName { get; set; }

        [CsvColumn(Name = "entityid", FieldIndex = 2)]
        public Guid EntityId { get; set; }

        [CsvColumn(Name = "attributename", FieldIndex = 3)]
        public string AttributeName { get; set; }

        [CsvColumn(Name = "attributetype", FieldIndex = 4)]
        public string AttributeType { get; set; }

        [CsvColumn(Name = "lookupentitylogicalname", FieldIndex = 5)]
        public string LookupEntityLogicalName { get; set; }

        [CsvColumn(Name = "attributevalue", FieldIndex = 6)]
        public string AttributeValue { get; set; }

        [CsvColumn(Name = "newrecordkey", FieldIndex = 7)]
        public string NewRecordKey { get; set; }

        public object ConvertedAttributeValue
        {
            get
            {
                if ((String.IsNullOrEmpty(AttributeValue) && AttributeType != "delete") || (String.IsNullOrEmpty(AttributeValue) ? "NULL" : AttributeValue.Trim().ToUpper()) == "NULL")
                {
                    return null;
                }

                switch (AttributeType)
                {
                    case "string":
                        return (string)TypeDescriptor.GetConverter(typeof(string)).ConvertFromString(AttributeValue);
                    case "int":
                        return (int)TypeDescriptor.GetConverter(typeof(int)).ConvertFromString(AttributeValue);
                    case "double":
                        return (double)TypeDescriptor.GetConverter(typeof(double)).ConvertFromString(AttributeValue);
                    case "money":
                        return new Money((decimal)TypeDescriptor.GetConverter(typeof(decimal)).ConvertFromString(AttributeValue));
                    case "datetime":
                        DateTime test;
                        if (DateTime.TryParse(AttributeValue, out test))
                        {

                            DateTime? val_d = (DateTime?)TypeDescriptor.GetConverter(typeof(DateTime?)).ConvertFromString(AttributeValue);
                            return val_d.HasValue ? val_d : null;
                        }
                        else
                        {
                            return null;
                        }
                    case "bool":
                        return (bool?)TypeDescriptor.GetConverter(typeof(bool?)).ConvertFromString(AttributeValue);
                    case "lookup":
                        if (AttributeName == "ownerid" && EntityId != null && EntityId != Guid.Empty)
                        {
                            return new AssignRequest()
                            {
                                Assignee = new EntityReference(LookupEntityLogicalName, new Guid(AttributeValue)),
                                Target = new EntityReference(EntityName, EntityId)
                            };
                        }
                        else
                        {
                            if (AttributeValue == null)
                                return null;
                            else
                                return new EntityReference(LookupEntityLogicalName, new Guid(AttributeValue));
                        }
                    case "optionset":
                        int? val_o = (int?)TypeDescriptor.GetConverter(typeof(int?)).ConvertFromString(AttributeValue);
                        return val_o.HasValue ? new OptionSetValue(val_o.Value) : null;
                    case "share":
                        return new GrantAccessRequest()
                        {
                            PrincipalAccess = new PrincipalAccess() {
                                Principal = new EntityReference(LookupEntityLogicalName, new Guid(AttributeValue)),
                                AccessMask = AccessRights.ReadAccess | AccessRights.AppendAccess | AccessRights.AppendToAccess | AccessRights.WriteAccess
                            },
                            Target = new EntityReference(EntityName, EntityId)
                        };
                    case "unshare":
                        return new ModifyAccessRequest()
                        {
                            PrincipalAccess = new PrincipalAccess()
                            {
                                Principal = new EntityReference(LookupEntityLogicalName, new Guid(AttributeValue)),
                                AccessMask = AccessRights.None
                            },
                            Target = new EntityReference(EntityName, EntityId)
                        };
                    case "delete":
                        return new DeleteRequest()
                        {
                            Target = new EntityReference(EntityName, EntityId)
                        };
                    case "deactivate":
                        return new SetStateRequest()
                        {
                            EntityMoniker = new EntityReference(EntityName, EntityId),
                            State = new OptionSetValue(1),
                            Status = new OptionSetValue(2)
                        };
                    case "removemember":
                        return new RemoveMemberListRequest()
                        {
                            EntityId = EntityId,
                            ListId = new Guid(AttributeValue)
                        };
                    case "addmember":
                        return new AddMemberListRequest()
                        {
                            EntityId = EntityId,
                            ListId = new Guid(AttributeValue),
                        };
                    case "associate":
                        return new AssociateRequest()
                        {
                            Target = new EntityReference(EntityName, EntityId),
                            RelatedEntities = ((Func<EntityReferenceCollection>)(() =>
                            {
                                EntityReferenceCollection value = new EntityReferenceCollection();

                                value.Add(new EntityReference(LookupEntityLogicalName, new Guid(AttributeValue)));

                                return value;
                            }))(),
                            Relationship = new Relationship() {
                                SchemaName = AttributeName,
                                PrimaryEntityRole = EntityRole.Referencing
                            },
                        };
                    case "id":
                        return new Guid(AttributeValue);
                    default:
                        return AttributeValue;
                }
            }
        }
    }
}
