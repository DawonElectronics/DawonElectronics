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
using CIM.MES.Common.Data;
using ConnectorDEPKG.Models;
using ConnectorDEPKG;
using ConnectorDEPKG.RuleServiceOI;
using DAWON_UV_INVENTORY_PROTO.Helper;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace DAWON_UV_INVENTORY_PROTO.Views
{
    /// <summary>
    /// Interaction logic for TrackInWindow.xaml
    /// </summary>
    public partial class TrackInWindowDepkg : ChromelessWindow
    {

        public TrackInWindowDepkgViewModel TrackinDepkgViewmodel = new TrackInWindowDepkgViewModel();
        DepkgHelper _depkgHelper = new DepkgHelper();
        public TrackInWindowDepkg()
        {

            InitializeComponent();
            TrackinDepkgViewmodel.SegementDataTable = _depkgHelper.qryWorkcenterDataTable();
            this.DataContext = TrackinDepkgViewmodel;

        }

        private void cmb_input_segment_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            GridRcv.ItemsSource = _depkgHelper.getRcvlotListDataTable();

        }

        private void grid_rcv_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var rowdata = GridRcv.SelectedItem;
                var pC = GridRcv.View.GetPropertyAccessProvider();

                var tool = pC.GetValue(rowdata, "SPECNR") as String;
                var lot = pC.GetValue(rowdata, "LOTID") as String;
                
                var pnlqty = Convert.ToInt16(pC.GetValue(rowdata, "PannelQty"));
                var lotdetailinfo = _depkgHelper.MesLotDetailInfoQry(lot, tool);
                bool issample = Char.IsLetter(lot, 0) && !lot.ToLower().StartsWith("p");

                if (!IsToolExist(tool))
                {
                    RegistTool(tool,  lot, issample);
                }
                using (var context = new Db_Uv_InventoryContext())
                {
                    var inputTemp = new TbUvWorkorder();

                    //고객사 선택, 필수항목으로 입력 여부 체크

                    var cust = new TbCustomer();
                    var selcust = "대덕전자(PKG)";
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
                    var pid =context.TbUvToolinfo.Where(w => w.CustToolno == tool).Select(x => x.ProductId).FirstOrDefault();


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
                        GridRcv.ItemsSource = _depkgHelper.getRcvlotListDataTable();
                    }
                    else if (lotcount != 0)
                    {
                        if (MessageBox.Show("중복된 LOT가 있습니다 추가하시겠습니까?", "중복 LOT 확인", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            context.TbUvWorkorder.AddAsync(inputTemp);
                            context.SaveChanges();
                            ExecuteRcv(lot);
                            GridRcv.ItemsSource = _depkgHelper.getRcvlotListDataTable();
                        }
                    }
                }
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                GridRcv.ItemsSource = _depkgHelper.getRcvlotListDataTable();
            }
}


        //여러로트 받기
        private async void btn_exe_rcv_Click(object sender, RoutedEventArgs e)
        {
            var selectedlist = GridRcv.SelectionController.SelectedRows;

            foreach (var item in selectedlist)
            {
                try
                {
                    if (item.RowData != null)
                    {
                        var rowdata = item.RowData;
                    
                    var pC = GridRcv.View.GetPropertyAccessProvider();

                    var tool = pC.GetValue(rowdata, "SPECNR") as String;
                    var lot = pC.GetValue(rowdata, "LOTID") as String;
                    
                    var pnlqty = Convert.ToInt16(pC.GetValue(rowdata, "PannelQty") as String);
                    var lotdetailinfo = _depkgHelper.MesLotDetailInfoQry(lot, tool);
                    bool issample = Char.IsLetter(lot, 0) && !lot.ToLower().StartsWith("p");

                    if (!IsToolExist(tool))
                    {
                        RegistTool(tool,  lot, issample);
                    }
                    using (var context = new Db_Uv_InventoryContext())
                    {
                        var inputTemp = new TbUvWorkorder();

                        //고객사 선택, 필수항목으로 입력 여부 체크

                        var cust = new TbCustomer();
                        var selcust = "대덕전자(PKG)";
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
                        var pid = context.TbUvToolinfo.Where(w => w.CustToolno == tool).Select(x => x.ProductId).FirstOrDefault();

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
                            await ExecuteRcv(lot);
                        }
                        else if (lotcount != 0)
                        {
                            if (MessageBox.Show("중복된 LOT가 있습니다 추가하시겠습니까?", "중복 LOT 확인", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            {
                                context.TbUvWorkorder.AddAsync(inputTemp);
                                context.SaveChanges();
                                await ExecuteRcv(lot);
                            }
                        }
                    }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    GridRcv.ItemsSource = _depkgHelper.getRcvlotListDataTable();
                }
                
            }
            GridRcv.ItemsSource = _depkgHelper.getRcvlotListDataTable();
        }

        private async Task<bool> ExecuteRcv(string lot)
        {
            bool result = false;
            var client = new MesRuleServiceOIClient();
            try
            {
                var qrymsg = new MessageData()
                {
                    COMMAND = "MoveReceiveLot",
                    TID = Guid.NewGuid().ToString(),
                    USERID = "103518",
                    IPADDRESS = "192.168.0.20",
                    SITEID = "1130",
                    DATALIST = new List<Dictionary<string, object>>()
                    {
                        new Dictionary<string, object>()
                        {
                            {
                                "lotId",
                                (object) lot
                            },
                            {
                                "location",
                                (object) "103518"
                            },
                            {
                                "userId",
                                (object) "103518"
                            },
                            {
                                "OUTTOOUTFLAG",
                                (object) "Y"
                            },
                            {
                                "SCHEDULETOLOCATION",
                                (object) "103518"
                            }
                        }
                    }
                };

                var dt = client.ExecCommandAsync(qrymsg).Result.DATASET.Tables["Reply"].Rows[0]["STATE"];
                if ((bool)dt == true)
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
            var client = new MesRuleServiceOIClient();
            try
            {
                var lotlist = new List<Dictionary<string, object>>();
                foreach (var item in lot)
                {
                    lotlist.Add(new Dictionary<string, object>()
                        {
                            {
                                "lotId",
                                (object) item
                            },
                            {
                                "location",
                                (object) "103518"
                            },
                            {
                                "userId",
                                (object) "103518"
                            },
                            {
                                "OUTTOOUTFLAG",
                                (object) "Y"
                            },
                            {
                                "SCHEDULETOLOCATION",
                                (object) "103518"
                            }
                        }

                        );
                }

                var qrymsg = new MessageData()
                    {
                        COMMAND = "MoveReceiveLot",
                        TID = Guid.NewGuid().ToString(),
                        USERID = "103518",
                        IPADDRESS = "192.168.0.20",
                        SITEID = "1130",
                        DATALIST = lotlist
                };

                var dt = client.ExecCommand(qrymsg).DATASET.Tables["Reply"].Rows[0]["STATE"];
                if ((bool)dt == true)
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

        private bool IsToolExist(string tool)
        {
            var result = false;
            using (var context = new Db_Uv_InventoryContext())
            {
                var toolinfo = context.TbUvToolinfo.Where(w => w.CustToolno == tool);
                if (toolinfo.Count() > 0)
                    result = true;
                else if (toolinfo.Count() == 0)
                    result = false;
            }

            return result;
        }

        private void RegistTool(string tool, string lot, bool issample)
        {
            //try
            //{
                using (var context = new Db_Uv_InventoryContext())
                {
                    var dbtools = context.TbUvToolinfo.Where(w => w.CustToolno == tool);
                    var ldrillinfo = _depkgHelper.MesHoleInfoQry(lot,tool);
                    var ldrillinfoSeq = ldrillinfo.Where(x => x.ProcName.ToLower().Contains("uv")).GroupBy(x => x.ProcSeq).Select(x => x.Key).ToList<string>();


                    foreach (var seq in ldrillinfoSeq)
                    {
                        if (dbtools.Where(x => x.MesSeqCode == seq).Count() == 0)
                        {
                            var layerfr = ldrillinfo.Where(x => x.ProcSeq == seq)
                                .Select(x => x.TotalLayerFrom).FirstOrDefault();
                            var layerto = ldrillinfo.Where(x => x.ProcSeq == seq)
                                .Select(x => x.TotalLayerTo).FirstOrDefault();
                            var tempTool = GetTbUvToolinfo_DEPKG(tool, lot, seq, layerfr,layerto, issample);
                            context.TbUvToolinfo.AddAsync(tempTool);
                            context.SaveChanges();
                            //MainWindow.MainwindowViewModel.ToolInfos = new ObservableCollection<TbUvToolinfo>(context.TbUvToolinfo);
                        }

                        else if (dbtools.Where(x => x.MesSeqCode == seq).Count() > 0)
                        {
                            continue;
                        }
                    }
                }
            //}
            //catch (Exception ex)
            //{ MessageBox.Show(ex.Message); }
        }

        //드릴 정보 조회
        private TbUvToolinfo GetTbUvToolinfo_DEPKG(string tool,  string lot, string seq,string from,string to, bool issample)
        {
            var tempTool = new TbUvToolinfo();
            from = from.PadLeft(3, '0');
            to = to.PadLeft(3, '0');
            //try
            //{
            using (var context = new Db_Uv_InventoryContext())

                {
                    tempTool.CustId = "UV_02";
                    tempTool.CustName = "대덕전자(PKG)";
                    var lotdetailinfo = _depkgHelper.MesLotDetailInfoQry(lot, tool);
                    var holeinfoqry = _depkgHelper.MesHoleInfoQryDS(lot,tool);
                    var ldrillinfo = holeinfoqry.Tables[2].AsEnumerable().Select(row =>
                        new MesHoleInfo
                        {
                            ProcSeq = row.Field<string>("PROCSEQ"),
                            ProcLayerFrom = row.Field<string>("PRLAYERFR"),
                            ProcLayerTo = row.Field<string>("PRLAYERTO"),
                            ProcCode = row.Field<string>("PROCCD"),
                            ProcName = row.Field<string>("PROCNM"),
                            TotalLayerFrom = row.Field<string>("LAYERFR"),
                            TotalLayerTo = row.Field<string>("LAYERTO"),
                            LaserType = row.Field<string>("SPPROC"),
                            CapturePadSize = row.Field<string>("SVHPAD"),
                            LaserShot = row.Field<string>("SHOT"),
                            HoleCount = row.Field<string>("ZSUM1"),
                            HoleSizeTop = row.Field<string>("TOP1"),
                            HoleSizeBot = row.Field<string>("Bottom1")
                        }).ToList();

                    var ldrillinfo2 = holeinfoqry.Tables[3].AsEnumerable().Select(row =>
                        new MesHoleInfo2
                        {
                            ProcSeq = row.Field<string>("PROCSEQ"),
                            ProcLayerFrom = row.Field<string>("PRLAYERFR"),
                            ProcLayerTo = row.Field<string>("PRLAYERTO"),
                            ProcCode = row.Field<string>("PROCCD"),
                            ProcName = row.Field<string>("PROCNM"),
                            TotalLayerFrom = row.Field<string>("LAYERFR"),
                            TotalLayerTo = row.Field<string>("LAYERTO"),
                            PcsHoleCount = row.Field<string>("HOLENR"),
                            HoleSize = row.Field<string>("DHS"),
                            HoleSeq = row.Field<string>("HOLESEQ"),
                            ToleranceM = row.Field<string>("TOLERANCEM"),
                            ToleranceP = row.Field<string>("TOLERANCEP"),
                            LaserProcType = row.Field<string>("BRIEFS")
                        }).ToList();

                    var lotinfo = _depkgHelper.MesLotInfoQry(lot,tool);
                    var specinfo = _depkgHelper.MesSpecInfoQry(lot,tool);
                    var layerinfo = _depkgHelper.MesLayerInfoQry(lot, tool);
                    var countItem = ldrillinfo.FindAll(f => f.LaserType.ToLower().Contains("uv"));
                    var countDbItem = context.TbUvToolinfo.Where(w => w.CustToolno == tool && w.MesPrcName.ToLower().Contains("uv"));
                    
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
                    tempTool.CreateDate = DateTime.ParseExact(specinfo.CreateDate, "yyyyMMdd", null);

                    tempTool.ArrayBlk = specinfo.Arrayx * specinfo.Arrayy;
                    tempTool.WorksizeX = specinfo.Worksizex;
                    tempTool.WorksizeY = specinfo.Worksizey;
                    tempTool.PcssizeX = specinfo.Unitsizex;
                    tempTool.PcssizeY = specinfo.Unitsizey;
                    tempTool.Pcs = specinfo.Pcppanel;
                    tempTool.Pcsperstrip = specinfo.PcsPerStrip;
                    tempTool.StriparrayBlk = specinfo.StripArrayBlock;
                    tempTool.StriparrayCol = specinfo.StripArrayCol;
                    tempTool.StriparrayRow = specinfo.StripArrayRow;

                    if (layerinfo.Where(x => x.MaterialInfo.FromLayer == from).Select(x => x.MaterialInfo.MaterialType)
                            .FirstOrDefault().Contains("CCL"))
                    {
                        var linfo = layerinfo.Where(x => x.MaterialInfo.FromLayer == from).First();
                        string insulinfo = string.Empty;
                        tempTool.InsulThickness = Convert.ToDecimal(linfo.MaterialInfo.MaterialThickness);
                        tempTool.CuThickness = Convert.ToDecimal(linfo.CopperThickness);
                        tempTool.InsulType = linfo.MaterialInfo.MaterialType;
                        foreach (var item in linfo.MaterialInfo.GetType().GetProperties())
                        {
                            insulinfo += item.GetValue(linfo.MaterialInfo, null).ToString()+",";
                        }
                        tempTool.InsulInfo = insulinfo;
                        tempTool.Depth = Convert.ToDecimal(linfo.InsulThickness)+ Convert.ToDecimal(linfo.CopperThickness);
                        tempTool.MainHoleSize = ldrillinfo2.Where(t => t.ProcSeq == seq).First().HoleSize.Trim();
                }

                    else if (layerinfo.Where(x => x.MaterialInfo.FromLayer == from).Select(x => x.MaterialInfo.MaterialType)
                            .FirstOrDefault().Contains("C/F"))
                    {
                        var bom = (Convert.ToInt16(layerinfo.Where(x => x.MaterialInfo.FromLayer == from).First().MaterialInfo.BomNumber)+1).ToString().PadLeft(3,'0');
                        var linfo = layerinfo.Where(x => x.MaterialInfo.BomNumber == bom).First();

                        string insulinfo = string.Empty;
                        tempTool.InsulThickness = Convert.ToDecimal(linfo.MaterialInfo.MaterialThickness);
                        tempTool.CuThickness = Convert.ToDecimal(linfo.CopperThickness);
                        tempTool.InsulType = linfo.MaterialInfo.MaterialType;
                        foreach (var item in linfo.MaterialInfo.GetType().GetProperties())
                        {
                            insulinfo += item.GetValue(linfo.MaterialInfo, null).ToString() + ",";
                        }
                        tempTool.InsulInfo = insulinfo;
                        tempTool.Depth = Convert.ToDecimal(linfo.InsulThickness) + Convert.ToDecimal(layerinfo.Where(x => x.MaterialInfo.FromLayer == from).First().CopperThickness);
                        tempTool.MainHoleSize = ldrillinfo2.Where(t => t.ProcSeq == seq).First().HoleSize.Trim();
                }

                    
                    

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
                        tempTool.PrcLayerFrom1 = ldrillinfo.Where(t => t.ProcSeq == seq).First().ProcLayerFrom;
                        tempTool.PrcLayerTo1 = ldrillinfo.Where(t => t.ProcSeq == seq).First().ProcLayerTo;
                        tempTool.HoleCount1 = ldrillinfo.Where(t => t.ProcSeq == seq).First().HoleCount;

                        tempTool.PrcLayerFrom2 = ldrillinfo.Where(t => t.ProcSeq == seq).Skip(1).First().ProcLayerFrom;
                        tempTool.PrcLayerTo2 = ldrillinfo.Where(t => t.ProcSeq == seq).Skip(1).First().ProcLayerTo;
                        tempTool.HoleCount2 = ldrillinfo.Where(t => t.ProcSeq == seq).Skip(1).First().HoleCount;

                        tempTool.HoleCount = "CS:" + tempTool.HoleCount1 + " SS:" + tempTool.HoleCount2;
                    }
                    

                    if (issample == true)
                    { tempTool.Sample = true; }
                    else if (issample != true)
                    { tempTool.Sample = false; }

                    var laserprctype = ldrillinfo2.Where(t => t.ProcSeq == seq).GroupBy(x => x.LaserProcType).Select(k => k.Key).First();
                    
                    if (laserprctype.Contains("VIP"))
                    {
                        tempTool.PrcCode = "UV_SHT_DR_002";
                        tempTool.PrcName = "드릴(PTH)";
                    }

                    else if (laserprctype.Contains("LVH"))
                    {
                        tempTool.PrcCode = "UV_SHT_DR_001";
                        tempTool.PrcName = "드릴(BVH)";
                    }
                    else 
                    {
                    tempTool.PrcCode = "UV_SHT_DR_002";
                    tempTool.PrcName = "드릴(PTH)";
                }

                var thisyear = DateTime.Now.Year.ToString().Substring(2) + "-DEP-UV-";
                    var prdidNo = context.TbUvToolinfo.Where(x => x.ProductId.Contains(thisyear)).Count() + 1;

                    tempTool.ProductId = thisyear + prdidNo.ToString("D4");

                }
            //}

            //catch (Exception ex)
            //{ MessageBox.Show(ex.Message); }

            return tempTool;
        }

        private void BtnExeRcv_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
