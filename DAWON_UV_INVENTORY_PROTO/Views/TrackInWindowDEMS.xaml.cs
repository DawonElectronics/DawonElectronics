using ConnectorDEMS;
using DAWON_UV_INVENTORY_PROTO.Models;
using DAWON_UV_INVENTORY_PROTO.ViewModels;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using AutoMapper;
using ConnectorDEMS.Models;
using ConnectorDEPKG.Models;
using Syncfusion.Data.Extensions;
using MesLotDetailInfo = ConnectorDEMS.Models.MesLotDetailInfo;

namespace DAWON_UV_INVENTORY_PROTO.Views
{
    /// <summary>
    /// Interaction logic for TrackInWindow.xaml
    /// </summary>
    ///
    public class AutoMapperProfileDeMs : Profile
    {

        public AutoMapperProfileDeMs()
        {
            //源类=>目标类
            //CreateMap<DogModel, Dog2Model>();

            //DataTable=>Model
            IMappingExpression<DataRow, DeMsRcvModelAfterValidation> mappingExpression;
            mappingExpression = CreateMap<DataRow, DeMsRcvModelAfterValidation>();
            mappingExpression.ForMember(d => d.IsRegist, o => o.MapFrom(s => s["IsRegist"]));
            mappingExpression.ForMember(d => d.Businessunit, o => o.MapFrom(s => s["Businessunit"]));
            mappingExpression.ForMember(d => d.Chk, o => o.MapFrom(s => s["Chk"]));
            mappingExpression.ForMember(d => d.Confirm, o => o.MapFrom(s => s["Confirm"]));
            mappingExpression.ForMember(d => d.ProductCode, o => o.MapFrom(s => s["ProductCode"]));
            mappingExpression.ForMember(d => d.Defectpcsqty, o => o.MapFrom(s => s["Defectpcsqty"]));
            mappingExpression.ForMember(d => d.Defectpnlqty, o => o.MapFrom(s => s["Defectpnlqty"]));
            mappingExpression.ForMember(d => d.Defectsqaremeter, o => o.MapFrom(s => s["Defectsqaremeter"]));
            mappingExpression.ForMember(d => d.Defectstripqty, o => o.MapFrom(s => s["Defectstripqty"]));
            mappingExpression.ForMember(d => d.Duedate, o => o.MapFrom(s => s["Duedate"]));
            mappingExpression.ForMember(d => d.FInsite, o => o.MapFrom(s => s["F_Insite"]));
            mappingExpression.ForMember(d => d.FromVendorId, o => o.MapFrom(s => s["FromVendorId"]));
            mappingExpression.ForMember(d => d.Fromlocation, o => o.MapFrom(s => s["Fromlocation"]));
            mappingExpression.ForMember(d => d.Ftrmi, o => o.MapFrom(s => s["Ftrmi"]));
            mappingExpression.ForMember(d => d.Hottype, o => o.MapFrom(s => s["Hottype"]));
            mappingExpression.ForMember(d => d.Hottype1, o => o.MapFrom(s => s["Hottype1"]));
            mappingExpression.ForMember(d => d.Inspectionmethod, o => o.MapFrom(s => s["Inspectionmethod"]));
            
            mappingExpression.ForMember(d => d.Ishold, o => o.MapFrom(s => s["Ishold"]));
            mappingExpression.ForMember(d => d.Isinspectionactualresult, o => o.MapFrom(s => s["Isinspectionactualresult"]));
            mappingExpression.ForMember(d => d.Isrepair, o => o.MapFrom(s => s["Isrepair"]));
            
            mappingExpression.ForMember(d => d.NInsite, o => o.MapFrom(s => s["N_Insite"]));
            mappingExpression.ForMember(d => d.ToVendorId, o => o.MapFrom(s => s["ToVendorId"]));
            mappingExpression.ForMember(d => d.Kname, o => o.MapFrom(s => s["Kname"]));
            mappingExpression.ForMember(d => d.Kunnr, o => o.MapFrom(s => s["Kunnr"]));
            mappingExpression.ForMember(d => d.Lotno, o => o.MapFrom(s => s["Lotno"]));
            mappingExpression.ForMember(d => d.Lastsendtime, o => o.MapFrom(s => s["Lastsendtime"]));
            mappingExpression.ForMember(d => d.Layer, o => o.MapFrom(s => s["Layer"]));
            mappingExpression.ForMember(d => d.Lotid, o => o.MapFrom(s => s["Lotid"]));
            mappingExpression.ForMember(d => d.Lotname, o => o.MapFrom(s => s["Lotname"]));
            mappingExpression.ForMember(d => d.Mesprocessstate, o => o.MapFrom(s => s["Mesprocessstate"]));
            mappingExpression.ForMember(d => d.Nextprocesssegmentid, o => o.MapFrom(s => s["Nextprocesssegmentid"]));
            mappingExpression.ForMember(d => d.Nextprocesssegmentname, o => o.MapFrom(s => s["Nextprocesssegmentname"]));
            mappingExpression.ForMember(d => d.Outsourcingcompany, o => o.MapFrom(s => s["Outsourcingcompany"]));
            mappingExpression.ForMember(d => d.Pannelqty, o => o.MapFrom(s => s["Pannelqty"]));
            mappingExpression.ForMember(d => d.Pieceqty, o => o.MapFrom(s => s["Pieceqty"]));
            mappingExpression.ForMember(d => d.ProcessState, o => o.MapFrom(s => s["ProcessState"]));
            mappingExpression.ForMember(d => d.Processsegment, o => o.MapFrom(s => s["Processsegment"]));
            mappingExpression.ForMember(d => d.Processsegmentname, o => o.MapFrom(s => s["Processsegmentname"]));
            mappingExpression.ForMember(d => d.Productdefinition, o => o.MapFrom(s => s["Productdefinition"]));
            mappingExpression.ForMember(d => d.Productrevision, o => o.MapFrom(s => s["Productrevision"]));
            mappingExpression.ForMember(d => d.Receivewaittime, o => o.MapFrom(s => s["Receivewaittime"]));
            mappingExpression.ForMember(d => d.Spectype2, o => o.MapFrom(s => s["Spectype2"]));
            mappingExpression.ForMember(d => d.Setlocation, o => o.MapFrom(s => s["Setlocation"]));
            mappingExpression.ForMember(d => d.Sqaremeter, o => o.MapFrom(s => s["Sqaremeter"]));
            mappingExpression.ForMember(d => d.State, o => o.MapFrom(s => s["State"]));
            mappingExpression.ForMember(d => d.Stripqty, o => o.MapFrom(s => s["Stripqty"]));
            mappingExpression.ForMember(d => d.Sublotmaterialid, o => o.MapFrom(s => s["Sublotmaterialid"]));
            mappingExpression.ForMember(d => d.Tolocation, o => o.MapFrom(s => s["Tolocation"]));
            mappingExpression.ForMember(d => d.Toolnumber, o => o.MapFrom(s => s["Toolnumber"]));
            mappingExpression.ForMember(d => d.Usr02, o => o.MapFrom(s => s["Usr02"]));
            mappingExpression.ForMember(d => d.Usr03, o => o.MapFrom(s => s["Usr03"]));
            mappingExpression.ForMember(d => d.Vtext, o => o.MapFrom(s => s["Vtext"]));
        }
    }
    public partial class TrackInWindowDems : ChromelessWindow
    {

        public TrackInWindowDemsViewModel TrackinDemsViewmodel = new TrackInWindowDemsViewModel();
        DemsHelper _demsClient = new DemsHelper();
        Regex _reDelot = new Regex(@".[0-9]{6}[-]?[0-9]{1}.[0-9]{2}.");
        private IMapper mapper;
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
        private bool GetRegist(string tool)
        {
            var registed = MainWindow._mainwindowViewModel.ToolInfos.Select(s => s.CustToolno).ToList<string>();
            //var registed2 = MainWindow._mainwindowViewModel.Customer.Select(s => s.CustName).ToList<string>();
            var result = registed.Contains(tool);
            return result;
        }

        public List<T> ReadData<T>(DataTable dt)
        {
            var configuration = new MapperConfiguration(a => { a.AddProfile(new AutoMapperProfileDeMs()); });
            mapper = configuration.CreateMapper();
            return mapper.Map<IEnumerable<DataRow>, List<T>>(dt.Rows.ToList<DataRow>());
        }
        private void cmb_input_segment_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            var rcvdt = de_ms_qry_rcv_lotlist(TrackinDemsViewmodel.WorkcenterId);

            rcvdt.Columns.Add("IsRegist");
            var toollist = rcvdt.AsEnumerable().Select(w => w.Field<string>("TOOLNUMBER")).ToList<string>();

            foreach (var item in toollist)
            {
                rcvdt.Select(string.Format("[TOOLNUMBER] = '{0}'", item)).ToList<DataRow>()
                    .ForEach(r => r["IsRegist"] = GetRegist(item));
            }
            TrackinDemsViewmodel.RcvLotList = ReadData<DeMsRcvModelAfterValidation>(rcvdt);
            //GridRcv.ItemsSource = rcvdt;

        }
        private void UpdateGridRcv()
        {

            //var rcvdt = de_ms_qry_rcv_lotlist(TrackinDemsViewmodel.WorkcenterId);

            //rcvdt.Columns.Add("IsRegist");

            //var toollist = rcvdt.AsEnumerable().Select(w => w.Field<string>("TOOLNUMBER")).ToList<string>();

            //foreach (var item in toollist)
            //{
            //    rcvdt.Select(string.Format("[TOOLNUMBER] = '{0}'", item)).ToList<DataRow>()
            //        .ForEach(r => r["IsRegist"] = GetRegist(item));
            //}

            //GridRcv.ItemsSource = rcvdt;

            var rcvdt = de_ms_qry_rcv_lotlist(TrackinDemsViewmodel.WorkcenterId);

            rcvdt.Columns.Add("IsRegist");
            var toollist = rcvdt.AsEnumerable().Select(w => w.Field<string>("TOOLNUMBER")).ToList<string>();

            foreach (var item in toollist)
            {
                rcvdt.Select(string.Format("[TOOLNUMBER] = '{0}'", item)).ToList<DataRow>()
                    .ForEach(r => r["IsRegist"] = GetRegist(item));
            }
            TrackinDemsViewmodel.RcvLotList = ReadData<DeMsRcvModelAfterValidation>(rcvdt);
            //GridRcv.ItemsSource = rcvdt;

        }
        private DataTable de_ms_qry_rcv_lotlist(string workcenter)
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
                var rcvlist = dsRcvlist.Tables[0];
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

                var tool = pC.GetValue(rowdata, "Toolnumber") as String;
                var lot = pC.GetValue(rowdata, "Lotid") as String;
                var prcname = pC.GetValue(rowdata, "Processsegmentname") as String;
                var workcenter = TrackinDemsViewmodel.WorkcenterId;
                var pnlqty = Convert.ToInt16(pC.GetValue(rowdata, "Pannelqty"));
                var lotdetailinfo = _demsClient.MesLotDetailInfoQry(lot, tool);
                var fromlayer = pC.GetValue(rowdata, "Usr02") as String;
                var user = MainWindow._mainwindowViewModel.UserList.Where(x => x.UserName == MainWindow._mainwindowViewModel.SelectedUser).First();
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
                    inputTemp.TrackinUserId = user.UserId;

                    //LOT입력, 필수항목으로 입력 여부 체크
                    inputTemp.Lotid = lot;

                    //툴입력, 필수항목으로 입력 여부 체크
                    string pid = string.Empty;
                    if (fromlayer == null)
                        pid = MainWindow._mainwindowViewModel.ToolInfos.Where(w => w.CustToolno == tool).Select(x => x.ProductId).FirstOrDefault();
                    else if (fromlayer != null)
                        pid = MainWindow._mainwindowViewModel.ToolInfos.Where(w => w.CustToolno == tool && w.PrcLayerFrom1 == fromlayer).Select(x => x.ProductId).FirstOrDefault();

                    inputTemp.ProductId = pid;
                    inputTemp.SampleOrder = issample;
                    inputTemp.Pnlqty = pnlqty;
                    inputTemp.CreateTime = DateTime.Now;
                    inputTemp.TrackinTime = DateTime.Now;
                    inputTemp.Txid = Guid.NewGuid();
                    var lotcount = context.TbUvWorkorder.Where(x => x.Lotid == lot && x.IsDone == false && x.ProductId == pid).Count();
                    if (lotcount == 0)
                    {
                        context.TbUvWorkorder.Add(inputTemp);
                        context.SaveChanges();
                        ExecuteRcv(lot);
                    }
                    else if (lotcount != 0)
                    {
                        if (MessageBox.Show("중복된 LOT가 있습니다 추가하시겠습니까?", "중복 LOT 확인", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            context.TbUvWorkorder.Add(inputTemp);
                            context.SaveChanges();
                            ExecuteRcv(lot);
                        }
                        else
                        {
                            ExecuteRcv(lot);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            UpdateGridRcv();
            UpdateFiltered_WorkorderList();
        }


        private void BtnAddOnly_OnClick(object sender, RoutedEventArgs e)
        {
            BtnExeRcv.IsEnabled = false;
            BtnAddOnly.IsEnabled = false;
            var selectedlist = GridRcv.SelectionController.SelectedRows;
            var rcvlist = new List<string>();

            string lot = string.Empty;
            var tool_prcname = string.Empty;
            var user = MainWindow._mainwindowViewModel.UserList.Where(x => x.UserName == MainWindow._mainwindowViewModel.SelectedUser).First();
            using (var context = new Db_Uv_InventoryContext())
            {
                foreach (var item in selectedlist)
                {
                    try
                    {
                        var rowdata = item.RowData;
                        var pC = GridRcv.View.GetPropertyAccessProvider();
                        var tool = pC.GetValue(rowdata, "Toolnumber") as String;
                        lot = pC.GetValue(rowdata, "Lotid") as String;
                        var prcname = pC.GetValue(rowdata, "Processsegmentname") as String;
                        var workcenter = TrackinDemsViewmodel.WorkcenterId;
                        var pnlqty = Convert.ToInt16(pC.GetValue(rowdata, "Pannelqty"));
                        var fromlayer = pC.GetValue(rowdata, "Usr02") as String;
                        var lotdetailinfo = new MesLotDetailInfo();
                        bool issample = false;

                        if (Char.IsLetter(lot, 0) && (!lot.ToLower().StartsWith("p") || !lot.ToLower().StartsWith("w")))
                        {
                            issample = true;
                        }

                        if (tool_prcname != tool + prcname)
                        {
                            lotdetailinfo = _demsClient.MesLotDetailInfoQry(lot, tool);
                            if (!IsToolExist(tool, prcname))
                            {
                                RegistTool(tool, workcenter, lot, issample, lotdetailinfo.Seqnr);
                            }
                        }

                        tool_prcname = tool + prcname;

                        var inputTemp = new TbUvWorkorder();
                        //고객사 선택, 필수항목으로 입력 여부 체크

                        var selcust = "대덕전자(MS)";
                        inputTemp.CustId = MainWindow._mainwindowViewModel.ToolInfos.Where(x => x.CustName == selcust)
                            .FirstOrDefault().CustId;

                        //작성자 선택, 필수항목으로 입력 여부 체크
                        inputTemp.TrackinUserId = user.UserId;

                        //LOT입력, 필수항목으로 입력 여부 체크
                        inputTemp.Lotid = lot;

                        //툴입력, 필수항목으로 입력 여부 체크                
                        string pid = string.Empty;
                        if (fromlayer == null)
                            pid = MainWindow._mainwindowViewModel.ToolInfos.Where(w => w.CustToolno == tool)
                                .Select(x => x.ProductId).FirstOrDefault();
                        else if (fromlayer != null)
                            pid = MainWindow._mainwindowViewModel.ToolInfos
                                .Where(w => w.CustToolno == tool && w.PrcLayerFrom1 == fromlayer)
                                .Select(x => x.ProductId).FirstOrDefault();

                        inputTemp.ProductId = pid;
                        inputTemp.SampleOrder = issample;
                        inputTemp.Pnlqty = pnlqty;
                        inputTemp.CreateTime = DateTime.Now;
                        inputTemp.TrackinTime = DateTime.Now;
                        inputTemp.Txid = Guid.NewGuid();
                        var lotcount = context.TbUvWorkorder
                            .Where(x => x.Lotid == lot && x.IsDone == false && x.ProductId == pid).Count();
                        if (lotcount == 0)
                        {
                            context.TbUvWorkorder.Add(inputTemp);
                            context.SaveChanges();
                            //ExecuteRcv(lot);
                            rcvlist.Add(lot);
                        }
                        else if (lotcount != 0)
                        {
                            if (MessageBox.Show("중복된 LOT가 있습니다 추가하시겠습니까?", "중복 LOT 확인", MessageBoxButton.YesNo) ==
                                MessageBoxResult.Yes)
                            {
                                context.TbUvWorkorder.Add(inputTemp);
                                context.SaveChanges();
                                rcvlist.Add(lot);
                            }
                            else if (MessageBox.Show("중복된 LOT가 있습니다 추가하시겠습니까?", "중복 LOT 확인", MessageBoxButton.YesNo) ==
                                     MessageBoxResult.No)
                            {
                                rcvlist.Add(lot);
                            }
                        }

                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }

            UpdateGridRcv();
            UpdateFiltered_WorkorderList();
            BtnExeRcv.IsEnabled = true;
            BtnAddOnly.IsEnabled = true;
        }

        //여러로트 받기
        private void btn_exe_rcv_Click(object sender, RoutedEventArgs e)
        {
            BtnExeRcv.IsEnabled = false;
            BtnAddOnly.IsEnabled = false;
            var selectedlist = GridRcv.SelectionController.SelectedRows;
            var rcvlist = new List<string>();
            string lot = string.Empty;
            var tool_prcname = string.Empty;
            var user = MainWindow._mainwindowViewModel.UserList.Where(x => x.UserName == MainWindow._mainwindowViewModel.SelectedUser).First();
            using (var context = new Db_Uv_InventoryContext())
            {
                foreach (var item in selectedlist)
                {
                    try
                    {
                        var rowdata = item.RowData;
                        var pC = GridRcv.View.GetPropertyAccessProvider();
                        var tool = pC.GetValue(rowdata, "Toolnumber") as String;
                        lot = pC.GetValue(rowdata, "Lotid") as String;
                        var prcname = pC.GetValue(rowdata, "Processsegmentname") as String;
                        var workcenter = TrackinDemsViewmodel.WorkcenterId;
                        var pnlqty = Convert.ToInt16(pC.GetValue(rowdata, "Pannelqty"));
                        var fromlayer = pC.GetValue(rowdata, "Usr02") as String;

                        var lotdetailinfo = new MesLotDetailInfo();
                        bool issample = false;
                        if (Char.IsLetter(lot, 0) && (!lot.ToLower().StartsWith("p") || !lot.ToLower().StartsWith("w")))
                        {
                            issample = true;
                        }

                        if (tool_prcname != tool + prcname)
                        {
                            lotdetailinfo = _demsClient.MesLotDetailInfoQry(lot, tool);
                            if (!IsToolExist(tool, prcname))
                            {
                                RegistTool(tool, workcenter, lot, issample, lotdetailinfo.Seqnr);
                            }
                        }

                        tool_prcname = tool + prcname;

                        var inputTemp = new TbUvWorkorder();
                        //고객사 선택, 필수항목으로 입력 여부 체크

                        var selcust = "대덕전자(MS)";
                        inputTemp.CustId = MainWindow._mainwindowViewModel.ToolInfos.Where(x => x.CustName == selcust)
                            .FirstOrDefault().CustId;


                        //작성자 선택, 필수항목으로 입력 여부 체크
                        inputTemp.TrackinUserId = user.UserId;

                        //LOT입력, 필수항목으로 입력 여부 체크
                        inputTemp.Lotid = lot;

                        //툴입력, 필수항목으로 입력 여부 체크                
                        string pid = string.Empty;
                        if (fromlayer == null)
                            pid = MainWindow._mainwindowViewModel.ToolInfos.Where(w => w.CustToolno == tool)
                                .Select(x => x.ProductId).FirstOrDefault();
                        else if (fromlayer != null)
                            pid = MainWindow._mainwindowViewModel.ToolInfos
                                .Where(w => w.CustToolno == tool && w.PrcLayerFrom1 == fromlayer)
                                .Select(x => x.ProductId).FirstOrDefault();

                        inputTemp.ProductId = pid;
                        inputTemp.SampleOrder = issample;
                        inputTemp.Pnlqty = pnlqty;
                        inputTemp.CreateTime = DateTime.Now;
                        inputTemp.TrackinTime = DateTime.Now;
                        inputTemp.Txid = Guid.NewGuid();
                        var lotcount = context.TbUvWorkorder
                            .Where(x => x.Lotid == lot && x.IsDone == false && x.ProductId == pid).Count();
                        if (lotcount == 0)
                        {
                            context.TbUvWorkorder.Add(inputTemp);
                            context.SaveChanges();
                            rcvlist.Add(lot);
                        }
                        else if (lotcount != 0)
                        {
                            if (MessageBox.Show("중복된 LOT가 있습니다 추가하시겠습니까?", "중복 LOT 확인", MessageBoxButton.YesNo) ==
                                MessageBoxResult.Yes)
                            {
                                context.TbUvWorkorder.Add(inputTemp);
                                context.SaveChanges();
                                rcvlist.Add(lot);
                            }
                            else if (MessageBox.Show("중복된 LOT가 있습니다 추가하시겠습니까?", "중복 LOT 확인", MessageBoxButton.YesNo) ==
                                     MessageBoxResult.No)
                            {
                                rcvlist.Add(lot);
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            if (rcvlist.Count == 1)
                ExecuteRcv(lot);
            else if (rcvlist.Count > 1)
                ExecuteMultiRcv(rcvlist);

            UpdateGridRcv();
            UpdateFiltered_WorkorderList();
            BtnExeRcv.IsEnabled = true;
            BtnAddOnly.IsEnabled = true;
        }
        public void UpdateFiltered_WorkorderList()
        {
            using (var db = new Db_Uv_InventoryContext())
            {
                var customer = MainWindow._mainwindowViewModel.SelectedCustomerWo;
                var issample = false;
                if (MainWindow._mainwindowViewModel.SelectedIsSampleWo == "양산") issample = false;
                else if (MainWindow._mainwindowViewModel.SelectedIsSampleWo == "샘플") issample = true;
                MainWindow._mainwindowViewModel.WorkOrderList =
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

                var resultLotlist = client.ExecCommandAsync(qryLotlistMsg);
                //var result_lotlist = client.ExecCommandAsync(new MesClient_DE_MS.ExecCommandRequest(qry_lotlist_msg));
                resultRcvXml.LoadXml(resultLotlist.Result.OBJECT.ToString());

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

        private bool IsToolExist(string tool, string prcname)
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

        private void RegistTool(string tool, string workcenter, string lot, bool issample, string seqnr)
        {
            try
            {
                using (var context = new Db_Uv_InventoryContext())
                {
                    var tools = context.TbUvToolinfo.Where(w => w.CustToolno == tool);
                    var ldrillinfoSeq = _demsClient.MesHoleInfoQry(tool).Where(x => x.ProcName.ToLower().Contains("uv")).GroupBy(x => x.ProcSeq).Select(x => x.Key).ToList<string>();
                    var lbcutinfoSeq = _demsClient.MesLBodyCutInfoQry(tool, seqnr).GroupBy(x => x.ProcSeq).Select(x => x.Key).ToList<string>();

                    foreach (var seq in ldrillinfoSeq)
                    {
                        if (seq != "")
                        {
                            if (tools.Where(x => x.MesSeqCode == seq).Count() == 0)
                            {
                                var tempTool = GetTbUvToolinfo_DE_MS(tool, workcenter, lot, seq, issample);
                                context.TbUvToolinfo.AddAsync(tempTool);
                                context.SaveChanges();
                                MainWindow._mainwindowViewModel.ToolInfos =
                                    new List<TbUvToolinfo>(context.TbUvToolinfo);
                            }

                            else if (tools.Where(x => x.MesSeqCode == seq).Count() > 0)
                            {
                                continue;
                            }
                        }
                    }

                    foreach (var seq in lbcutinfoSeq)
                    {
                        if (seq != "")
                        {
                            if (tools.Where(x => x.MesSeqCode == seq).Count() == 0)
                            {
                                var tempTool = GetTbUvToolinfo_DE_MS_lbcut(tool, workcenter, lot, seq, issample, seqnr);
                                context.TbUvToolinfo.AddAsync(tempTool);
                                context.SaveChanges();
                                MainWindow._mainwindowViewModel.ToolInfos = new List<TbUvToolinfo>(context.TbUvToolinfo);
                            }

                            else if (tools.Where(x => x.MesSeqCode == seq).Count() > 0)
                            {
                                continue;
                            }

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
                    tempTool.CreateDate = lotdetailinfo.Okdat;

                    tempTool.ArrayBlk = specinfo.Arrayx * specinfo.Arrayy;
                    tempTool.WorksizeX = specinfo.Worksizex;
                    tempTool.WorksizeY = specinfo.Worksizey;
                    tempTool.Pcs = specinfo.Pcppanel;

                    tempTool.MainHoleSize = ldrillinfo.Where(t => t.ProcSeq == seq).First().HoleSize.Trim();

                    if (ldrillinfo.FindAll(f => f.ProcSeq == seq).Count() == 1)
                    {
                        if (ldrillinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode && (t.LaserProcType.Contains("PTH")))
                            .Any())
                        {
                            tempTool.PrcLayerFrom1 = ldrillinfo.Where(t => t.ProcSeq == seq).First().ProcLayerFrom;
                            tempTool.PrcLayerTo1 = ldrillinfo.Where(t => t.ProcSeq == seq).First().ProcLayerTo;
                            tempTool.HoleCountPth = ldrillinfo.Where(t => t.ProcSeq == seq).First().HoleCount;
                            tempTool.HoleCount = "PTH:" + tempTool.HoleCountPth;
                        }
                        else
                        {
                            tempTool.PrcLayerFrom1 = ldrillinfo.Where(t => t.ProcSeq == seq).First().ProcLayerFrom;
                            tempTool.PrcLayerTo1 = ldrillinfo.Where(t => t.ProcSeq == seq).First().ProcLayerTo;
                            tempTool.HoleCount1 = ldrillinfo.Where(t => t.ProcSeq == seq).First().HoleCount;
                            tempTool.HoleCount = "CS:" + tempTool.HoleCount1;
                        }
                        
                    }

                    else if (ldrillinfo.FindAll(f => f.ProcSeq == seq).Count() == 2)
                    {
                        tempTool.PrcLayerFrom1 = ldrillinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode).First().ProcLayerFrom;
                        tempTool.PrcLayerTo1 = ldrillinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode).First().ProcLayerTo;
                        tempTool.HoleCount1 = ldrillinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode).First().HoleCount;

                        tempTool.PrcLayerFrom2 = ldrillinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode).Skip(1).First().ProcLayerFrom;
                        tempTool.PrcLayerTo2 = ldrillinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode).Skip(1).First().ProcLayerTo;
                        tempTool.HoleCount2 = ldrillinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode).Skip(1).First().HoleCount;

                        tempTool.HoleCount = "CS:" + tempTool.HoleCount1 + " SS:" + tempTool.HoleCount2;
                    }

                    else if (ldrillinfo.FindAll(f => f.ProcSeq == seq).Count() == 3)
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
                        if (laserprctype[0].Contains("PTH"))
                        {
                            tempTool.PrcCode = "UV_SHT_DR_002";
                            tempTool.PrcName = "드릴(PTH)";
                        }

                        else if (laserprctype[0].Contains("BVH"))
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
        private TbUvToolinfo GetTbUvToolinfo_DE_MS_lbcut(string tool, string workcenter, string lot, string seq, bool issample, string seqnr)
        {
            var tempTool = new TbUvToolinfo();

            //try
            //{

            using (var context = new Db_Uv_InventoryContext())

            {
                var lotdetailinfo = _demsClient.MesLotDetailInfoQry(lot, tool);
                var lotinfo = _demsClient.MesLotInfoQry(lot);
                var specinfo = _demsClient.MesSpecInfoQry(tool, lotdetailinfo.Seqnr);
                var lbcutinfo = _demsClient.MesLBodyCutInfoQry(tool, seqnr);

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
                tempTool.CreateDate = lotdetailinfo.Okdat;

                tempTool.ArrayBlk = specinfo.Arrayx * specinfo.Arrayy;
                tempTool.WorksizeX = specinfo.Worksizex;
                tempTool.WorksizeY = specinfo.Worksizey;
                tempTool.Pcs = specinfo.Pcppanel;

                tempTool.MainHoleSize = lbcutinfo.Where(t => t.ProcSeq == seq).First().HoleSize.Trim();

                tempTool.PrcLayerFrom1 = lbcutinfo.Where(t => t.ProcSeq == seq).First().ProcLayerFrom;
                tempTool.PrcLayerTo1 = lbcutinfo.Where(t => t.ProcSeq == seq).First().ProcLayerTo;
                tempTool.HoleCount = "길이:" + lbcutinfo.Where(t => t.ProcSeq == seq).First().ProcDistance;
                tempTool.Depth = Convert.ToDecimal(lbcutinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode).First().Depth.Trim());
                tempTool.ToolNotes = "가공면:" + lbcutinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode).First().Side + " Tool순번:" + lbcutinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode).First().ToolSeq; ;

                if (issample == true)
                { tempTool.Sample = true; }
                else if (issample != true)
                { tempTool.Sample = false; }

                tempTool.PrcCode = "UV_SHT_CUT_007";
                tempTool.PrcName = "컷(BODY)";

                var thisyear = DateTime.Now.Year.ToString().Substring(2) + "-DEM-UV-";
                //var prdidNo = context.TbUvToolinfo.Where(x => x.ProductId.Contains(thisyear)).Count() + 1;
                var prdidNo = context.TbUvToolinfo.Where(x => x.ProductId.Contains(thisyear)).OrderBy(s => s.ProductId).Select(s => Convert.ToInt16(s.ProductId.Substring(10, 4))).LastOrDefault() + 1;
                tempTool.ProductId = thisyear + prdidNo.ToString("D4");
            }
            //}

            //catch (Exception ex)
            //{ MessageBox.Show(ex.Message); }

            return tempTool;
        }
        private void TboxLot_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (_reDelot.IsMatch(e.Key.ToString())) e.Handled = true;
            var lot = _reDelot.Match(TboxLot.Text).Value;

            foreach (var item in TrackinDemsViewmodel.RcvLotList)
            {
                var record = item;

                if (record.Lotid == lot || record.Lotno == lot)
                {
                    GridRcv.SelectedItems.Add(item);
                    TboxLot.Text = string.Empty;
                    TboxLot.Focus();
                    break;
                }

            }
        }


    }
}
