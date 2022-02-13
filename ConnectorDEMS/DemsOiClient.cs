using System;
using System.CodeDom.Compiler;
using System.ServiceModel;
using System.Threading.Tasks;

namespace ConnectorDEMS
{

    [GeneratedCode("System.ServiceModel", "4.8.0.0")]
    public class DemsOiClient : ClientBase<IMesRuleService>, IMesRuleService
    {
        public DemsOiClient() : base(GetDefaultBinding(), GetDefaultEndpointAddress())
        {
        }

        //static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint,
        //    System.ServiceModel.Description.ClientCredentials clientCredentials);
        

        public DemsOiClient(System.ServiceModel.Channels.Binding binding,
            EndpointAddress remoteAddress) : base(binding, remoteAddress)
        {
            //ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        //public virtual Task OpenAsync()
        //{
        //    return Task.Factory.FromAsync(((ICommunicationObject)(this)).BeginOpen(null, null), new Action<IAsyncResult>(((ICommunicationObject)(this)).EndOpen));
        //}

        //public virtual Task CloseAsync()
        //{
        //    return Task.Factory.FromAsync(((ICommunicationObject)(this)).BeginClose(null, null), new Action<IAsyncResult>(((ICommunicationObject)(this)).EndClose));
        //}
        public IMesRuleService.MessageData ExecCommand(IMesRuleService.MessageData requestData) => Channel.ExecCommand(requestData);
        public Task<IMesRuleService.MessageData> ExecCommandAsync(IMesRuleService.MessageData requestData) => Channel.ExecCommandAsync(requestData);
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            NetTcpBinding myBinding = new NetTcpBinding();
            myBinding.Name = "NetTcpBinding_IMesRuleService";
            myBinding.OpenTimeout = TimeSpan.FromMinutes(1);
            myBinding.CloseTimeout = TimeSpan.FromMinutes(1);
            myBinding.ReceiveTimeout = TimeSpan.FromMinutes(10);
            myBinding.SendTimeout = TimeSpan.FromMinutes(10);
            myBinding.TransferMode = TransferMode.Buffered;
            myBinding.MaxBufferSize = int.MaxValue;
            myBinding.MaxReceivedMessageSize = int.MaxValue;
            myBinding.Security.Mode = SecurityMode.None;
            myBinding.ReliableSession.Enabled = false;
            myBinding.ReliableSession.InactivityTimeout = TimeSpan.FromDays(15);
            myBinding.ReliableSession.Ordered = true;
            myBinding.ReaderQuotas.MaxDepth = 32;
            myBinding.ReaderQuotas.MaxStringContentLength = 2147483647;
            myBinding.ReaderQuotas.MaxArrayLength = 563840;
            myBinding.ReaderQuotas.MaxBytesPerRead = 4096;
            myBinding.ReaderQuotas.MaxNameTableCharCount = 16384;
            return myBinding;
        }

        private static EndpointAddress GetDefaultEndpointAddress()
        {
            EndpointAddress myEndpoint = new
                EndpointAddress("net.tcp://192.168.6.89:9003/DDGUIService1150/MesWcfService.svc");
            return myEndpoint;
        }

    }
    public interface IMesRuleServiceChannel : IMesRuleService, IClientChannel
    {
    }

}
