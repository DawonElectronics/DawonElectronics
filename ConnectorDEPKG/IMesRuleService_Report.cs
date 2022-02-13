// Decompiled with JetBrains decompiler
// Type: THiRA.MES.UI.MessageService.RuleServiceReport.IMesRuleService
// Assembly: MessageService, Version=1.2.2.1, Culture=neutral, PublicKeyToken=null
// MVID: 774B9968-F6FC-4CCB-BAD3-2A78FEA18DCC
// Assembly location: C:\Users\Administrator\AppData\Local\Apps\2.0\W5K5YAVL.PNB\ZNXXJDY0.ZXZ\thir..tion_0000000000000000_0002.0007_e2a5d03c6a0e4ed0\MessageService.dll


using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Cache;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;
using ConnectorDEPKG;
using CIM.MES.Common.Data;

namespace ConnectorDEPKG.RuleServiceReport
{
    [GeneratedCode("System.ServiceModel", "4.8.0.0")]
    [ServiceContract(ConfigurationName = "ConnectorDEPKG.RuleServiceReport.IMesRuleService")]
    public interface IMesRuleService
    {
        [OperationContract(Action = "http://tempuri.org/IMesRuleService/ExecCommand", ReplyAction = "http://tempuri.org/IMesRuleService/ExecCommandResponse")]
        [ServiceKnownType(typeof(MarshalByRefObject))]
        [ServiceKnownType(typeof(DBNull))]
        //[ServiceKnownType(typeof(BitmapImage))]
        //[ServiceKnownType(typeof(BitmapSource))]
        //[ServiceKnownType(typeof(BitmapCacheOption))]
        //[ServiceKnownType(typeof(BitmapCreateOptions))]
        //[ServiceKnownType(typeof(Rotation))]
        [ServiceKnownType(typeof(RequestCachePolicy))]
        //[ServiceKnownType(typeof(Freezable))]
        //[ServiceKnownType(typeof(DependencyObject))]
        //[ServiceKnownType(typeof(Int32Rect))]
        //[ServiceKnownType(typeof(Animatable))]
        //[ServiceKnownType(typeof(Bitmap))]
        //[ServiceKnownType(typeof(Image))]
        //[ServiceKnownType(typeof(DispatcherObject))]
        [ServiceKnownType(typeof(Dictionary<string, Dictionary<string, object>>))]
        [ServiceKnownType(typeof(Dictionary<string, object>))]
        [ServiceKnownType(typeof(Dictionary<string, object>[]))]
        [ServiceKnownType(typeof(Dictionary<object, object>[]))]
        [ServiceKnownType(typeof(Dictionary<object, object>))]
        [ServiceKnownType(typeof(Dictionary<string, Dictionary<string, object>>[]))]
        //[ServiceKnownType(typeof(Dictionary<string, BitmapImage>[]))]
        //[ServiceKnownType(typeof(Dictionary<string, BitmapImage>))]
        //[ServiceKnownType(typeof(Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, Bitmap>[]>[]>>[]))]
        //[ServiceKnownType(typeof(Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, Bitmap>[]>[]>>))]
        //[ServiceKnownType(typeof(Dictionary<string, Dictionary<string, Dictionary<string, Bitmap>[]>[]>))]
        //[ServiceKnownType(typeof(Dictionary<string, Dictionary<string, Bitmap>[]>[]))]
        //[ServiceKnownType(typeof(Dictionary<string, Dictionary<string, Bitmap>[]>))]
        //[ServiceKnownType(typeof(Dictionary<string, Bitmap>[]))]
        //[ServiceKnownType(typeof(Dictionary<string, Bitmap>))]
        //[ServiceKnownType(typeof(Dictionary<string, Dictionary<string, Dictionary<string, BitmapImage>[]>>[]))]
        //[ServiceKnownType(typeof(Dictionary<string, Dictionary<string, Dictionary<string, BitmapImage>[]>>))]
        //[ServiceKnownType(typeof(Dictionary<string, Dictionary<string, BitmapImage>[]>))]
        //[ServiceKnownType(typeof(Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, BitmapImage>[]>[]>>[]))]
        //[ServiceKnownType(typeof(Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, BitmapImage>[]>[]>>))]
        //[ServiceKnownType(typeof(Dictionary<string, Dictionary<string, Dictionary<string, BitmapImage>[]>[]>))]
        //[ServiceKnownType(typeof(Dictionary<string, Dictionary<string, BitmapImage>[]>[]))]
        //[ServiceKnownType(typeof(Dictionary<string, Dictionary<string, Dictionary<string, BitmapImage>>[]>[]))]
        //[ServiceKnownType(typeof(Dictionary<string, Dictionary<string, Dictionary<string, BitmapImage>>[]>))]
        //[ServiceKnownType(typeof(Dictionary<string, Dictionary<string, BitmapImage>>[]))]
        //[ServiceKnownType(typeof(Dictionary<string, Dictionary<string, BitmapImage>>))]
        //[ServiceKnownType(typeof(Dictionary<string, Dictionary<string, Dictionary<string, Bitmap>[]>>[]))]
        //[ServiceKnownType(typeof(Dictionary<string, Dictionary<string, Dictionary<string, Bitmap>[]>>))]
        [ServiceKnownType(typeof(Dictionary<string, Dictionary<string, Dictionary<string, object>>[]>[]))]
        [ServiceKnownType(typeof(Dictionary<string, Dictionary<string, Dictionary<string, object>>[]>))]
        [ServiceKnownType(typeof(object[]))]
        //[ServiceKnownType(typeof(Dictionary<string, Dictionary<string, Bitmap>>[]))]
        //[ServiceKnownType(typeof(Dictionary<string, Dictionary<string, Bitmap>>))]
        [ServiceKnownType(typeof(Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, object>>[]>>[]))]
        [ServiceKnownType(typeof(Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, object>>[]>>))]
        [ServiceKnownType(typeof(Dictionary<string, Dictionary<string, object>[]>[]))]
        [ServiceKnownType(typeof(Dictionary<string, Dictionary<string, object>[]>))]
        [ServiceKnownType(typeof(Dictionary<string, Dictionary<string, Dictionary<string, object>[]>>[]))]
        [ServiceKnownType(typeof(Dictionary<string, Dictionary<string, Dictionary<string, object>[]>>))]
        [ServiceKnownType(typeof(Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, object>[]>[]>>[]))]
        [ServiceKnownType(typeof(Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, object>[]>[]>>))]
        [ServiceKnownType(typeof(Dictionary<string, Dictionary<string, Dictionary<string, object>[]>[]>))]
        //[ServiceKnownType(typeof(Dictionary<string, Dictionary<string, Dictionary<string, Bitmap>>[]>[]))]
        //[ServiceKnownType(typeof(Dictionary<string, Dictionary<string, Dictionary<string, Bitmap>>[]>))]
        [ServiceKnownType(typeof(Stream))]
        //[ServiceKnownType(typeof(ImageSource))]
        MessageData ExecCommand(MessageData requestData);

        [OperationContract(Action = "http://tempuri.org/IMesRuleService/ExecCommand", ReplyAction = "http://tempuri.org/IMesRuleService/ExecCommandResponse")]
        Task<MessageData> ExecCommandAsync(MessageData requestData);

        
    }
}
