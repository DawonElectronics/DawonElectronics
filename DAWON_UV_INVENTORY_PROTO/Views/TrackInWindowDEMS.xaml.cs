using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using DAWON_UV_INVENTORY_PROTO.Models;
using System.Xml.Serialization;
using DAWON_UV_INVENTORY_PROTO.ViewModels;
using Newtonsoft.Json;
using System.Diagnostics;
using System.ServiceModel;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using ConnectorDEMS;
using DAWON_UV_INVENTORY_PROTO.Helper;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace DAWON_UV_INVENTORY_PROTO.Views
{
    /// <summary>
    /// Interaction logic for TrackInWindow.xaml
    /// </summary>
    public partial class TrackInWindowDems : ChromelessWindow
    {
        
        public TrackInWindowDemsViewModel TrackinDemsViewmodel = new TrackInWindowDemsViewModel();
        DemsHelper _demsClient = new DemsHelper();
        public TrackInWindowDems()
        {
                    
            InitializeComponent();
            TrackinDemsViewmodel.SegementDataTable = de_ms_qry_workcenter();
            this.DataContext = TrackinDemsViewmodel;
           
        }
       
        private DataTable de_ms_qry_workcenter()
        {
            //var client = new DemsOiClient();
            var client = new DemsOiClient();
            try
            {
                XmlDocument qryWorkcenter = new XmlDocument();
                var resultWorkcenterXml = new XmlDocument();

                qryWorkcenter.LoadXml(Resource1.Qry_CboWorkCenter);

                var qryWorkcenterMsg = new IMesRuleService.MessageData();
                qryWorkcenterMsg.COMMAND = "GetQueryResult";
                qryWorkcenterMsg.OBJECT = qryWorkcenter.OuterXml;

                //var result_workcenter = client.ExecCommandAsync(new MesClient_DE_MS.ExecCommandRequest(qry_workcenter_msg));
                var aa = client.ChannelFactory.CreateChannel();
                
                var resultWorkcenter = client.ExecCommand(qryWorkcenterMsg);
                resultWorkcenterXml.LoadXml(resultWorkcenter.OBJECT.ToString());
                //result_lotlist_xml.LoadXml(result_lotlist);
                var sr = new StringReader(resultWorkcenterXml["message"]["body"]["DATALIST"].OuterXml);

                var dsWorkcenter = new DataSet();
                dsWorkcenter.ReadXml(sr);
                DataTable dt = dsWorkcenter.Tables[0];

                
                return dt;

                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                client.Abort();
                return null;
            }
        }
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
        private void cmb_input_segment_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            GridRcv.ItemsSource = de_ms_qry_rcv_lotlist(TrackinDemsViewmodel.WorkcenterId);

        }
        private DataView de_ms_qry_rcv_lotlist(string workcenter)
        {
            var client = new DemsOiClient();

            try
            {
                XmlDocument qryRcvlotlist = new XmlDocument();
                var resultRcvlotlistXml = new XmlDocument();

                qryRcvlotlist.LoadXml(Resource1.Qry_RcvLotList);

                var workcenterchange = qryRcvlotlist.SelectSingleNode("//message//body//BINDV//WORKCENTER");
                workcenterchange.InnerText = workcenter;


                var qryLotlistMsg = new IMesRuleService.MessageData();
                
                qryLotlistMsg.COMMAND = "GetStoredProcedureResult";
                
                qryLotlistMsg.OBJECT = qryRcvlotlist.OuterXml;

                //var result_lotlist = client.ExecCommandAsync(new MesClient_DE_MS.ExecCommandRequest(qry_lotlist_msg));
                var resultLotlist = client.ExecCommand(qryLotlistMsg);
                resultRcvlotlistXml.LoadXml(resultLotlist.OBJECT.ToString());
                
                var sr = new StringReader(resultRcvlotlistXml["message"]["body"]["DATASET"]["DATALIST"].OuterXml);

                var dsRcvlist = new DataSet();
                dsRcvlist.ReadXml(sr);
                
                string jsonString = string.Empty;
                //jsonString = JsonConvert.SerializeObject(dsRcvlist.Tables[0]);

                //var rcvlist = JsonConvert.DeserializeObject<List<DemsRcvList>>(jsonString);
                var rcvlist = dsRcvlist.Tables[0].DefaultView;
                client.Close();
                return rcvlist;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                client.Abort();
                return null;
            }
        }

        private void grid_rcv_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var rowdata = GridRcv.SelectedItem;
                var pC = GridRcv.View.GetPropertyAccessProvider();

                var tool = pC.GetValue(rowdata, "TOOLNUMBER") as String;
                var lot = pC.GetValue(rowdata, "LOTID") as String;
                var prcname = pC.GetValue(rowdata, "PROCESSSEGMENTNAME") as String;
                var workcenter = TrackinDemsViewmodel.WorkcenterId;
                var pnlqty = Convert.ToInt16(pC.GetValue(rowdata, "PANNELQTY") as String);
                var lotdetailinfo = _demsClient.MesLotDetailInfoQry(lot, tool);
                var fromlayer = pC.GetValue(rowdata, "USR02") as String;

                bool issample = false;
                if (Char.IsLetter(lot, 0) && !lot.ToLower().StartsWith("p"))
                {
                    issample = true;
                }

                if (!IsToolExist(tool, prcname))
                {
                    RegistTool(tool, workcenter, lot, issample, lotdetailinfo.Seqnr);
                }
                using (var context = new Db_Uv_InventoryContext())
                {
                    var inputTemp = new TbUvWorkorder();

                    //고객사 선택, 필수항목으로 입력 여부 체크

                    var cust = new TbCustomer();
                    var selcust = "대덕전자(MS)";
                    cust = context.TbCustomer.Where(x => x.CustName == selcust).FirstOrDefault();
                    inputTemp.CustId = cust.CustId;

                    //작성자 선택, 필수항목으로 입력 여부 체크

                    var usr = new TbUsers();
                    var seluser = MainWindow.MainwindowViewModel.SelectedUser;
                    usr = context.TbUsers.Where(x => x.UserName == seluser).FirstOrDefault();
                    inputTemp.TrackinUser = usr;

                    //LOT입력, 필수항목으로 입력 여부 체크

                    inputTemp.Lotid = lot;

                    //툴입력, 필수항목으로 입력 여부 체크
                    string pid = string.Empty;
                    if (fromlayer == null)
                        pid = MainWindow.MainwindowViewModel.ToolInfos.Where(w => w.CustToolno == tool).Select(x => x.ProductId).FirstOrDefault();
                    else if (fromlayer != null)
                        pid = MainWindow.MainwindowViewModel.ToolInfos.Where(w => w.CustToolno == tool&&w.PrcLayerFrom1 == fromlayer).Select(x => x.ProductId).FirstOrDefault();

                    inputTemp.ProductId = pid;
                    inputTemp.SampleOrder = issample;
                    inputTemp.Pnlqty = pnlqty;
                    inputTemp.CreateTime = DateTime.Now;
                    inputTemp.TrackinTime = DateTime.Now;
                    inputTemp.Txid = Guid.NewGuid();
                    var lotcount = context.TbUvWorkorder.Where(x => x.Lotid == lot && x.IsDone == false && x.ProductId == pid).Count();
                    if (lotcount == 0)
                    {
                        context.TbUvWorkorder.AddAsync(inputTemp);
                        context.SaveChanges();
                        ExecuteRcv(lot);
                        
                    }
                    else if (lotcount != 0)
                    {
                        if (MessageBox.Show("중복된 LOT가 있습니다 추가하시겠습니까?", "중복 LOT 확인", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            context.TbUvWorkorder.AddAsync(inputTemp);
                            context.SaveChanges();
                            ExecuteRcv(lot);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            GridRcv.ItemsSource = de_ms_qry_rcv_lotlist(TrackinDemsViewmodel.WorkcenterId);
            UpdateFiltered_WorkorderList();
        }

        
        //여러로트 받기
        private void btn_exe_rcv_Click(object sender, RoutedEventArgs e)
        {
            BtnExeRcv.IsEnabled = false;
            var selectedlist = GridRcv.SelectionController.SelectedRows;
            var rcvlist = new List<string>();
            string lot = string.Empty;
            var tool_prcname = string.Empty;
            var dbUser = new List<TbUsers>();
            var dbCustomer = new List<TbCustomer>();
            using (var db = new Db_Uv_InventoryContext())
            {
                dbUser = db.TbUsers.ToList<TbUsers>();
                dbCustomer = db.TbCustomer.ToList<TbCustomer>();
            }

            foreach (var item in selectedlist)
            {
                try
                {
                    var rowdata = item.RowData;
                    var pC = GridRcv.View.GetPropertyAccessProvider();
                    var tool = pC.GetValue(rowdata, "TOOLNUMBER") as String;
                    lot = pC.GetValue(rowdata, "LOTID") as String;

                    var prcname = pC.GetValue(rowdata, "PROCESSSEGMENTNAME") as String;
                    var workcenter = TrackinDemsViewmodel.WorkcenterId;
                    var pnlqty = Convert.ToInt16(pC.GetValue(rowdata, "PANNELQTY"));
                    var fromlayer = pC.GetValue(rowdata, "USR02") as String;



                    var lotdetailinfo = _demsClient.MesLotDetailInfoQry(lot, tool);
                    bool issample = false;
                    if (Char.IsLetter(lot, 0) && (!lot.ToLower().StartsWith("p") || !lot.ToLower().StartsWith("w")))
                    {
                        issample = true;
                    }

                    if (tool_prcname != tool + prcname)
                    {
                        if (!IsToolExist(tool, prcname))
                        {
                            RegistTool(tool, workcenter, lot, issample, lotdetailinfo.Seqnr);
                        }
                    }

                    tool_prcname = tool + prcname;

                    using (var context = new Db_Uv_InventoryContext())
                    {
                        var inputTemp = new TbUvWorkorder();

                        //고객사 선택, 필수항목으로 입력 여부 체크

                        
                        var selcust = "대덕전자(MS)";
                        inputTemp.CustId = MainWindow.MainwindowViewModel.ToolInfos.Where(x => x.CustName == selcust).FirstOrDefault().CustId;
                        

                        //작성자 선택, 필수항목으로 입력 여부 체크

                        var seluser = MainWindow.MainwindowViewModel.SelectedUser;
                        inputTemp.TrackinUser = dbUser.Where(x => x.UserName == seluser).FirstOrDefault();
                        

                        //LOT입력, 필수항목으로 입력 여부 체크

                        inputTemp.Lotid = lot;

                        //툴입력, 필수항목으로 입력 여부 체크                
                        string pid = string.Empty;
                        if (fromlayer == null)
                            pid = MainWindow.MainwindowViewModel.ToolInfos.Where(w => w.CustToolno == tool).Select(x => x.ProductId).FirstOrDefault();
                        else if (fromlayer != null)
                            pid = MainWindow.MainwindowViewModel.ToolInfos.Where(w => w.CustToolno == tool && w.PrcLayerFrom1 == fromlayer).Select(x => x.ProductId).FirstOrDefault();

                        inputTemp.ProductId = pid;
                        inputTemp.SampleOrder = issample;
                        inputTemp.Pnlqty = pnlqty;
                        inputTemp.CreateTime = DateTime.Now;
                        inputTemp.TrackinTime = DateTime.Now;
                        inputTemp.Txid = Guid.NewGuid();
                        var lotcount = context.TbUvWorkorder.Where(x => x.Lotid == lot && x.IsDone == false && x.ProductId == pid).Count();
                        if (lotcount == 0)
                        {
                            context.TbUvWorkorder.AddAsync(inputTemp);
                            context.SaveChanges();
                            //ExecuteRcv(lot);
                            rcvlist.Add(lot);
                        }
                        else if (lotcount != 0)
                        {
                            if (MessageBox.Show("중복된 LOT가 있습니다 추가하시겠습니까?", "중복 LOT 확인", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            {
                                context.TbUvWorkorder.AddAsync(inputTemp);
                                context.SaveChanges();
                                //ExecuteRcv(lot);
                                rcvlist.Add(lot);
                            }
                            else if (MessageBox.Show("중복된 LOT가 있습니다 추가하시겠습니까?", "중복 LOT 확인", MessageBoxButton.YesNo) == MessageBoxResult.No)
                            {
                                //ExecuteRcv(lot);
                                rcvlist.Add(lot);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (rcvlist.Count == 1)
                ExecuteRcv(lot);
            else if (rcvlist.Count >1)
                ExecuteMultiRcv(rcvlist);

            GridRcv.ItemsSource = de_ms_qry_rcv_lotlist(TrackinDemsViewmodel.WorkcenterId);
            UpdateFiltered_WorkorderList();
            BtnExeRcv.IsEnabled = true;
        }
        public void UpdateFiltered_WorkorderList()
        {
            using (var db = new Db_Uv_InventoryContext())
            {
                var customer = MainWindow.MainwindowViewModel.SelectedCustomerWo;
                var issample = false;
                if (MainWindow.MainwindowViewModel.SelectedIsSampleWo == "양산") issample = false;
                else if (MainWindow.MainwindowViewModel.SelectedIsSampleWo == "샘플") issample = true;
                MainWindow.MainwindowViewModel.WorkOrderList =
                    new ObservableCollection<ViewUvWorkorder>(db.ViewUvWorkorder.Where(x =>
                        x.CustName == customer && x.SampleOrder == issample));
            }
        }
        private bool ExecuteRcv(string lot)
        {
            bool result = false;
            var client = new DemsOiClient();
            try
            {
                XmlDocument exeRcv = new XmlDocument();
                var resultRcvXml = new XmlDocument();

                exeRcv.LoadXml(Resource1.Exe_RcvLot);

                var lotchange = exeRcv.SelectSingleNode("//message//body//MOVELINERECEIVELIST//MOVELINERECEIVE//LOTID");
                lotchange.InnerText = lot;

                var qryLotlistMsg = new IMesRuleService.MessageData();
                
                qryLotlistMsg.COMMAND = "MoveLineReceive";
                
                qryLotlistMsg.OBJECT = exeRcv.OuterXml;

                var resultLotlist =client.ExecCommandAsync(qryLotlistMsg);
                //var result_lotlist = client.ExecCommandAsync(new MesClient_DE_MS.ExecCommandRequest(qry_lotlist_msg));
                resultRcvXml.LoadXml(resultLotlist.Result.OBJECT.ToString());

                var sr = new StringReader(resultRcvXml["message"]["return"]["returncode"].OuterXml);
                if (sr.ToString() == "0")
                { result = true;}

                client.Close();
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                client.Abort();
                return result;
            }
        }

        private bool ExecuteMultiRcv(List<string> lot)
        {
            bool result = false;
            var client = new DemsOiClient();
            try
            {
                var resultRcvXml = new XmlDocument();
                var rcvitemxml = new XmlDocument();
                var exeMultircv = new XmlDocument();
                exeMultircv.LoadXml(Resource1.Exe_RcvLot);
                
                var lotchange = exeMultircv.SelectSingleNode("//message//body//MOVELINERECEIVELIST");
                lotchange.FirstChild.SelectSingleNode("LOTID").InnerText = lot[0];

                for (int i = 1; i < lot.Count; i++)
                {
                    var tempnode = exeMultircv.SelectSingleNode("message//body//MOVELINERECEIVELIST//MOVELINERECEIVE").Clone();
                    tempnode.SelectSingleNode("LOTID").InnerText = lot[i];
                    lotchange.AppendChild(tempnode);
                }
                
                
                var qryLotlistMsg = new IMesRuleService.MessageData();
                
                qryLotlistMsg.COMMAND = "MoveLineReceive";
                
                qryLotlistMsg.OBJECT = exeMultircv.OuterXml;

                var resultLotlist = client.ExecCommand(qryLotlistMsg);
                //var result_lotlist = client.ExecCommandAsync(new MesClient_DE_MS.ExecCommandRequest(qry_lotlist_msg));
                resultRcvXml.LoadXml(resultLotlist.OBJECT.ToString());

                var sr = new StringReader(resultRcvXml["message"]["return"]["returncode"].OuterXml);
                if (sr.ToString() == "0")
                { result = true; }

                client.Close();
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                client.Abort();
                return result;
            }
        }

        private bool IsToolExist(string tool,string prcname)
        {
            var result = false;
            using (var context = new Db_Uv_InventoryContext())
            {
                var toolinfo = context.TbUvToolinfo.Where(w => w.CustToolno == tool && w.MesPrcName == prcname);
                if (toolinfo.Count() > 0)
                    result = true;
                else if (toolinfo.Count() == 0)
                    result = false;

            }

            return result;
        }

        private void RegistTool(string tool, string workcenter, string lot, bool issample,string seqnr)
        {
            try
            {
                using (var context = new Db_Uv_InventoryContext())
                {
                    var tools = context.TbUvToolinfo.Where(w => w.CustToolno == tool);
                    var ldrillinfoSeq = _demsClient.MesHoleInfoQry(tool).Where(x=>x.ProcName.ToLower().Contains("uv")).GroupBy(x => x.ProcSeq).Select(x => x.Key).ToList<string>();
                    var lbcutinfoSeq   = _demsClient.MesLBodyCutInfoQry(tool,seqnr).GroupBy(x => x.ProcSeq).Select(x => x.Key).ToList<string>();

                    foreach (var seq in ldrillinfoSeq)
                    {
                        if (tools.Where(x => x.MesSeqCode == seq).Count() == 0)
                        {
                            var tempTool = GetTbUvToolinfo_DE_MS(tool, workcenter, lot, seq,issample);
                            context.TbUvToolinfo.AddAsync(tempTool);
                            context.SaveChanges();
                            MainWindow.MainwindowViewModel.ToolInfos = new ObservableCollection<TbUvToolinfo>(context.TbUvToolinfo);
                        }

                        else if (tools.Where(x => x.MesSeqCode == seq).Count() > 0)
                        {
                            continue;
                        }
                    }

                    foreach (var seq in lbcutinfoSeq)
                    {
                        if (tools.Where(x => x.MesSeqCode == seq).Count() == 0)
                        {
                            var tempTool = GetTbUvToolinfo_DE_MS_lbcut(tool, workcenter, lot, seq, issample, seqnr);
                            context.TbUvToolinfo.AddAsync(tempTool);
                            context.SaveChanges();
                            MainWindow.MainwindowViewModel.ToolInfos = new ObservableCollection<TbUvToolinfo>(context.TbUvToolinfo);
                        }

                        else if (tools.Where(x => x.MesSeqCode == seq).Count() > 0)
                        {
                            continue;
                        }
                    }


                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        //드릴 정보 조회
        private TbUvToolinfo GetTbUvToolinfo_DE_MS(string tool, string workcenter, string lot, string seq, bool issample)
        {
            var tempTool = new TbUvToolinfo();

            try
            {
                using (var context = new Db_Uv_InventoryContext())

                {
                    tempTool.CustId = "UV_01";
                    tempTool.CustName = "대덕전자(MS)";
                    var lotdetailinfo = _demsClient.MesLotDetailInfoQry(lot, tool);
                    var ldrillinfo = _demsClient.MesHoleInfoQry(tool);
                    var lotinfo = _demsClient.MesLotInfoQry(lot);
                    var specinfo = _demsClient.MesSpecInfoQry(tool, lotdetailinfo.Seqnr);
                    var countItem = ldrillinfo.FindAll(f => f.LaserType.Contains("UV"));
                    var countDbItem = context.TbUvToolinfo.Where(w => w.CustToolno == tool && w.MesPrcName.Contains("Uv"));
                    
                    tempTool.CustModelname = lotdetailinfo.ModelName;
                    tempTool.CustRevision = lotdetailinfo.ModelRev.Trim();
                    tempTool.CustToolno = lotdetailinfo.ToolNo;
                    tempTool.Layer = lotdetailinfo.LayerTotal;
                    tempTool.EndCustomer = lotdetailinfo.Kname;
                    tempTool.CustomerShipto = lotdetailinfo.Shipto;
                    tempTool.ProductType = lotdetailinfo.SpecType2;
                    tempTool.MesSeqCode = seq;
                    tempTool.MesPrcName = ldrillinfo.Where(t => t.ProcSeq == seq).First().ProcName;
                    tempTool.MesPrcCode = ldrillinfo.Where(t => t.ProcSeq == seq).First().ProcCode;
                    
                    tempTool.StackType = lotdetailinfo.SpecType1;
                    tempTool.PrdCategory = lotdetailinfo.ProductType;
                    tempTool.LayerStructure = lotdetailinfo.LayerStructure;
                    tempTool.CustComment = lotdetailinfo.Reason;
                    tempTool.CreateDate = DateTime.ParseExact(lotdetailinfo.Okdat, "yyyyMMdd", null);

                    tempTool.ArrayBlk = specinfo.Arrayx * specinfo.Arrayy;
                    tempTool.WorksizeX = specinfo.Worksizex;
                    tempTool.WorksizeY = specinfo.Worksizey;
                    tempTool.Pcs = specinfo.Pcppanel;

                    tempTool.MainHoleSize = ldrillinfo.Where(t => t.ProcSeq == seq).First().HoleSize.Trim();

                    if (ldrillinfo.FindAll(f => f.ProcSeq == seq).Count() == 1)
                    {
                        //temp_tool.HoleCount = ldrillinfo.Where(t => t.ProcSeq == seq).First().HoleCount.Trim();
                        tempTool.PrcLayerFrom1 = ldrillinfo.Where(t => t.ProcSeq == seq).First().ProcLayerFrom;
                        tempTool.PrcLayerTo1 = ldrillinfo.Where(t => t.ProcSeq == seq).First().ProcLayerTo;
                        tempTool.HoleCount1 = ldrillinfo.Where(t => t.ProcSeq == seq).First().HoleCount;
                        tempTool.HoleCount = "CS:" + tempTool.HoleCount1;
                    }

                    if (ldrillinfo.FindAll(f => f.ProcSeq == seq).Count() == 2)
                    {
                        tempTool.PrcLayerFrom1 = ldrillinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode).First().ProcLayerFrom;
                        tempTool.PrcLayerTo1 = ldrillinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode).First().ProcLayerTo;
                        tempTool.HoleCount1 = ldrillinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode).First().HoleCount;

                        tempTool.PrcLayerFrom2 = ldrillinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode).Skip(1).First().ProcLayerFrom;
                        tempTool.PrcLayerTo2 = ldrillinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode).Skip(1).First().ProcLayerTo;
                        tempTool.HoleCount2 = ldrillinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode).Skip(1).First().HoleCount;

                        tempTool.HoleCount = "CS:" + tempTool.HoleCount1 + " SS:" + tempTool.HoleCount2;
                    }

                    if (ldrillinfo.FindAll(f => f.ProcSeq == seq).Count() == 3)
                    {
                        tempTool.HoleCount = ldrillinfo.Where(t => t.ProcSeq == seq).Sum(s => Convert.ToInt32(s.HoleCount)).ToString().Trim();

                        tempTool.PrcLayerFrom1 = ldrillinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode && (t.LaserProcType == "" || t.LaserProcType.Contains("BVH"))).First().ProcLayerFrom;
                        tempTool.PrcLayerTo1 = ldrillinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode && (t.LaserProcType == "" || t.LaserProcType.Contains("BVH"))).First().ProcLayerTo;
                        tempTool.HoleCount1 = ldrillinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode && (t.LaserProcType == "" || t.LaserProcType.Contains("BVH"))).First().HoleCount;

                        tempTool.PrcLayerFrom2 = ldrillinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode && (t.LaserProcType == "" || t.LaserProcType.Contains("BVH"))).Skip(1).First().ProcLayerFrom;
                        tempTool.PrcLayerTo2 = ldrillinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode && (t.LaserProcType == "" || t.LaserProcType.Contains("BVH"))).Skip(1).First().ProcLayerTo;
                        tempTool.HoleCount2 = ldrillinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode && (t.LaserProcType == "" || t.LaserProcType.Contains("BVH"))).Skip(1).First().HoleCount;

                        tempTool.HoleCountPth = ldrillinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode && (t.LaserProcType.Contains("PTH"))).First().HoleCount;

                        tempTool.HoleCount = "CS:" + tempTool.HoleCount1 + " SS:" + tempTool.HoleCount2 + " PTH:" + tempTool.HoleCountPth;
                    }

                    tempTool.Depth = Convert.ToDecimal(ldrillinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode).First().ZDepth.Trim());


                    if (issample == true)
                    { tempTool.Sample = true; }
                    else if (issample != true)
                    { tempTool.Sample = false; }

                    var laserprctype = ldrillinfo.Where(t => t.ProcSeq == seq).GroupBy(x => x.LaserProcType).Select(k => k.Key).ToList<string>();

                    if (laserprctype.Count() == 1)
                    {
                        if (laserprctype.Contains("PTH"))
                        {
                            tempTool.PrcCode = "UV_SHT_DR_002";
                            tempTool.PrcName = "드릴(PTH)";
                        }

                        else if (laserprctype.Contains("BVH"))
                        {
                            tempTool.PrcCode = "UV_SHT_DR_001";
                            tempTool.PrcName = "드릴(BVH)";
                        }
                        else
                        {
                            switch (ldrillinfo.Where(t => t.ProcSeq == seq).First().ProcName)
                            {
                                case "F.L.D_Uv":
                                    tempTool.PrcCode = "UV_SHT_DR_002";
                                    tempTool.PrcName = "드릴(PTH)";
                                    break;
                                case "L.D_Uv":
                                    tempTool.PrcCode = "UV_SHT_DR_001";
                                    tempTool.PrcName = "드릴(BVH)";
                                    break;
                            }
                        }
                    }

                    if (laserprctype.Count() == 2)
                    {
                        tempTool.PrcCode = "UV_SHT_DR_002";
                    }



                    var thisyear = DateTime.Now.Year.ToString().Substring(2) + "-DEM-UV-";
                    var prdidNo = context.TbUvToolinfo.Where(x => x.ProductId.Contains(thisyear)).Count() + 1;

                    tempTool.ProductId = thisyear + prdidNo.ToString("D4");

                }
            }

            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

            return tempTool;
        }

        //바디컷 정보 조회
        private TbUvToolinfo GetTbUvToolinfo_DE_MS_lbcut(string tool, string workcenter, string lot, string seq, bool issample,string seqnr)
        {
            var tempTool = new TbUvToolinfo();

            try
            {
               
                using (var context = new Db_Uv_InventoryContext())

                {
                    var lotdetailinfo = _demsClient.MesLotDetailInfoQry(lot, tool);
                    var lotinfo = _demsClient.MesLotInfoQry(lot);
                    var specinfo = _demsClient.MesSpecInfoQry(tool, lotdetailinfo.Seqnr);
                    var lbcutinfo = _demsClient.MesLBodyCutInfoQry(tool,seqnr);

                    tempTool.CustId = "UV_01";
                    tempTool.CustName = "대덕전자(MS)";

                    tempTool.CustModelname = lotdetailinfo.ModelName;
                    tempTool.CustRevision = lotdetailinfo.ModelRev.Trim();
                    tempTool.CustToolno = lotdetailinfo.ToolNo;
                    tempTool.Layer = lotdetailinfo.LayerTotal;
                    tempTool.EndCustomer = lotdetailinfo.Kname;
                    tempTool.CustomerShipto = lotdetailinfo.Shipto;
                    tempTool.ProductType = lotdetailinfo.SpecType2;
                    tempTool.MesSeqCode = seq;
                    tempTool.MesPrcName = lbcutinfo.Where(t => t.ProcSeq == seq).First().ProcName;
                    tempTool.MesPrcCode = lbcutinfo.Where(t => t.ProcSeq == seq).First().ProcCode;

                    tempTool.StackType = lotdetailinfo.SpecType1;
                    tempTool.PrdCategory = lotdetailinfo.ProductType;
                    tempTool.LayerStructure = lotdetailinfo.LayerStructure;
                    tempTool.CustComment = lotdetailinfo.Reason;
                    tempTool.CreateDate = DateTime.ParseExact(lotdetailinfo.Okdat, "yyyyMMdd", null);

                    tempTool.ArrayBlk = specinfo.Arrayx * specinfo.Arrayy;
                    tempTool.WorksizeX = specinfo.Worksizex;
                    tempTool.WorksizeY = specinfo.Worksizey;
                    tempTool.Pcs = specinfo.Pcppanel;

                    tempTool.MainHoleSize = lbcutinfo.Where(t => t.ProcSeq == seq).First().HoleSize.Trim();

                    tempTool.PrcLayerFrom1 = lbcutinfo.Where(t => t.ProcSeq == seq).First().ProcLayerFrom;
                    tempTool.PrcLayerTo1 = lbcutinfo.Where(t => t.ProcSeq == seq).First().ProcLayerTo;
                    tempTool.HoleCount = "길이:" + lbcutinfo.Where(t => t.ProcSeq == seq).First().ProcDistance;
                    tempTool.Depth = Convert.ToDecimal(lbcutinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode).First().Depth.Trim());
                    tempTool.ToolNotes = "가공면:" + lbcutinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode).First().Side+" Tool순번:" + lbcutinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode).First().ToolSeq; ;

                    if (issample == true)
                    { tempTool.Sample = true; }
                    else if (issample != true)
                    { tempTool.Sample = false; }

                    tempTool.PrcCode = "UV_SHT_CUT_007";
                    tempTool.PrcName = "컷(BODY)";

                    var thisyear = DateTime.Now.Year.ToString().Substring(2) + "-DEM-UV-";
                    var prdidNo = context.TbUvToolinfo.Where(x => x.ProductId.Contains(thisyear)).Count() + 1;
                    tempTool.ProductId = thisyear + prdidNo.ToString("D4");
                }
            }

            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

            return tempTool;
        }
    }
}
