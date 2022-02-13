using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Cache;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;

namespace ConnectorDEMS
{
    [ServiceContractAttribute(ConfigurationName = "ConnectorDEMS.IMesRuleService")]
    public interface IMesRuleService
    {
        [OperationContractAttribute(Action = "http://tempuri.org/IMesRuleService/ExecCommand", ReplyAction = "http://tempuri.org/IMesRuleService/ExecCommandResponse")]
        
        [ServiceKnownTypeAttribute(typeof(DBNull))]
        [ServiceKnownTypeAttribute(typeof(Dictionary<string, Dictionary<string, object>>))]
        [ServiceKnownTypeAttribute(typeof(Dictionary<string, object>))]
        [ServiceKnownTypeAttribute(typeof(Dictionary<string, object>[]))]
        [ServiceKnownTypeAttribute(typeof(Dictionary<object, object>[]))]
        [ServiceKnownTypeAttribute(typeof(Dictionary<object, object>))]
        [ServiceKnownTypeAttribute(typeof(object[]))]
        [ServiceKnownTypeAttribute(typeof(Dictionary<string, Dictionary<string, object>>[]))]
        MessageData ExecCommand(MessageData requestData);

        [OperationContractAttribute(Action = "http://tempuri.org/IMesRuleService/ExecCommand", ReplyAction = "http://tempuri.org/IMesRuleService/ExecCommandResponse")]
        Task<MessageData> ExecCommandAsync(MessageData requestData);

        [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/CIM.MES.Common.Data")]
        [KnownType(typeof(List<Dictionary<string, Dictionary<string, object>>>))]
        [KnownType(typeof(DBNull))]
        [KnownType(typeof(Dictionary<string, Dictionary<string, object>>))]
        [KnownType(typeof(Dictionary<string, object>))]
        [Serializable]
        public partial class MessageData
        {
            
                [DataMember]
                public string TID { get; set; }

                [DataMember]
                public string COMMAND { get; set; }

                [DataMember]
                public string COMMANDTYPE { get; set; }

                [DataMember]
                public bool ISREQUEST { get; set; }

                [DataMember]
                public string SITEID { get; set; }

                [DataMember]
                public string USERID { get; set; }

                [DataMember]
                public string IPADDRESS { get; set; }

                [DataMember]
                public string LANGUAGE { get; set; }

                [DataMember]
                public object OBJECT { get; set; }

                [DataMember]
                public string CODE { get; set; }

                [DataMember]
                public string MESSAGE { get; set; }

                [DataMember]
                public Hashtable HASHTABLE { get; set; }

                [DataMember]
                public List<Hashtable> HASHLIST { get; set; }

                [DataMember]
                public List<Dictionary<string, object>> DATALIST { get; set; }

                [DataMember]
                public Dictionary<string, Dictionary<string, object>> DATADIC { get; set; }

                [DataMember]
                public DataTable DATATABLE { get; set; }

                [DataMember]
                public DataSet DATASET { get; set; }

                [DataMember]
                public bool ISSUCCESS { get; set; }

                [DataMember]
                public string EXCEPTIONMESSAGE { get; set; }

                public MessageData()
                {
                    this.HASHTABLE = new Hashtable();
                    this.HASHLIST = new List<Hashtable>();
                    this.DATALIST = new List<Dictionary<string, object>>();
                    this.DATADIC = new Dictionary<string, Dictionary<string, object>>();
                }

                public override string ToString() => string.Format("TID={0} CMD={1} TYPE={2} REQ={3} SITE={4} USER={5} IP={6} SUCCESS={7}", (object)this.TID, (object)this.COMMAND, (object)this.COMMANDTYPE, this.ISREQUEST ? (object)"Y" : (object)"N", (object)this.SITEID, (object)this.USERID, (object)this.IPADDRESS, this.ISSUCCESS ? (object)"Y" : (object)"N");


            }
    }
}
