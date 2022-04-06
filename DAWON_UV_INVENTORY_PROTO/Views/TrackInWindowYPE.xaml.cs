using CIM.MES.Common.Data;
using ConnectorDEPKG;
using ConnectorDEPKG.Models;
using ConnectorDEPKG.RuleServiceOI;
using ConnectorYPE;
using DAWON_UV_INVENTORY_PROTO.Models;
using DAWON_UV_INVENTORY_PROTO.ViewModels;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ConnectorYPE.Models;
using DataRow = System.Data.DataRow;

namespace DAWON_UV_INVENTORY_PROTO.Views
{
    /// <summary>
    /// Interaction logic for TrackInWindow.xaml
    /// </summary>
    public partial class TrackInWindowYPE : ChromelessWindow
    {

        public TrackInWindowYPEViewModel TrackinYpeViewmodel = new TrackInWindowYPEViewModel();
        YpeHelper ypeHelper = new YpeHelper();
        private List<string> notRegistedTool = new List<string>();
        Regex prcnamereg = new Regex(@"(\d+.*?차)");
        public TrackInWindowYPE()
        {

            InitializeComponent();
            
            this.DataContext = TrackinYpeViewmodel;

            UpdateGridRcv();
        }

        private bool GetRegist(string tool, string rev, string mesprccode)
        {
            var registed = MainWindow._mainwindowViewModel.ToolInfos.Where(x=>x.CustName=="영풍전자").Select(s => new { s.CustToolno, s.CustRevision, s.MesPrcCode,s.YpeDatarev });
            //var registed2 = MainWindow._mainwindowViewModel.Customer.Select(s => s.CustName).ToList<string>();
            var result = registed.Where(x=>x.CustToolno == tool && x.CustRevision == rev && x.MesPrcCode== mesprccode).Any();
            return result;
        }
       

        private async void UpdateGridRcv()
        {

            var rcvdt = await ypeHelper.QryWipDT();
            //var rcvlist = await ypeHelper.QryWipList();

            rcvdt.Columns.Add("IsRegist");

            var lotlist = rcvdt.AsEnumerable().Select(w => w.Field<string>("lotid")).ToList<string>();

            foreach (var item in lotlist)
            {
                rcvdt.Select(string.Format("[lotid] = '{0}'", item)).ToList<DataRow>()
                    .ForEach(r => r["IsRegist"] = GetRegist(r["processdefid"].ToString(), r["productrevision"].ToString(), r["processsegmentid"].ToString()));
            }

            GridRcv.ItemsSource = rcvdt;


        }

        private async void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            var itemafter = new YPToolInfo();
            var rowdata = GridRcv.SelectedItem;
            var pC = GridRcv.View.GetPropertyAccessProvider();

            var tool = pC.GetValue(rowdata, "processdefid").ToString();
            var lot = pC.GetValue(rowdata, "lotid").ToString();
            var subrev = pC.GetValue(rowdata, "productrevision").ToString();
            var prccode = pC.GetValue(rowdata, "processsegmentid").ToString();
            var pnlqty = Convert.ToInt16(pC.GetValue(rowdata, "wiptotalpnl"));            
            var issample = (pC.GetValue(rowdata, "lottype").ToString() == "양산") ? false:true ;
            var user = MainWindow._mainwindowViewModel.UserList.Where(x => x.UserName == MainWindow._mainwindowViewModel.SelectedUser).First();

            var yptoollist = await ypeHelper.QryYPToolinfoList(tool, subrev);

            var materialinfolist = await ypeHelper.QryMaterialInfo(tool,subrev);

            using var db = new Db_Uv_InventoryContext();
            {
                foreach (var item in materialinfolist)
                {
                    var tmp_yp_bom = new TbBomYpeMaterial();
                    tmp_yp_bom.Arraypcs = item.arraypcs;
                    tmp_yp_bom.Worksurface = item.worksurface;
                    tmp_yp_bom.Workmethod = item.workmethod;
                    tmp_yp_bom.Assemblyqty = item.assemblyqty;
                    tmp_yp_bom.Assemblyitemversion = item.assemblyitemversion;
                    tmp_yp_bom.Assemblyitembomid = item.assemblyitembomid;
                    tmp_yp_bom.Assemblyitemid = item.assemblyitemid;
                    tmp_yp_bom.Botassemblyitemname = item.botassemblyitemname;
                    tmp_yp_bom.Calculatepcs = item.calculatepcs;
                    tmp_yp_bom.Bomid = item.bomid;
                    tmp_yp_bom.Parentbomid = item.parentbomid;
                    tmp_yp_bom.ParentsAssemblyitemid = item.parents_assemblyitemid;
                    tmp_yp_bom.ParentsAssemblyitemversion = item.parents_assemblyitemversion;
                    tmp_yp_bom.RootAssemblyitemid = item.root_assemblyitemid;
                    tmp_yp_bom.RootAssemblyitemversion = item.root_assemblyitemversion;
                    tmp_yp_bom.RootBomid = item.root_bomid;
                    tmp_yp_bom.Color = item.color;
                    tmp_yp_bom.Plantid = item.plantid;
                    tmp_yp_bom.Userlayer = item.userlayer;
                    tmp_yp_bom.Usersequence = item.usersequence;
                    tmp_yp_bom.Spec = item.spec;
                    tmp_yp_bom.Consumabletype = item.consumabletype;
                    tmp_yp_bom.Consumabletype2 = item.consumabletype2;
                    tmp_yp_bom.Inktype = item.inktype;
                    tmp_yp_bom.Lvl = item.lvl;
                    tmp_yp_bom.Maker = item.maker;
                    tmp_yp_bom.Pnlsize = item.pnlsize;
                    tmp_yp_bom.Pnlsizexaxis = item.pnlsizexaxis;
                    tmp_yp_bom.Pnlsizeyaxis = item.pnlsizeyaxis;
                    tmp_yp_bom.Requirementqty = item.requirementqty;

                    db.Add(tmp_yp_bom);
                }

                for (int i = 0; i < yptoollist.Count; i++)
                {
                    YPToolInfo beforeitem = new YPToolInfo();
                    bool first = false;
                    bool last = false;
                    bool linkedprc = false;
                    if (i == 0)
                    { first = true; }
                    else
                    {
                        beforeitem = yptoollist[i - 1];
                        first = false;
                    }

                    if (i == yptoollist.Count - 1)
                    { last = true;}

                    if (first && !last)
                    {
                        var before = Convert.ToInt16(yptoollist[i].MesSeqCode);
                        var after  = Convert.ToInt16(yptoollist[i+1].MesSeqCode);

                        if (after - before == 10)
                        {
                            linkedprc = true;
                        }
                    }

                    if (linkedprc)
                    {
                        var item = yptoollist[i];
                        itemafter = yptoollist[i+1];
                        var tmptool = new TbUvToolinfo();

                        var thisyear = DateTime.Now.Year.ToString().Substring(2) + "-YPE-UV-";
                        var prdidNo = db.TbUvToolinfo.Where(x => x.ProductId.Contains(thisyear)).Count() + 1;

                        tmptool.ProductId = thisyear + prdidNo.ToString("D4");


                        
                        tmptool.CustToolno = tool;
                        tmptool.CustModelname = item.CustModelname;
                        tmptool.CustRevision = item.MesRevision;
                        tmptool.YpeDatarev = item.DataRevision;
                        tmptool.CreateDate = item.CreateDate;
                        tmptool.CustName = "영풍전자";
                        tmptool.CustId = "UV_03";
                        tmptool.PrdCategory = item.PrdCategory;
                        tmptool.Sample = issample;
                        tmptool.WorksizeX = Convert.ToDecimal(item.WorksizeX);
                        tmptool.WorksizeY = Convert.ToDecimal(item.WorksizeY);
                        tmptool.ArrayBlk = Convert.ToInt16(item.ArrayBlk.Replace(".0", ""));
                        tmptool.Pcs = Convert.ToInt16(item.Pcs.Replace(".0",""));
                        tmptool.Layer = Convert.ToInt16(item.Layer);
                        tmptool.MesSeqCode = item.MesSeqCode + "/" + itemafter.MesSeqCode;
                        tmptool.MesPrcCode = item.MesPrcCode + "/" + itemafter.MesPrcCode;
                        tmptool.MesPrcName = item.MesPrcName + "/" + prcnamereg.Match(itemafter.MesPrcName).Groups[1].Value;
                        tmptool.CustomerShipto = item.EndCustomer;
                        tmptool.EndCustomer = item.EndCustomer;
                        tmptool.ProductType = item.ProductType;
                        tmptool.ToolNotes = item.ToolNotes+"/" + itemafter.ToolNotes;
                        tmptool.MainHoleSize = item.MainHoleSize;
                        tmptool.PrcLayerFrom1 = item.PrcLayerFrom1;
                        tmptool.PrcLayerTo1 = item.PrcLayerTo1;
                        tmptool.PrcLayerFrom2 = itemafter.PrcLayerFrom1;
                        tmptool.PrcLayerTo2 = itemafter.PrcLayerTo1;
                        tmptool.InsulInfo = item.InsulInfo;
                        //tmptool.InsulThickness = item.InsulInfo;
                        //tmptool.CuThickness = item.InsulInfo;
                        tmptool.CamFinished = false;

                        if (item.MesPrcCode.StartsWith("20161"))
                        {
                            tmptool.PrcName = "RTR(BVH)";
                            tmptool.PrcCode = "UV_RTR_DR_001";
                        }
                        else if (item.MesPrcCode.StartsWith("20160"))
                        {
                            tmptool.PrcName = "드릴(BVH)";
                            tmptool.PrcCode = "UV_SHT_DR_001";
                        }
                        else if (item.MesPrcCode.StartsWith("20240"))
                        {
                            tmptool.PrcName = "컷(PI)";
                            tmptool.PrcCode = "UV_SHT_CUT_006";
                        }
                        else if (item.MesPrcCode.StartsWith("20200"))
                        {
                            tmptool.PrcName = "부자재컷(BASE)";
                            tmptool.PrcCode = "UV_SHT_CUT_001";
                        }
                        else if (item.MesPrcCode.StartsWith("20210"))
                        {
                            tmptool.PrcName = "부자재컷(CL)";
                            tmptool.PrcCode = "UV_SHT_CUT_002";
                        }
                        else if (item.MesPrcCode.StartsWith("20230"))
                        {
                            tmptool.PrcName = "부자재컷(PP)";
                            tmptool.PrcCode = "UV_SHT_CUT_003";
                        }
                        else if (item.MesPrcCode.StartsWith("20220"))
                        {
                            tmptool.PrcName = "부자재컷(BS)";
                            tmptool.PrcCode = "UV_SHT_CUT_004";
                        }
                        else if (item.MesPrcCode.StartsWith("20280"))
                        {
                            tmptool.PrcName = "컷(BODY)";
                            tmptool.PrcCode = "UV_SHT_CUT_007";
                        }
                        db.Add(tmptool);
                        db.SaveChanges();
                    }
                    else if(!linkedprc && itemafter.MesSeqCode == yptoollist[i].MesSeqCode){continue;}

                    else
                    {
                        var item = yptoollist[i];
                        var tmptool = new TbUvToolinfo();

                        var thisyear = DateTime.Now.Year.ToString().Substring(2) + "-YPE-UV-";
                        var prdidNo = db.TbUvToolinfo.Where(x => x.ProductId.Contains(thisyear)).Count() + 1;

                        tmptool.ProductId = thisyear + prdidNo.ToString("D4");

                        tmptool.CustToolno = tool;
                        tmptool.CustModelname = item.CustModelname;
                        tmptool.CustRevision = item.MesRevision;
                        tmptool.YpeDatarev = item.DataRevision;
                        tmptool.CreateDate = item.CreateDate;
                        tmptool.CustName = "영풍전자";
                        tmptool.CustId = "UV_03";
                        tmptool.PrdCategory = item.PrdCategory;
                        tmptool.Sample = issample;
                        tmptool.WorksizeX = Convert.ToDecimal(item.WorksizeX);
                        tmptool.WorksizeY = Convert.ToDecimal(item.WorksizeY);
                        tmptool.ArrayBlk = Convert.ToInt16(item.ArrayBlk.Replace(".0", ""));
                        tmptool.Pcs = Convert.ToInt16(item.Pcs.Replace(".0",""));
                        tmptool.Layer = Convert.ToInt16(item.Layer);
                        tmptool.MesSeqCode = item.MesSeqCode;
                        tmptool.MesPrcCode = item.MesPrcCode;
                        tmptool.MesPrcName = item.MesPrcName;
                        tmptool.CustomerShipto = item.EndCustomer;
                        tmptool.EndCustomer = item.EndCustomer;
                        tmptool.ProductType = item.ProductType;
                        
                        tmptool.ToolNotes = item.ToolNotes;
                        tmptool.MainHoleSize = item.MainHoleSize;
                        tmptool.PrcLayerFrom1 = item.PrcLayerFrom1;
                        tmptool.PrcLayerTo1 = item.PrcLayerTo1;
                        tmptool.InsulInfo = item.InsulInfo;
                        //tmptool.InsulThickness = item.InsulInfo;
                        //tmptool.CuThickness = item.InsulInfo;
                        tmptool.CamFinished = false;

                        if (item.MesPrcCode.StartsWith("20161"))
                        {
                            tmptool.PrcName = "RTR(BVH)";
                            tmptool.PrcCode = "UV_RTR_DR_001";
                        }
                        else if (item.MesPrcCode.StartsWith("20160"))
                        {
                            tmptool.PrcName = "드릴(BVH)";
                            tmptool.PrcCode = "UV_SHT_DR_001";
                        }
                        else if (item.MesPrcCode.StartsWith("20240"))
                        {
                            tmptool.PrcName = "컷(PI)";
                            tmptool.PrcCode = "UV_SHT_CUT_006";
                        }
                        else if (item.MesPrcCode.StartsWith("20200"))
                        {
                            tmptool.PrcName = "부자재컷(BASE)";
                            tmptool.PrcCode = "UV_SHT_CUT_001";
                        }
                        else if (item.MesPrcCode.StartsWith("20210"))
                        {
                            tmptool.PrcName = "부자재컷(CL)";
                            tmptool.PrcCode = "UV_SHT_CUT_002";
                        }
                        else if (item.MesPrcCode.StartsWith("20230"))
                        {
                            tmptool.PrcName = "부자재컷(PP)";
                            tmptool.PrcCode = "UV_SHT_CUT_003";
                        }
                        else if (item.MesPrcCode.StartsWith("20220"))
                        {
                            tmptool.PrcName = "부자재컷(BS)";
                            tmptool.PrcCode = "UV_SHT_CUT_004";
                        }
                        else if (item.MesPrcCode.StartsWith("20280"))
                        {
                            tmptool.PrcName = "컷(BODY)";
                            tmptool.PrcCode = "UV_SHT_CUT_007";
                        }
                        db.Add(tmptool);
                        db.SaveChanges();
                    }
                    

                }

                


            }
            
            
        }

        //private void grid_rcv_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    try
        //    {
        //        var rowdata = GridRcv.SelectedItem;
        //        var pC = GridRcv.View.GetPropertyAccessProvider();

        //        var tool = pC.GetValue(rowdata, "SPECNR") as String;
        //        var lot = pC.GetValue(rowdata, "LOTID") as String;

        //        var pnlqty = Convert.ToInt16(pC.GetValue(rowdata, "PannelQty"));
        //        var lotdetailinfo = _depkgHelper.MesLotDetailInfoQry(lot, tool);
        //        bool issample = Char.IsLetter(lot, 0) && !lot.ToLower().StartsWith("p");
        //        var user = MainWindow._mainwindowViewModel.UserList.Where(x => x.UserName == MainWindow._mainwindowViewModel.SelectedUser).First();
        //        if (!IsToolExist(tool))
        //        {
        //            RegistTool(tool, lot, issample);
        //        }
        //        using (var context = new Db_Uv_InventoryContext())
        //        {
        //            var inputTemp = new TbUvWorkorder();

        //            //고객사 선택, 필수항목으로 입력 여부 체크

        //            var cust = new TbCustomer();
        //            var selcust = "대덕전자(PKG)";
        //            cust = context.TbCustomer.Where(x => x.CustName == selcust).FirstOrDefault();
        //            inputTemp.CustId = cust.CustId;

        //            //작성자 선택, 필수항목으로 입력 여부 체크

        //            var usr = new TbUsers();
        //            var seluser = MainWindow._mainwindowViewModel.SelectedUser;
        //            usr = context.TbUsers.Where(x => x.UserName == seluser).FirstOrDefault();
        //            inputTemp.TrackinUserId = user.UserId;

        //            //LOT입력, 필수항목으로 입력 여부 체크

        //            inputTemp.Lotid = lot;

        //            //툴입력, 필수항목으로 입력 여부 체크                
        //            var pid = context.TbUvToolinfo.Where(w => w.CustToolno == tool).Select(x => x.ProductId).FirstOrDefault();


        //            inputTemp.ProductId = pid;
        //            inputTemp.SampleOrder = issample;
        //            inputTemp.Pnlqty = pnlqty;
        //            inputTemp.CreateTime = DateTime.Now;
        //            inputTemp.TrackinTime = DateTime.Now;
        //            inputTemp.Txid = Guid.NewGuid();
        //            var lotcount = context.TbUvWorkorder.Where(x => x.Lotid == lot && x.IsDone == false && x.ProductId == pid).Count();
        //            if (lotcount == 0)
        //            {
        //                context.TbUvWorkorder.AddAsync(inputTemp);
        //                context.SaveChanges();
        //                ExecuteRcv(lot);
        //                UpdateGridRcv();
        //            }
        //            else if (lotcount != 0)
        //            {
        //                if (MessageBox.Show("중복된 LOT가 있습니다 추가하시겠습니까?", "중복 LOT 확인", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        //                {
        //                    context.TbUvWorkorder.AddAsync(inputTemp);
        //                    context.SaveChanges();
        //                    ExecuteRcv(lot);
        //                    UpdateGridRcv();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        UpdateGridRcv();
        //    }
        //}

        //private void BtnAddOnly_OnClick(object sender, RoutedEventArgs e)
        //{
        //    var selectedlist = GridRcv.SelectionController.SelectedRows;
        //    BtnAddOnly.IsEnabled = false;
        //    BtnExeRcv.IsEnabled = false;
        //    foreach (var item in selectedlist)
        //    {
        //        try
        //        {
        //            if (item.RowData != null)
        //            {
        //                var rowdata = item.RowData;

        //                var pC = GridRcv.View.GetPropertyAccessProvider();

        //                var tool = pC.GetValue(rowdata, "SPECNR") as String;
        //                var lot = pC.GetValue(rowdata, "LOTID") as String;

        //                var pnlqty = Convert.ToInt16(pC.GetValue(rowdata, "PannelQty"));
        //                var lotdetailinfo = _depkgHelper.MesLotDetailInfoQry(lot, tool);
        //                bool issample = Char.IsLetter(lot, 0) && !lot.ToLower().StartsWith("p");
        //                var user = MainWindow._mainwindowViewModel.UserList.Where(x => x.UserName == MainWindow._mainwindowViewModel.SelectedUser).First();
        //                if (!IsToolExist(tool))
        //                {
        //                    RegistTool(tool, lot, issample);
        //                }
        //                using (var context = new Db_Uv_InventoryContext())
        //                {
        //                    var inputTemp = new TbUvWorkorder();

        //                    //고객사 선택, 필수항목으로 입력 여부 체크

        //                    var cust = new TbCustomer();
        //                    var selcust = "대덕전자(PKG)";
        //                    cust = context.TbCustomer.Where(x => x.CustName == selcust).FirstOrDefault();
        //                    inputTemp.CustId = cust.CustId;

        //                    //작성자 선택, 필수항목으로 입력 여부 체크

        //                    inputTemp.TrackinUserId = user.UserId;

        //                    //LOT입력, 필수항목으로 입력 여부 체크

        //                    inputTemp.Lotid = lot;

        //                    //툴입력, 필수항목으로 입력 여부 체크                
        //                    var pid = context.TbUvToolinfo.Where(w => w.CustToolno == tool).Select(x => x.ProductId).FirstOrDefault();

        //                    inputTemp.ProductId = pid;
        //                    inputTemp.SampleOrder = issample;
        //                    inputTemp.Pnlqty = pnlqty;
        //                    inputTemp.CreateTime = DateTime.Now;
        //                    inputTemp.TrackinTime = DateTime.Now;
        //                    inputTemp.Txid = Guid.NewGuid();
        //                    var lotcount = context.TbUvWorkorder.Where(x => x.Lotid == lot && x.IsDone == false && x.ProductId == pid).Count();
        //                    if (lotcount == 0)
        //                    {
        //                        context.TbUvWorkorder.AddAsync(inputTemp);
        //                        context.SaveChanges();
        //                    }
        //                    else if (lotcount != 0)
        //                    {
        //                        if (MessageBox.Show("중복된 LOT가 있습니다 추가하시겠습니까?", "중복 LOT 확인", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        //                        {
        //                            context.TbUvWorkorder.AddAsync(inputTemp);
        //                            context.SaveChanges();
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //            UpdateGridRcv();
        //        }

        //    }
        //    UpdateGridRcv();
        //    BtnAddOnly.IsEnabled = true;
        //    BtnExeRcv.IsEnabled = true;
        //}
        ////여러로트 받기
        //private async void btn_exe_rcv_Click(object sender, RoutedEventArgs e)
        //{
        //    BtnAddOnly.IsEnabled = false;
        //    BtnExeRcv.IsEnabled = false;
        //    var selectedlist = GridRcv.SelectionController.SelectedRows;
        //    var rcvlist = new List<string>();
        //    string lot = string.Empty;
        //    var user = MainWindow._mainwindowViewModel.UserList.Where(x => x.UserName == MainWindow._mainwindowViewModel.SelectedUser).First();
        //    foreach (var item in selectedlist)
        //    {
        //        try
        //        {
        //            if (item.RowData != null)
        //            {
        //                var rowdata = item.RowData;

        //                var pC = GridRcv.View.GetPropertyAccessProvider();

        //                var tool = pC.GetValue(rowdata, "SPECNR") as String;
        //                lot = pC.GetValue(rowdata, "LOTID") as String;

        //                var pnlqty = Convert.ToInt16(pC.GetValue(rowdata, "PannelQty"));
        //                var lotdetailinfo = _depkgHelper.MesLotDetailInfoQry(lot, tool);
        //                bool issample = Char.IsLetter(lot, 0) && !lot.ToLower().StartsWith("p");

        //                if (!IsToolExist(tool))
        //                {
        //                    RegistTool(tool, lot, issample);
        //                }
        //                using (var context = new Db_Uv_InventoryContext())
        //                {
        //                    var inputTemp = new TbUvWorkorder();

        //                    //고객사 선택, 필수항목으로 입력 여부 체크

        //                    var cust = new TbCustomer();
        //                    var selcust = "대덕전자(PKG)";
        //                    cust = context.TbCustomer.Where(x => x.CustName == selcust).FirstOrDefault();
        //                    inputTemp.CustId = cust.CustId;

        //                    //작성자 선택, 필수항목으로 입력 여부 체크
        //                    inputTemp.TrackinUserId = user.UserId;

        //                    //LOT입력, 필수항목으로 입력 여부 체크

        //                    inputTemp.Lotid = lot;

        //                    //툴입력, 필수항목으로 입력 여부 체크                
        //                    var pid = context.TbUvToolinfo.Where(w => w.CustToolno == tool).Select(x => x.ProductId).FirstOrDefault();

        //                    inputTemp.ProductId = pid;
        //                    inputTemp.SampleOrder = issample;
        //                    inputTemp.Pnlqty = pnlqty;
        //                    inputTemp.CreateTime = DateTime.Now;
        //                    inputTemp.TrackinTime = DateTime.Now;
        //                    inputTemp.Txid = Guid.NewGuid();
        //                    var lotcount = context.TbUvWorkorder.Where(x => x.Lotid == lot && x.IsDone == false && x.ProductId == pid).Count();
        //                    if (lotcount == 0)
        //                    {
        //                        context.TbUvWorkorder.AddAsync(inputTemp);
        //                        context.SaveChanges();
        //                        rcvlist.Add(lot);
        //                        //await ExecuteRcv(lot);
        //                    }
        //                    else if (lotcount != 0)
        //                    {
        //                        if (MessageBox.Show("중복된 LOT가 있습니다 추가하시겠습니까?", "중복 LOT 확인", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        //                        {
        //                            context.TbUvWorkorder.AddAsync(inputTemp);
        //                            context.SaveChanges();
        //                            rcvlist.Add(lot);
        //                            //await ExecuteRcv(lot);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //            UpdateGridRcv();
        //        }

        //    }
        //    if (rcvlist.Count == 1)
        //        ExecuteRcv(lot);
        //    else if (rcvlist.Count > 1)
        //        ExecuteMultiRcv(rcvlist);

        //    UpdateGridRcv();

        //    BtnAddOnly.IsEnabled = true;
        //    BtnExeRcv.IsEnabled = true;
        //}

        //private async Task<bool> ExecuteRcv(string lot)
        //{
        //    bool result = false;
        //    var client = new MesRuleServiceOIClient();
        //    try
        //    {
        //        var qrymsg = new MessageData()
        //        {
        //            COMMAND = "MoveReceiveLot",
        //            TID = Guid.NewGuid().ToString(),
        //            USERID = "103518",
        //            IPADDRESS = "192.168.0.20",
        //            SITEID = "1130",
        //            DATALIST = new List<Dictionary<string, object>>()
        //            {
        //                new Dictionary<string, object>()
        //                {
        //                    {
        //                        "lotId",
        //                        (object) lot
        //                    },
        //                    {
        //                        "location",
        //                        (object) "103518"
        //                    },
        //                    {
        //                        "userId",
        //                        (object) "103518"
        //                    },
        //                    {
        //                        "OUTTOOUTFLAG",
        //                        (object) "Y"
        //                    },
        //                    {
        //                        "SCHEDULETOLOCATION",
        //                        (object) "103518"
        //                    }
        //                }
        //            }
        //        };

        //        var dt = client.ExecCommandAsync(qrymsg).Result.DATASET.Tables["Reply"].Rows[0]["STATE"];
        //        if ((bool)dt == true)
        //        { result = true; }

        //        client.Close();
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        client.Abort();
        //        return result;
        //    }
        //}

        //private bool ExecuteMultiRcv(List<string> lot)
        //{
        //    bool result = false;
        //    var client = new MesRuleServiceOIClient();
        //    try
        //    {
        //        var lotlist = new List<Dictionary<string, object>>();
        //        foreach (var item in lot)
        //        {
        //            lotlist.Add(new Dictionary<string, object>()
        //                {
        //                    {
        //                        "lotId",
        //                        (object) item
        //                    },
        //                    {
        //                        "location",
        //                        (object) "103518"
        //                    },
        //                    {
        //                        "userId",
        //                        (object) "103518"
        //                    },
        //                    {
        //                        "OUTTOOUTFLAG",
        //                        (object) "Y"
        //                    },
        //                    {
        //                        "SCHEDULETOLOCATION",
        //                        (object) "103518"
        //                    }
        //                }

        //                );
        //        }

        //        var qrymsg = new MessageData()
        //        {
        //            COMMAND = "MoveReceiveLot",
        //            TID = Guid.NewGuid().ToString(),
        //            USERID = "103518",
        //            IPADDRESS = "192.168.0.20",
        //            SITEID = "1130",
        //            DATALIST = lotlist
        //        };

        //        var dt = client.ExecCommand(qrymsg).DATASET.Tables["Reply"].Rows[0]["STATE"];
        //        if ((bool)dt == true)
        //        { result = true; }

        //        client.Close();
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        client.Abort();
        //        return result;
        //    }
        //}

        //private bool IsToolExist(string tool)
        //{
        //    var result = false;
        //    using (var context = new Db_Uv_InventoryContext())
        //    {
        //        var toolinfo = context.TbUvToolinfo.Where(w => w.CustToolno == tool);
        //        if (toolinfo.Count() > 0)
        //            result = true;
        //        else if (toolinfo.Count() == 0)
        //            result = false;
        //    }

        //    return result;
        //}

        //private void RegistTool(string tool, string lot, bool issample)
        //{
        //    //try
        //    //{
        //    using (var context = new Db_Uv_InventoryContext())
        //    {
        //        var dbtools = context.TbUvToolinfo.Where(w => w.CustToolno == tool);
        //        var ldrillinfo = _depkgHelper.MesHoleInfoQry(lot, tool);
        //        var ldrillinfoSeq = ldrillinfo.Where(x => x.ProcName.ToLower().Contains("uv")).GroupBy(x => x.ProcSeq).Select(x => x.Key).ToList<string>();


        //        foreach (var seq in ldrillinfoSeq)
        //        {
        //            if (dbtools.Where(x => x.MesSeqCode == seq).Count() == 0)
        //            {
        //                var layerfr = ldrillinfo.Where(x => x.ProcSeq == seq)
        //                    .Select(x => x.TotalLayerFrom).FirstOrDefault();
        //                var layerto = ldrillinfo.Where(x => x.ProcSeq == seq)
        //                    .Select(x => x.TotalLayerTo).FirstOrDefault();
        //                var tempTool = GetTbUvToolinfo_DEPKG(tool, lot, seq, layerfr, layerto, issample);
        //                context.TbUvToolinfo.AddAsync(tempTool);
        //                context.SaveChanges();
        //                //MainWindow._mainwindowViewModel.ToolInfos = new ObservableCollection<TbUvToolinfo>(context.TbUvToolinfo);
        //            }

        //            else if (dbtools.Where(x => x.MesSeqCode == seq).Count() > 0)
        //            {
        //                continue;
        //            }
        //        }
        //    }
        //    //}
        //    //catch (Exception ex)
        //    //{ MessageBox.Show(ex.Message); }
        //}

        //드릴 정보 조회
        //private TbUvToolinfo GetTbUvToolinfo_DEPKG(string tool, string lot, string seq, string from, string to, bool issample)
        //{
        //    var tempTool = new TbUvToolinfo();
        //    from = from.PadLeft(3, '0');
        //    to = to.PadLeft(3, '0');
        //    //try
        //    //{
        //    using (var context = new Db_Uv_InventoryContext())

        //    {
        //        tempTool.CustId = "UV_02";
        //        tempTool.CustName = "대덕전자(PKG)";
        //        var lotdetailinfo = _depkgHelper.MesLotDetailInfoQry(lot, tool);
        //        var holeinfoqry = _depkgHelper.MesHoleInfoQryDS(lot, tool);
        //        var ldrillinfo = holeinfoqry.Tables[2].AsEnumerable().Select(row =>
        //            new MesHoleInfo
        //            {
        //                ProcSeq = row.Field<string>("PROCSEQ"),
        //                ProcLayerFrom = row.Field<string>("PRLAYERFR"),
        //                ProcLayerTo = row.Field<string>("PRLAYERTO"),
        //                ProcCode = row.Field<string>("PROCCD"),
        //                ProcName = row.Field<string>("PROCNM"),
        //                TotalLayerFrom = row.Field<string>("LAYERFR"),
        //                TotalLayerTo = row.Field<string>("LAYERTO"),
        //                LaserType = row.Field<string>("SPPROC"),
        //                CapturePadSize = row.Field<string>("SVHPAD"),
        //                LaserShot = row.Field<string>("SHOT"),
        //                HoleCount = row.Field<string>("ZSUM1"),
        //                HoleSizeTop = row.Field<string>("TOP1"),
        //                HoleSizeBot = row.Field<string>("Bottom1")
        //            }).ToList();

        //        var ldrillinfo2 = holeinfoqry.Tables[3].AsEnumerable().Select(row =>
        //            new MesHoleInfo2
        //            {
        //                ProcSeq = row.Field<string>("PROCSEQ"),
        //                ProcLayerFrom = row.Field<string>("PRLAYERFR"),
        //                ProcLayerTo = row.Field<string>("PRLAYERTO"),
        //                ProcCode = row.Field<string>("PROCCD"),
        //                ProcName = row.Field<string>("PROCNM"),
        //                TotalLayerFrom = row.Field<string>("LAYERFR"),
        //                TotalLayerTo = row.Field<string>("LAYERTO"),
        //                PcsHoleCount = row.Field<string>("HOLENR"),
        //                HoleSize = row.Field<string>("DHS"),
        //                HoleSeq = row.Field<string>("HOLESEQ"),
        //                ToleranceM = row.Field<string>("TOLERANCEM"),
        //                ToleranceP = row.Field<string>("TOLERANCEP"),
        //                LaserProcType = row.Field<string>("BRIEFS")
        //            }).ToList();

        //        var lotinfo = _depkgHelper.MesLotInfoQry(lot, tool);
        //        var specinfo = _depkgHelper.MesSpecInfoQry(lot, tool);
        //        var layerinfo = _depkgHelper.MesLayerInfoQry(lot, tool);
        //        var countItem = ldrillinfo.FindAll(f => f.LaserType.ToLower().Contains("uv"));
        //        var countDbItem = context.TbUvToolinfo.Where(w => w.CustToolno == tool && w.MesPrcName.ToLower().Contains("uv"));

        //        tempTool.CustModelname = lotdetailinfo.ModelName;
        //        tempTool.CustRevision = lotdetailinfo.ModelRev.Trim();
        //        tempTool.CustToolno = lotdetailinfo.ToolNo;
        //        tempTool.Layer = lotdetailinfo.LayerTotal;
        //        tempTool.EndCustomer = lotdetailinfo.Kname;
        //        tempTool.CustomerShipto = lotdetailinfo.Shipto;
        //        tempTool.ProductType = lotdetailinfo.SpecType2;
        //        tempTool.MesSeqCode = seq;
        //        tempTool.MesPrcName = ldrillinfo.Where(t => t.ProcSeq == seq).First().ProcName;
        //        tempTool.MesPrcCode = ldrillinfo.Where(t => t.ProcSeq == seq).First().ProcCode;

        //        tempTool.StackType = lotdetailinfo.SpecType1;
        //        tempTool.PrdCategory = lotdetailinfo.ProductType;
        //        tempTool.CreateDate = specinfo.CreateDate;

        //        tempTool.ArrayBlk = specinfo.Arrayx * specinfo.Arrayy;
        //        tempTool.WorksizeX = specinfo.Worksizex;
        //        tempTool.WorksizeY = specinfo.Worksizey;
        //        tempTool.PcssizeX = specinfo.Unitsizex;
        //        tempTool.PcssizeY = specinfo.Unitsizey;
        //        tempTool.Pcs = specinfo.Pcppanel;
        //        tempTool.Pcsperstrip = specinfo.PcsPerStrip;
        //        tempTool.StriparrayBlk = specinfo.StripArrayBlock;
        //        tempTool.StriparrayCol = specinfo.StripArrayCol;
        //        tempTool.StriparrayRow = specinfo.StripArrayRow;

        //        if (layerinfo.Where(x => x.MaterialInfo.FromLayer == from).Select(x => x.MaterialInfo.MaterialType)
        //                .FirstOrDefault().Contains("CCL"))
        //        {
        //            var linfo = layerinfo.Where(x => x.MaterialInfo.FromLayer == from).First();
        //            string insulinfo = string.Empty;
        //            tempTool.InsulThickness = Convert.ToDecimal(linfo.MaterialInfo.MaterialThickness);
        //            tempTool.CuThickness = Convert.ToDecimal(linfo.CopperThickness);
        //            tempTool.InsulType = linfo.MaterialInfo.MaterialType;
        //            foreach (var item in linfo.MaterialInfo.GetType().GetProperties())
        //            {
        //                insulinfo += item.GetValue(linfo.MaterialInfo, null).ToString() + ",";
        //            }
        //            tempTool.InsulInfo = insulinfo;
        //            tempTool.Depth = Convert.ToDecimal(linfo.InsulThickness) + Convert.ToDecimal(linfo.CopperThickness);
        //            tempTool.MainHoleSize = ldrillinfo2.Where(t => t.ProcSeq == seq).First().HoleSize.Trim();
        //        }

        //        else if (layerinfo.Where(x => x.MaterialInfo.FromLayer == from).Select(x => x.MaterialInfo.MaterialType)
        //                .FirstOrDefault().Contains("C/F"))
        //        {
        //            var bom = (Convert.ToInt16(layerinfo.Where(x => x.MaterialInfo.FromLayer == from).First().MaterialInfo.BomNumber) + 1).ToString().PadLeft(3, '0');
        //            var linfo = layerinfo.Where(x => x.MaterialInfo.BomNumber == bom).First();

        //            string insulinfo = string.Empty;
        //            tempTool.InsulThickness = Convert.ToDecimal(linfo.MaterialInfo.MaterialThickness);
        //            tempTool.CuThickness = Convert.ToDecimal(linfo.CopperThickness);
        //            tempTool.InsulType = linfo.MaterialInfo.MaterialType;
        //            foreach (var item in linfo.MaterialInfo.GetType().GetProperties())
        //            {
        //                insulinfo += item.GetValue(linfo.MaterialInfo, null).ToString() + ",";
        //            }
        //            tempTool.InsulInfo = insulinfo;
        //            tempTool.Depth = Convert.ToDecimal(linfo.InsulThickness) + Convert.ToDecimal(layerinfo.Where(x => x.MaterialInfo.FromLayer == from).First().CopperThickness);
        //            tempTool.MainHoleSize = ldrillinfo2.Where(t => t.ProcSeq == seq).First().HoleSize.Trim();
        //        }
        //        var laserprctype = ldrillinfo2.Where(t => t.ProcSeq == seq).GroupBy(x => x.LaserProcType).Select(k => k.Key).First();

        //        if (laserprctype.Contains("VIP"))
        //        {
        //            tempTool.PrcCode = "UV_SHT_DR_002";
        //            tempTool.PrcName = "드릴(PTH)";
        //        }

        //        else if (laserprctype.Contains("LVH"))
        //        {
        //            tempTool.PrcCode = "UV_SHT_DR_001";
        //            tempTool.PrcName = "드릴(BVH)";
        //        }
        //        else
        //        {
        //            tempTool.PrcCode = "UV_SHT_DR_002";
        //            tempTool.PrcName = "드릴(PTH)";
        //        }

        //        if (ldrillinfo.FindAll(f => f.ProcSeq == seq).Count() == 1)
        //        {
        //            //temp_tool.HoleCount = ldrillinfo.Where(t => t.ProcSeq == seq).First().HoleCount.Trim();
        //            tempTool.PrcLayerFrom1 = ldrillinfo.Where(t => t.ProcSeq == seq).First().ProcLayerFrom;
        //            tempTool.PrcLayerTo1 = ldrillinfo.Where(t => t.ProcSeq == seq).First().ProcLayerTo;
        //            tempTool.HoleCount1 = ldrillinfo.Where(t => t.ProcSeq == seq).First().HoleCount;
        //            tempTool.HoleCount = "CS:" + tempTool.HoleCount1;
        //        }

        //        if (ldrillinfo.FindAll(f => f.ProcSeq == seq).Count() == 2)
        //        {
        //            tempTool.PrcLayerFrom1 = ldrillinfo.Where(t => t.ProcSeq == seq).First().ProcLayerFrom;
        //            tempTool.PrcLayerTo1 = ldrillinfo.Where(t => t.ProcSeq == seq).First().ProcLayerTo;
        //            tempTool.HoleCount1 = ldrillinfo.Where(t => t.ProcSeq == seq).First().HoleCount;

        //            tempTool.PrcLayerFrom2 = ldrillinfo.Where(t => t.ProcSeq == seq).Skip(1).First().ProcLayerFrom;
        //            tempTool.PrcLayerTo2 = ldrillinfo.Where(t => t.ProcSeq == seq).Skip(1).First().ProcLayerTo;
        //            tempTool.HoleCount2 = ldrillinfo.Where(t => t.ProcSeq == seq).Skip(1).First().HoleCount;

        //            if (tempTool.PrcName.Contains("BVH"))
        //                tempTool.HoleCount = "CS:" + tempTool.HoleCount1 + " SS:" + tempTool.HoleCount2;
        //            else if (tempTool.PrcName.Contains("PTH"))
        //                tempTool.HoleCount = tempTool.HoleCount2;
        //        }


        //        if (issample == true)
        //        { tempTool.Sample = true; }
        //        else if (issample != true)
        //        { tempTool.Sample = false; }




        //        var thisyear = DateTime.Now.Year.ToString().Substring(2) + "-DEP-UV-";
        //        var prdidNo = context.TbUvToolinfo.Where(x => x.ProductId.Contains(thisyear)).Count() + 1;

        //        tempTool.ProductId = thisyear + prdidNo.ToString("D4");

        //    }
        //    //}

        //    //catch (Exception ex)
        //    //{ MessageBox.Show(ex.Message); }

        //    return tempTool;
        //}

        //private void BtnExeRcv_OnClick(object sender, RoutedEventArgs e)
        //{

        //}


    }
}
