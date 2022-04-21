using ConnectorYPE;
using ConnectorYPE.Models;
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
using System.Windows.Controls;
using System.Windows.Input;
using AutoMapper;
using Syncfusion.Data.Extensions;
using Syncfusion.UI.Xaml.Grid;
using DataRow = System.Data.DataRow;

namespace DAWON_UV_INVENTORY_PROTO.Views
{
    /// <summary>
    /// Interaction logic for TrackInWindow.xaml
    /// </summary>
    public partial class TrackInWindowYPE : ChromelessWindow
    {
        Regex re_yplot = new Regex(@"[0-9]{6}.[0-9]{3}.[0-9].[A-Z]{2}[0-9]{2}.[0-9]{3}.[0-9]{3}");
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
            var registed = MainWindow._mainwindowViewModel.ToolInfos.Where(x => x.CustName == "영풍전자").Select(s => new { s.CustToolno, s.CustRevision, s.MesPrcCode, s.YpeDatarev });
            //var registed2 = MainWindow._mainwindowViewModel.Customer.Select(s => s.CustName).ToList<string>();
            var result = registed.Where(x => x.CustToolno == tool && x.CustRevision == rev && x.MesPrcCode == mesprccode).Any();
            return result;
        }


        private async void UpdateGridRcv()
        {

            //var rcvdt = await ypeHelper.QryWipDT();


            //DataTable rcvdt2 = rcvdt.AsEnumerable()
            //    .Where(r =>((Convert.ToInt16(r.Field<string>("processsegmentid").Substring(0, 4))) > 2015 && (Convert.ToInt16(r.Field<string>("processsegmentid").Substring(0, 4))) < 2030))
            //    .CopyToDataTable();


            //rcvdt2.Columns.Add("IsRegist");

            //var lotlist = rcvdt2.AsEnumerable().Select(w => w.Field<string>("lotid")).ToList<string>();

            //foreach (var item in lotlist)
            //{
            //    rcvdt2.Select(string.Format("[lotid] = '{0}'", item)).ToList<DataRow>()
            //        .ForEach(r => r["IsRegist"] = GetRegist(r["processdefid"].ToString(), r["productrevision"].ToString(), r["processsegmentid"].ToString()));
            //}

           
            var rcvlist = await ypeHelper.QryWipList();
            var rcvlist2 = new List<WipModelAfterToolValidation>();
            VisualStateManager.GoToState(this.GridRcv, "Busy", true);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<WipModel, WipModelAfterToolValidation>());
            var mapper = new Mapper(config);
            var tmprcvlist = rcvlist.Where(x => (Convert.ToInt16(x.processsegmentid.Substring(0, 4)) > 2015 
                                                 && Convert.ToInt16(x.processsegmentid.Substring(0, 4)) < 2030) &&(x.processstate.Contains("WaitForReceive") || x.processstate.Contains("Wait"))).ToList<WipModel>();
            rcvlist2 = mapper.Map<List<WipModel>, List<WipModelAfterToolValidation>>(tmprcvlist);

            foreach (var item in rcvlist2)
            {
                item.IsRegist = GetRegist(item.processdefid, item.productrevision, item.processsegmentid);
            }
            
            TrackinYpeViewmodel.RcvLotList = rcvlist2;
            if(GridRcv.IsLoaded)
            { VisualStateManager.GoToState(this.GridRcv, "Normal", true); }
            
        }

        private async void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            var itemafter = new YPToolInfo();
            var rowdata = GridRcv.SelectedItem;
            var pC = GridRcv.View.GetPropertyAccessProvider();

            var tool = pC.GetValue(rowdata, "processdefid").ToString();
            //var tool = "H580035-D";
            var lot = pC.GetValue(rowdata, "lotid").ToString();
            var subrev = pC.GetValue(rowdata, "productrevision").ToString();
            //var subrev = "0";
            var prccode = pC.GetValue(rowdata, "processsegmentid").ToString();
            var pnlqty = Convert.ToInt16(pC.GetValue(rowdata, "wiptotalpnl"));
            var issample = (pC.GetValue(rowdata, "lottype").ToString() == "양산") ? false : true;
            var user = MainWindow._mainwindowViewModel.UserList.Where(x => x.UserName == MainWindow._mainwindowViewModel.SelectedUser).First();
            
            var yptoollist = await ypeHelper.QryYPToolinfoList(tool, subrev);
            yptoollist = yptoollist.OrderBy(x => x.MesSeqCode).ToList<YPToolInfo>();
            var materialinfolist = await ypeHelper.QryMaterialInfo(tool, subrev);

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
                    { last = true; }

                    if (!last)
                    {
                        var before = Convert.ToInt16(yptoollist[i].MesSeqCode);
                        var after = Convert.ToInt16(yptoollist[i + 1].MesSeqCode);

                        if (after - before == 10)
                        {
                            linkedprc = true;
                        }
                    }

                    if (linkedprc)
                    {
                        var item = yptoollist[i];
                        itemafter = yptoollist[i + 1];
                        var tmptool = new TbUvToolinfo();

                        var thisyear = DateTime.Now.Year.ToString().Substring(2) + "-YPE-UV-";
                        //var prdidNo = db.TbUvToolinfo.Where(x => x.ProductId.Contains(thisyear)).Count() + 1;
                        var prdidNo = db.TbUvToolinfo.Where(x => x.ProductId.Contains(thisyear)).OrderBy(s => s.ProductId).Select(s => Convert.ToInt16(s.ProductId.Substring(10, 4))).LastOrDefault() + 1;
                        tmptool.ProductId = thisyear + prdidNo.ToString("D4");



                        tmptool.CustToolno = tool;
                        tmptool.CustModelname = item.CustModelname;
                        tmptool.CustRevision = item.MesRevision.Trim();
                        tmptool.YpeDatarev = item.DataRevision;
                        tmptool.CreateDate = item.CreateDate;
                        tmptool.CustName = "영풍전자";
                        tmptool.CustId = "UV_03";
                        tmptool.PrdCategory = item.PrdCategory;
                        tmptool.Sample = issample;
                        tmptool.WorksizeX = Convert.ToDecimal(item.WorksizeX);
                        tmptool.WorksizeY = Convert.ToDecimal(item.WorksizeY);
                        tmptool.ArrayBlk = Convert.ToInt16(item.ArrayBlk.Replace(".0", ""));
                        tmptool.Pcs = Convert.ToInt16(item.Pcs.Replace(".0", ""));
                        tmptool.Layer = Convert.ToInt16(item.Layer);
                        tmptool.MesSeqCode = item.MesSeqCode + "/" + itemafter.MesSeqCode;
                        tmptool.MesPrcCode = item.MesPrcCode + "/" + itemafter.MesPrcCode;
                        tmptool.MesPrcName = item.MesPrcName + "/" + prcnamereg.Match(itemafter.MesPrcName).Groups[1].Value;
                        tmptool.CustomerShipto = item.EndCustomer;
                        tmptool.EndCustomer = item.EndCustomer;
                        tmptool.ProductType = item.ProductType;
                        tmptool.ToolNotes = item.ToolNotes + "/" + itemafter.ToolNotes;
                        tmptool.MainHoleSize = item.MainHoleSize;
                        tmptool.PrcLayerFrom1 = item.PrcLayerFrom1;
                        tmptool.PrcLayerTo1 = item.PrcLayerTo1;
                        tmptool.PrcLayerFrom2 = itemafter.PrcLayerFrom1;
                        tmptool.PrcLayerTo2 = itemafter.PrcLayerTo1;
                        tmptool.InsulInfo = item.InsulInfo;
                        //tmptool.InsulThickness = item.InsulInfo;
                        //tmptool.CuThickness = item.InsulInfo;
                        tmptool.CamFinished = false;
                        tmptool.YpNextResourcelist = await ypeHelper.QryRoutingResource(itemafter.NextOpid);
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
                    else if (!linkedprc && itemafter.MesSeqCode == yptoollist[i].MesSeqCode) { continue; }

                    else
                    {
                        var item = yptoollist[i];
                        var tmptool = new TbUvToolinfo();

                        var thisyear = DateTime.Now.Year.ToString().Substring(2) + "-YPE-UV-";
                        //var prdidNo = db.TbUvToolinfo.Where(x => x.ProductId.Contains(thisyear)).Count() + 1;
                        var prdidNo = db.TbUvToolinfo.Where(x => x.ProductId.Contains(thisyear)).OrderBy(s => s.ProductId).Select(s => Convert.ToInt16(s.ProductId.Substring(10, 4))).LastOrDefault() + 1;
                        tmptool.ProductId = thisyear + prdidNo.ToString("D4");

                        tmptool.CustToolno = tool;
                        tmptool.CustModelname = item.CustModelname;
                        tmptool.CustRevision = item.MesRevision.Trim();
                        tmptool.YpeDatarev = item.DataRevision;
                        tmptool.CreateDate = item.CreateDate;
                        tmptool.CustName = "영풍전자";
                        tmptool.CustId = "UV_03";
                        tmptool.PrdCategory = item.PrdCategory;
                        tmptool.Sample = issample;
                        tmptool.WorksizeX = Convert.ToDecimal(item.WorksizeX);
                        tmptool.WorksizeY = Convert.ToDecimal(item.WorksizeY);
                        tmptool.ArrayBlk = Convert.ToInt16(item.ArrayBlk.Replace(".0", ""));
                        tmptool.Pcs = Convert.ToInt16(item.Pcs.Replace(".0", ""));
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
                        tmptool.PrcLayerFrom2 = item.PrcLayerFrom2;
                        tmptool.PrcLayerTo2 = item.PrcLayerTo2;
                        tmptool.InsulInfo = item.InsulInfo;
                        //tmptool.InsulThickness = item.InsulInfo;
                        //tmptool.CuThickness = item.InsulInfo;
                        tmptool.CamFinished = false;
                        tmptool.YpNextResourcelist = await ypeHelper.QryRoutingResource(item.NextOpid);
                        if (item.MesPrcCode.StartsWith("20161"))
                        {
                            tmptool.PrcName = "RTR(BVH)";
                            tmptool.PrcCode = "UV_RTR_DR_001";
                        }
                        else if (item.MesPrcCode.StartsWith("20160"))
                        {
                            if (Convert.ToInt16(item.MesSeqCode) < 70)
                            {
                                tmptool.PrcName = "BASE";
                                tmptool.PrcCode = "UV_SHT_DR_004";
                            }
                            else
                            {
                                tmptool.PrcName = "드릴(BVH)";
                                tmptool.PrcCode = "UV_SHT_DR_001";
                            }
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
                MainWindow._mainwindowViewModel.ToolInfos = new List<TbUvToolinfo>(db.TbUvToolinfo);
            }
        }

        private void GridRcv_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
           if (Keyboard.IsKeyDown(Key.LeftCtrl))
            { 
                if (re_yplot.IsMatch(e.Key.ToString())) e.Handled = true;
                var a = Clipboard.GetText();
                foreach (var item in TrackinYpeViewmodel.RcvLotList)
                {
                    WipModelAfterToolValidation record = item;
                    
                    
                    
                     if (record.lotid == a )
                    {
                        //TrackinYpeViewmodel.SelectedItem = item;
                        GridRcv.SelectedItems.Add(item);
                        break;
                    }

                }
        }
    }

        private async void GridRcv_OnItemsSourceChanged(object? sender, GridItemsSourceChangedEventArgs e)
        {
            await Task.Delay(1900);
            sfBusyIndicator.IsBusy = false;
        }

        private void UIElement_OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (!re_yplot.IsMatch(e.Key.ToString())) e.Handled = true;
            var lot = re_yplot.Match(tboxLot.Text).Groups[1].Value;

            foreach (var item in TrackinYpeViewmodel.RcvLotList)
            {
                WipModelAfterToolValidation record = item;

                if (record.lotid == lot)
                {
                    GridRcv.SelectedItems.Add(item);
                    tboxLot.Text = string.Empty;
                    tboxLot.Focus();
                    break;
                }

            }
        }

        private void TboxLot_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (re_yplot.IsMatch(e.Key.ToString())) e.Handled = true;
            var lot = re_yplot.Match(tboxLot.Text).Value;

            foreach (var item in TrackinYpeViewmodel.RcvLotList)
            {
                WipModelAfterToolValidation record = item;

                if (record.lotid == lot)
                {
                    GridRcv.SelectedItems.Add(item);
                    tboxLot.Text = string.Empty;
                    tboxLot.Focus();
                    break;
                }

            }
        }

       
    }
}
