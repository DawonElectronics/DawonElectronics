using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Xml;
using ConnectorDEMS;
using ConnectorDEMS.Models;

namespace ConnectorDEMS
{
    public class DemsHelper
    {
        public List<MesHoleInfo> MesHoleInfoQry(string tool)
        {
            var mesClient = new DemsOiClient();
            var qryHoleinfolist = new XmlDocument();

            qryHoleinfolist.LoadXml(XmlResources.Qry_LHoleInfoList);
            var toolchange = qryHoleinfolist.SelectSingleNode("//message//body//BINDV//SPECNR");

            toolchange.InnerText = tool;

            var qryHoleinfolistMsg = new IMesRuleService.MessageData();
            qryHoleinfolistMsg.CODE = "true";
            qryHoleinfolistMsg.COMMAND = "GetQueryResult";
            qryHoleinfolistMsg.COMMANDTYPE = "true";
            qryHoleinfolistMsg.OBJECT = qryHoleinfolist.OuterXml;

            qryHoleinfolistMsg.MESSAGE = "true";
            qryHoleinfolistMsg.USERID = "FPCBB4";

            var resultHoleinfolist = mesClient.ExecCommandAsync(qryHoleinfolistMsg);

            var resultHoleinfolistXml = new XmlDocument();


            resultHoleinfolistXml.LoadXml(resultHoleinfolist.Result.OBJECT.ToString());


            var sr = new StringReader(resultHoleinfolistXml["message"]["body"]["DATALIST"].OuterXml);

            Debug.WriteLine("MesHoleInfoQry");
            Debug.WriteLine((string?)resultHoleinfolist.Result.OBJECT.ToString());
            var dsHoleinfolist = new DataSet();
            dsHoleinfolist.ReadXml(sr);

            DataTable dt = dsHoleinfolist.Tables[0];


            var dt2 = dt.DefaultView.ToTable(false, new string[] { "PROCSEQ", "PROCCD", "PROCNM", "LAYERFR", "LAYERTO", "PRLAYERFR", "PRLAYERTO", "SHOT", "SPPROC", "LASERTYPE", "SVH", "SVHPAD", "ZDEPTH", "SPECNR", "ZSUM" });

            IList<MesHoleInfo> resultholeinfo = dt2.AsEnumerable().Select(row =>
                                                new MesHoleInfo
                                                {
                                                    ProcSeq = row.Field<string>("PROCSEQ"),
                                                    ProcCode = row.Field<string>("PROCCD"),
                                                    ProcName = row.Field<string>("PROCNM"),
                                                    TotalLayerFrom = row.Field<string>("LAYERFR"),
                                                    TotalLayerTo = row.Field<string>("LAYERTO"),
                                                    ProcLayerFrom = row.Field<string>("PRLAYERFR"),
                                                    ProcLayerTo = row.Field<string>("PRLAYERTO"),
                                                    LaserShot = row.Field<string>("SHOT"),
                                                    LaserType = row.Field<string>("SPPROC"),
                                                    LaserProcType = row.Field<string>("LASERTYPE"),
                                                    HoleSize = row.Field<string>("SVH"),
                                                    CapturePadSize = row.Field<string>("SVHPAD"),
                                                    ZDepth = row.Field<string>("ZDEPTH"),
                                                    ToolNo = row.Field<string>("SPECNR"),
                                                    HoleCount = row.Field<string>("ZSUM").Trim()
                                                }).ToList();


            mesClient.Close();
            return resultholeinfo.ToList();
        }

        public bool ExecuteCancelRcvlot(string lot)
        {
            bool result = false;
            var client = new DemsOiClient();
            try
            {
                //var client = factory.CreateChannel();
                XmlDocument exeCancelRcv = new XmlDocument();
                var resultCancelRcvXml = new XmlDocument();

                exeCancelRcv.LoadXml(XmlResources.Exe_RcvLot);

                var lotchange = exeCancelRcv.SelectSingleNode("//message//body//CANCELMOVELINERECEIVELIST//CANCELMOVELINERECEIVE//LOTID");
                lotchange.InnerText = lot;

                var exeCancelRcvMsg = new IMesRuleService.MessageData();
                exeCancelRcvMsg.COMMAND = "CancelMoveLineReceive";
                exeCancelRcvMsg.OBJECT = exeCancelRcv.OuterXml;

                var resultCancelRcvlot = client.ExecCommandAsync(exeCancelRcvMsg);
                resultCancelRcvXml.LoadXml(resultCancelRcvlot.Result.OBJECT.ToString());

                var sr = new StringReader(resultCancelRcvXml["message"]["return"]["returncode"].OuterXml);
                if (sr.ToString() == "0")
                { result = true; }

                client.Close();
                return result;
            }
            catch (Exception ex)
            {
                client.Abort();
                return result;
                throw;
            }
        }
        public List<MesLBodyCutInfo> MesLBodyCutInfoQry(string tool, string seq)
        {
            var mesClient = new DemsOiClient();
            var qrybodycutinfolist = new XmlDocument();

            qrybodycutinfolist.LoadXml(XmlResources.Qry_LBodyCut);
            var toolchange = qrybodycutinfolist.SelectSingleNode("//message//body//BINDV//SPECNR");
            var seqchange = qrybodycutinfolist.SelectSingleNode("//message//body//BINDV//SEQNR");

            toolchange.InnerText = tool;
            seqchange.InnerText = seq;

            var qryBodycutinfolistMsg = new IMesRuleService.MessageData();
            qryBodycutinfolistMsg.CODE = "true";
            qryBodycutinfolistMsg.COMMAND = "GetQueryResult";
            qryBodycutinfolistMsg.COMMANDTYPE = "true";
            qryBodycutinfolistMsg.OBJECT = qrybodycutinfolist.OuterXml;
            qryBodycutinfolistMsg.MESSAGE = "true";
            qryBodycutinfolistMsg.USERID = "103518";

            var resultBodycutinfolist = mesClient.ExecCommandAsync(qryBodycutinfolistMsg);

            var resultBodycutinfolistXml = new XmlDocument();


            resultBodycutinfolistXml.LoadXml(resultBodycutinfolist.Result.OBJECT.ToString());


            var sr = new StringReader(resultBodycutinfolistXml["message"]["body"]["DATALIST"].OuterXml);

            Debug.WriteLine("MesLBodyCutInfoQry");
            Debug.WriteLine((string?)resultBodycutinfolist.Result.OBJECT.ToString());
            var dsBodycutinfolist = new DataSet();
            dsBodycutinfolist.ReadXml(sr);

            DataTable dt = dsBodycutinfolist.Tables[0];

            var dt2 = dt.DefaultView.ToTable(false, new string[] { "PROCSEQ", "PROCCD", "PROCNM", "LAYERFR", "LAYERTO", "RSEQNR", "SPPROC", "SIDE", "STACK", "PROCDIST", "HOLESIZE", "HOLENR", "STUB", "DEPTH", "SPECNR", "MEABM", "SEQNR" });

            IList<MesLBodyCutInfo> resultbodycutinfo = dt2.AsEnumerable().Select(row =>
                                                new MesLBodyCutInfo
                                                {
                                                    ProcSeq = row.Field<string>("PROCSEQ"),
                                                    ProcCode = row.Field<string>("PROCCD"),
                                                    ProcName = row.Field<string>("PROCNM"),
                                                    RoundSeq = row.Field<string>("RSEQNR"),
                                                    SpecialProcessType = row.Field<string>("SPPROC"),
                                                    ProcLayerFrom = row.Field<string>("LAYERFR"),
                                                    ProcLayerTo = row.Field<string>("LAYERTO"),
                                                    Side = row.Field<string>("SIDE"),
                                                    Stack = row.Field<string>("STACK"),
                                                    ProcDistance = row.Field<string>("PROCDIST"),
                                                    HoleSize = row.Field<string>("HOLESIZE"),
                                                    HoleCount = row.Field<string>("HOLENR"),
                                                    Depth = row.Field<string>("DEPTH"),
                                                    ToolNo = row.Field<string>("SPECNR"),
                                                    DistanceUnit = row.Field<string>("MEABM"),
                                                    ToolSeq = row.Field<string>("SEQNR"),
                                                    Stub = row.Field<string>("STUB")
                                                }).ToList();

            mesClient.Close();
            return resultbodycutinfo.ToList();
        }

        public MesLotDetailInfo MesLotDetailInfoQry(string lot, string tool)
        {
            var mesClient = new DemsOiClient();
            var qryDetail13 = new XmlDocument();
            var qryDetail14 = new XmlDocument();

            qryDetail13.LoadXml(XmlResources.Qry_LotDetailInfo_13);
            qryDetail14.LoadXml(XmlResources.Qry_LotDetailInfo);

            var lotchange13 = qryDetail13.SelectSingleNode("//message//body//BINDV//LOTID");
            var toolchange13 = qryDetail13.SelectSingleNode("//message//body//BINDV//SPECNR");

            var lotchange14 = qryDetail14.SelectSingleNode("//message//body//BINDV//LOTID");

            lotchange13.InnerText = lot;
            toolchange13.InnerText = tool;

            lotchange14.InnerText = lot;

            var qryHoleinfolistMsg13 = new IMesRuleService.MessageData();
            qryHoleinfolistMsg13.CODE = "true";
            qryHoleinfolistMsg13.COMMAND = "GetQueryResult";
            qryHoleinfolistMsg13.COMMANDTYPE = "true";
            qryHoleinfolistMsg13.OBJECT = qryDetail13.OuterXml;
            qryHoleinfolistMsg13.TID = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
            qryHoleinfolistMsg13.MESSAGE = "true";
            qryHoleinfolistMsg13.USERID = "FPCBB4";

            //var result_infolist = channel.ExecCommand(qry_holeinfolist_msg);
            var resultInfolist = mesClient.ExecCommandAsync(qryHoleinfolistMsg13);
            var resultInfolistXml = new XmlDocument();

            resultInfolistXml.LoadXml(resultInfolist.Result.OBJECT.ToString());


            var sr = new StringReader(resultInfolistXml["message"]["body"]["DATALIST"].OuterXml);
            Debug.WriteLine("MesLotDetailInfoQry");
            Debug.WriteLine((string?)resultInfolist.Result.OBJECT.ToString());
            var dsInfolist = new DataSet();
            dsInfolist.ReadXml(sr);

            DataTable dt = dsInfolist.Tables[0];


            var dt2 = dt.DefaultView.ToTable(false, new string[] { "SPECNR", "LAYERNR", "LAYERSTR", "SPECTYPE", "SPECTYPE2", "PRDHA", "KDMAT", "REV", "REASON", "OKDAT", "SEQNR", });

            MesLotDetailInfo resultinfo = dt2.AsEnumerable().Select(row =>
                                                new MesLotDetailInfo
                                                {
                                                    ToolNo = row.Field<string>("SPECNR"),
                                                    LayerTotal = Convert.ToInt16(row.Field<string>("LAYERNR")),
                                                    LayerStructure = row.Field<string>("LAYERSTR"),
                                                    SpecType1 = row.Field<string>("SPECTYPE"),
                                                    SpecType2 = row.Field<string>("SPECTYPE2"),
                                                    ProductType = row.Field<string>("PRDHA"),
                                                    ModelName = row.Field<string>("KDMAT"),
                                                    ModelRev = row.Field<string>("REV").Trim(),
                                                    Reason = row.Field<string>("REASON"),
                                                    Okdat = row.Field<string>("OKDAT"),
                                                    Seqnr = row.Field<string>("SEQNR")

                                                }).First();
            var qryHoleinfolistMsg14 = new IMesRuleService.MessageData();
            qryHoleinfolistMsg14.CODE = "true";
            qryHoleinfolistMsg14.COMMAND = "GetQueryResult";
            qryHoleinfolistMsg14.COMMANDTYPE = "true";
            qryHoleinfolistMsg14.TID = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
            qryHoleinfolistMsg14.MESSAGE = "true";
            qryHoleinfolistMsg14.USERID = "FPCBB4";
            qryHoleinfolistMsg14.OBJECT = qryDetail14.OuterXml;

            var resultInfolist14 = mesClient.ExecCommandAsync(qryHoleinfolistMsg14);



            resultInfolistXml.LoadXml(resultInfolist14.Result.OBJECT.ToString());
            sr = new StringReader(resultInfolistXml["message"]["body"]["DATALIST"].OuterXml);
            var dsInfolist2 = new DataSet();
            dsInfolist2.ReadXml(sr);
            var dt3 = dsInfolist2.Tables[0];
            resultinfo.Shipto = dt3.AsEnumerable().Select(x => x.Field<string>("SHIPTO")).First();
            resultinfo.Kname = dt3.AsEnumerable().Select(x => x.Field<string>("KNAME")).First();
            mesClient.Close();
            return resultinfo;
        }

        public MesLotInfo MesLotInfoQry(string lot)
        {
            var mesClient = new DemsOiClient();
            var qryHoleinfolist = new XmlDocument();

            qryHoleinfolist.LoadXml(XmlResources.Qry_LotInfo);
            var lotchange = qryHoleinfolist.SelectSingleNode("//message//body//BINDV//LOTID");

            lotchange.InnerText = lot;


            var qryHoleinfolistMsg = new IMesRuleService.MessageData();
            qryHoleinfolistMsg.CODE = "true";
            qryHoleinfolistMsg.COMMAND = "GetQueryResult";
            qryHoleinfolistMsg.COMMANDTYPE = "true";
            qryHoleinfolistMsg.OBJECT = qryHoleinfolist.OuterXml;
            qryHoleinfolistMsg.MESSAGE = "true";
            qryHoleinfolistMsg.USERID = "FPCBB4";


            //var mesClient = new MesClient_DE_MS();
            //var channel = mesClient.ChannelFactory.CreateChannel();

            var resultInfolist = mesClient.ExecCommandAsync(qryHoleinfolistMsg);

            var resultInfolistXml = new XmlDocument();


            resultInfolistXml.LoadXml(resultInfolist.Result.OBJECT.ToString());


            var sr = new StringReader(resultInfolistXml["message"]["body"]["DATALIST"].OuterXml);
            Debug.WriteLine("MesLotInfoQry");
            Debug.WriteLine((string?)resultInfolist.Result.OBJECT.ToString());
            var dsInfolist = new DataSet();
            dsInfolist.ReadXml(sr);

            DataTable dt = dsInfolist.Tables[0];


            var dt2 = dt.DefaultView.ToTable(false, new string[] { "SPECNR", "ROOTPARENTLOTID", "PROCESSSEGMENTID", "LOCATION", "PANNELQTY", "SQAREMETER", "KDMAT", "KNAME" });

            MesLotInfo resultinfo = dt2.AsEnumerable().Select(row =>
                                                new MesLotInfo
                                                {
                                                    ToolNo = row.Field<string>("SPECNR"),
                                                    RootLotId = row.Field<string>("ROOTPARENTLOTID"),
                                                    ProcessSegmentId = row.Field<string>("PROCESSSEGMENTID"),
                                                    Location = row.Field<string>("LOCATION"),
                                                    PanelQty = row.Field<string>("PANNELQTY"),
                                                    SquareMeter = row.Field<string>("SQAREMETER"),
                                                    ModelNameFull = row.Field<string>("KDMAT"),
                                                    CustomerName = row.Field<string>("KNAME")
                                                }).First();

            mesClient.Close();
            return resultinfo;
        }
        public MesSpecInfo MesSpecInfoQry(string tool, string seqnr)
        {
            var mesClient = new DemsOiClient();
            var qrySpecinfolist = new XmlDocument();

            qrySpecinfolist.LoadXml(XmlResources.Qry_SpecInfo);
            var toolchange = qrySpecinfolist.SelectSingleNode("//message//body//BINDV//SPECNR");
            var seqchange = qrySpecinfolist.SelectSingleNode("//message//body//BINDV//SEQNR");
            toolchange.InnerText = tool;
            seqchange.InnerText = seqnr;

            var qrySpecinfolistMsg = new IMesRuleService.MessageData();

            qrySpecinfolistMsg.COMMAND = "GetQueryResult";

            qrySpecinfolistMsg.OBJECT = qrySpecinfolist.OuterXml;

            qrySpecinfolistMsg.MESSAGE = "true";
            qrySpecinfolistMsg.USERID = "FPCBB4";

            var resultSpecinfolist = mesClient.ExecCommandAsync(qrySpecinfolistMsg);

            var resultSpecinfolistXml = new XmlDocument();


            resultSpecinfolistXml.LoadXml(resultSpecinfolist.Result.OBJECT.ToString());


            var sr = new StringReader(resultSpecinfolistXml["message"]["body"]["DATALIST"].OuterXml);
            Debug.WriteLine("MesLotInfoQry");
            Debug.WriteLine((string?)resultSpecinfolist.Result.OBJECT.ToString());
            var dsSpecinfolist = new DataSet();
            dsSpecinfolist.ReadXml(sr);

            DataTable dt = dsSpecinfolist.Tables[0];


            var dt2 = dt.DefaultView.ToTable(false, new string[] { "WORKSIZEX", "WORKSIZEY", "ARRAYX", "ARRAYY", "PCPPANEL" });

            MesSpecInfo resultinfo = dt2.AsEnumerable().Select(row =>
                                                new MesSpecInfo
                                                {
                                                    Worksizex = Convert.ToInt16(row.Field<string>("WORKSIZEX")),
                                                    Worksizey = Convert.ToInt16(row.Field<string>("WORKSIZEY")),
                                                    Arrayx = Convert.ToInt16(row.Field<string>("ARRAYX")),
                                                    Arrayy = Convert.ToInt16(row.Field<string>("ARRAYY")),
                                                    Pcppanel = Convert.ToInt16(row.Field<string>("PCPPANEL"))
                                                }).First();

            mesClient.Close();
            return resultinfo;
        }
    }
}
