using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using CIM.MES.Common.Data;

namespace ConnectorDEPKG.RuleServiceOI
{
    
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public class MesRuleServiceOIClient : ClientBase<IMesRuleService>, IMesRuleService
    {
        public MesRuleServiceOIClient() : base(GetDefaultBinding(), GetDefaultEndpointAddress())
        {

        }
        //static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint,
        //    System.ServiceModel.Description.ClientCredentials clientCredentials);
        public MesRuleServiceOIClient(System.ServiceModel.Channels.Binding binding,
            EndpointAddress remoteAddress) : base(binding, remoteAddress)
        {

        }
        public MessageData ExecCommand(MessageData requestData) => Channel.ExecCommand(requestData);

        public Task<MessageData> ExecCommandAsync(MessageData requestData) => Channel.ExecCommandAsync(requestData);

        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            BasicHttpBinding myBinding = new BasicHttpBinding(BasicHttpSecurityMode.None);

            myBinding.Name = "BasicHttpBinding_IMesRuleService";
            myBinding.CloseTimeout = new TimeSpan(0, 3, 10);
            myBinding.OpenTimeout = new TimeSpan(0, 3, 10);
            myBinding.ReceiveTimeout = new TimeSpan(0, 3, 10);
            myBinding.SendTimeout = new TimeSpan(0, 3, 10);
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
                EndpointAddress("http://192.168.8.234:8009/MesRuleService.svc");

            return myEndpoint;
        }

    }
}
