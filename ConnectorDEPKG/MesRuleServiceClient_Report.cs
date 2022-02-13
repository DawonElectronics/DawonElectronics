using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ConnectorDEPKG;
using CIM.MES.Common.Data;

namespace ConnectorDEPKG.RuleServiceReport
{
    [DebuggerStepThrough]
    [GeneratedCode("System.ServiceModel", "4.8.0.0")]
    public partial class MesRuleServiceReportClient : ClientBase<IMesRuleService>, IMesRuleService
    {
        public MesRuleServiceReportClient() : base(GetDefaultBinding(), GetDefaultEndpointAddress())
        {
        }
       
        public MesRuleServiceReportClient(System.ServiceModel.Channels.Binding binding,
            EndpointAddress remoteAddress) : base(binding, remoteAddress)
        {
            
        }
        public MessageData ExecCommand(MessageData requestData) => Channel.ExecCommand(requestData);

        public Task<MessageData> ExecCommandAsync(MessageData requestData) => Channel.ExecCommandAsync(requestData);

        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            BasicHttpBinding myBinding = new BasicHttpBinding(BasicHttpSecurityMode.None);

            myBinding.Name = "BasicHttpBinding_IMesRuleService2";
            myBinding.CloseTimeout = new TimeSpan(0, 7, 0);
            myBinding.OpenTimeout = new TimeSpan(0, 7, 0);
            myBinding.ReceiveTimeout = new TimeSpan(0, 7, 0);
            myBinding.SendTimeout = new TimeSpan(0, 7, 0);
            myBinding.AllowCookies = false;
            myBinding.BypassProxyOnLocal = false;
            myBinding.TransferMode = TransferMode.Streamed;
            myBinding.MaxBufferPoolSize = 2147483647;
            myBinding.MaxBufferSize = 2147483647;
            myBinding.MaxReceivedMessageSize = 2147483647;
            myBinding.MessageEncoding = WSMessageEncoding.Text;
            myBinding.TextEncoding = Encoding.UTF8;
            myBinding.ReaderQuotas.MaxDepth = 64;
            myBinding.ReaderQuotas.MaxStringContentLength = 2147483647;
            myBinding.ReaderQuotas.MaxArrayLength = 2147483647;
            myBinding.ReaderQuotas.MaxBytesPerRead = 2147483647;
            myBinding.ReaderQuotas.MaxNameTableCharCount = 2147483647;

            return myBinding;
        }

        private static EndpointAddress GetDefaultEndpointAddress()
        {
            EndpointAddress myEndpoint = new
                EndpointAddress("http://192.168.8.234:8007/MesRuleService.svc");

            return myEndpoint;
        }

    }

}
