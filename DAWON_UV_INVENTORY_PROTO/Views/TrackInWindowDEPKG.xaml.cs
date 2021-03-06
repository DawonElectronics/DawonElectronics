using CIM.MES.Common.Data;
using ConnectorDEPKG;
using ConnectorDEPKG.Models;
using ConnectorDEPKG.RuleServiceOI;
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
using AutoMapper;
using Syncfusion.Data.Extensions;
using DataRow = System.Data.DataRow;

namespace DAWON_UV_INVENTORY_PROTO.Views
{
    /// <summary>
    /// Interaction logic for TrackInWindow.xaml
    /// </summary>
    ///
    public class AutoMapperProfileDePkg : Profile
    {

        public AutoMapperProfileDePkg()
        {
            //源类=>目标类
            //CreateMap<DogModel, Dog2Model>();

            //DataTable=>Model
            IMappingExpression<DataRow, DePkgRcvModelAfterValidation> mappingExpression;
            mappingExpression = CreateMap<DataRow, DePkgRcvModelAfterValidation>();
            mappingExpression.ForMember(d => d.IsRegist, o => o.MapFrom(s => s["IsRegist"]));
            mappingExpression.ForMember(d => d.LOTID, o => o.MapFrom(s => s["LOTID"]));
            mappingExpression.ForMember(d => d.CHK, o => o.MapFrom(s => s["CHK"]));
            mappingExpression.ForMember(d => d.HotType, o => o.MapFrom(s => s["HotType"]));
            mappingExpression.ForMember(d => d.IntransitTimeHour, o => o.MapFrom(s => s["IntransitTimeHour"]));
            mappingExpression.ForMember(d => d.KSHORTNAME, o => o.MapFrom(s => s["KSHORTNAME"]));
            mappingExpression.ForMember(d => d.KUNNR, o => o.MapFrom(s => s["KUNNR"]));
            mappingExpression.ForMember(d => d.LAYERDIVISION, o => o.MapFrom(s => s["LAYERDIVISION"]));
            mappingExpression.ForMember(d => d.LPST, o => o.MapFrom(s => s["LPST"]));
            mappingExpression.ForMember(d => d.OrderPriority, o => o.MapFrom(s => s["OrderPriority"]));
            mappingExpression.ForMember(d => d.PLANCOMPLETE, o => o.MapFrom(s => s["PLANCOMPLETE"]));
            mappingExpression.ForMember(d => d.PLANEQUIPMENT, o => o.MapFrom(s => s["PLANEQUIPMENT"]));
            mappingExpression.ForMember(d => d.PLANSTART, o => o.MapFrom(s => s["PLANSTART"]));
            mappingExpression.ForMember(d => d.PROCESSSEQ, o => o.MapFrom(s => s["PROCESSSEQ"]));
            mappingExpression.ForMember(d => d.PRODUCT_LAYER, o => o.MapFrom(s => s["PRODUCT_LAYER"]));
            mappingExpression.ForMember(d => d.PannelQty, o => o.MapFrom(s => s["PannelQty"]));
            mappingExpression.ForMember(d => d.PieceQty, o => o.MapFrom(s => s["PieceQty"]));
            mappingExpression.ForMember(d => d.ProductCode, o => o.MapFrom(s => s["ProductCode"]));
            mappingExpression.ForMember(d => d.SHIPTO, o => o.MapFrom(s => s["SHIPTO"]));
            mappingExpression.ForMember(d => d.RunningTime, o => o.MapFrom(s => s["RunningTime"]));
            mappingExpression.ForMember(d => d.ProductRevision, o => o.MapFrom(s => s["ProductRevision"]));
            mappingExpression.ForMember(d => d.ProductDefinition, o => o.MapFrom(s => s["ProductDefinition"]));
            mappingExpression.ForMember(d => d.SPECNR, o => o.MapFrom(s => s["SPECNR"]));
            mappingExpression.ForMember(d => d.STATE, o => o.MapFrom(s => s["STATE"]));
            mappingExpression.ForMember(d => d.SUPDA, o => o.MapFrom(s => s["SUPDA"]));
            mappingExpression.ForMember(d => d.SqareMeter, o => o.MapFrom(s => s["SqareMeter"]));
            mappingExpression.ForMember(d => d.StripQty, o => o.MapFrom(s => s["StripQty"]));
            mappingExpression.ForMember(d => d.WORK_SEQ, o => o.MapFrom(s => s["WORK_SEQ"]));
            mappingExpression.ForMember(d => d.TRANSFERPROC, o => o.MapFrom(s => s["TRANSFERPROC"]));
            mappingExpression.ForMember(d => d.TRANSFERTIME, o => o.MapFrom(s => s["TRANSFERTIME"]));
            mappingExpression.ForMember(d => d.WaitingTime, o => o.MapFrom(s => s["WaitingTime"]));
            mappingExpression.ForMember(d => d.WTTIME, o => o.MapFrom(s => s["WTTIME"]));
            mappingExpression.ForMember(d => d.WORK_SEQ_ASC, o => o.MapFrom(s => s["WORK_SEQ_ASC"]));

        }
    }

    public partial class TrackInWindowDepkg : ChromelessWindow
    {

        public TrackInWindowDepkgViewModel TrackinDepkgViewmodel = new TrackInWindowDepkgViewModel();
        DepkgHelper _depkgHelper = new DepkgHelper();
        Regex _reDelot = new Regex(@".[0-9]{6}[-]?[0-9]{1}.[0-9]{2}.");
        private List<string> notRegistedTool = new List<string>();
        private IMapper mapper;
            public TrackInWindowDepkg()
        {

            InitializeComponent();
            TrackinDepkgViewmodel.SegementDataTable = _depkgHelper.qryWorkcenterDataTable();
            this.DataContext = TrackinDepkgViewmodel;

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
            var configuration = new MapperConfiguration(a => { a.AddProfile(new AutoMapperProfileDePkg()); });
            mapper = configuration.CreateMapper();
            return mapper.Map<IEnumerable<DataRow>, List <T>>(dt.Rows.ToList<DataRow>());
        }
        private void cmb_input_segment_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var rcvdt = _depkgHelper.getRcvlotListDataTable();

            rcvdt.Columns.Add("IsRegist");

            var toollist = rcvdt.AsEnumerable().Select(w => w.Field<string>("SPECNR")).ToList<string>();

            foreach (var item in toollist)
            {
                rcvdt.Select(string.Format("[SPECNR] = '{0}'", item)).ToList<DataRow>()
                    .ForEach(r => r["IsRegist"] = GetRegist(item));
            }

            TrackinDepkgViewmodel.RcvLotList = ReadData<DePkgRcvModelAfterValidation>(rcvdt);

            //GridRcv.ItemsSource = rcvdt;

            //Binding bindbg = new Binding();
            //bindbg.Converter = new GridTrackinDepkgColorConverter();           

            //var rowStyle = new Style { TargetType = typeof(VirtualizingCellsControl) };

            //rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.BackgroundProperty, bindbg));

            //GridRcv.RowStyle = rowStyle;
            //GridRcv.View.RefreshFilter();

        }

        private void UpdateGridRcv()
        {
            var rcvdt = _depkgHelper.getRcvlotListDataTable();

            rcvdt.Columns.Add("IsRegist");

            var toollist = rcvdt.AsEnumerable().Select(w => w.Field<string>("SPECNR")).ToList<string>();

            foreach (var item in toollist)
            {
                rcvdt.Select(string.Format("[SPECNR] = '{0}'", item)).ToList<DataRow>()
                    .ForEach(r => r["IsRegist"] = GetRegist(item));
            }

            GridRcv.ItemsSource = rcvdt;

            //Binding bindbg = new Binding();
            //bindbg.Converter = new GridTrackinDepkgColorConverter();           

            //var rowStyle = new Style { TargetType = typeof(VirtualizingCellsControl) };

            //rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.BackgroundProperty, bindbg));

            //GridRcv.RowStyle = rowStyle;
            //GridRcv.View.RefreshFilter();

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
                var user = MainWindow._mainwindowViewModel.UserList.Where(x => x.UserName == MainWindow._mainwindowViewModel.SelectedUser).First();
                if (!IsToolExist(tool))
                {
                    RegistTool(tool, lot, issample);
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
                    var seluser = MainWindow._mainwindowViewModel.SelectedUser;
                    usr = context.TbUsers.Where(x => x.UserName == seluser).FirstOrDefault();
                    inputTemp.TrackinUserId = user.UserId;

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
                        ExecuteRcv(lot);
                        UpdateGridRcv();
                    }
                    else if (lotcount != 0)
                    {
                        if (MessageBox.Show("중복된 LOT가 있습니다 추가하시겠습니까?", "중복 LOT 확인", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            context.TbUvWorkorder.AddAsync(inputTemp);
                            context.SaveChanges();
                            ExecuteRcv(lot);
                            UpdateGridRcv();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                UpdateGridRcv();
            }
        }

        private void BtnAddOnly_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedlist = GridRcv.SelectionController.SelectedRows;
            BtnAddOnly.IsEnabled = false;
            BtnExeRcv.IsEnabled = false;
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

                        var pnlqty = Convert.ToInt16(pC.GetValue(rowdata, "PannelQty"));
                        var lotdetailinfo = _depkgHelper.MesLotDetailInfoQry(lot, tool);
                        bool issample = Char.IsLetter(lot, 0) && !lot.ToLower().StartsWith("p");
                        var user = MainWindow._mainwindowViewModel.UserList.Where(x => x.UserName == MainWindow._mainwindowViewModel.SelectedUser).First();
                        if (!IsToolExist(tool))
                        {
                            RegistTool(tool, lot, issample);
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

                            inputTemp.TrackinUserId = user.UserId;

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
                            }
                            else if (lotcount != 0)
                            {
                                if (MessageBox.Show("중복된 LOT가 있습니다 추가하시겠습니까?", "중복 LOT 확인", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                                {
                                    context.TbUvWorkorder.AddAsync(inputTemp);
                                    context.SaveChanges();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    UpdateGridRcv();
                }

            }
            UpdateGridRcv();
            BtnAddOnly.IsEnabled = true;
            BtnExeRcv.IsEnabled = true;
        }
        //여러로트 받기
        private async void btn_exe_rcv_Click(object sender, RoutedEventArgs e)
        {
            BtnAddOnly.IsEnabled = false;
            BtnExeRcv.IsEnabled = false;
            var selectedlist = GridRcv.SelectionController.SelectedRows;
            var rcvlist = new List<string>();
            string lot = string.Empty;
            var user = MainWindow._mainwindowViewModel.UserList.Where(x => x.UserName == MainWindow._mainwindowViewModel.SelectedUser).First();
            foreach (var item in selectedlist)
            {
                try
                {
                    if (item.RowData != null)
                    {
                        var rowdata = item.RowData;

                        var pC = GridRcv.View.GetPropertyAccessProvider();

                        var tool = pC.GetValue(rowdata, "SPECNR") as String;
                        lot = pC.GetValue(rowdata, "LOTID") as String;

                        var pnlqty = Convert.ToInt16(pC.GetValue(rowdata, "PannelQty"));
                        var lotdetailinfo = _depkgHelper.MesLotDetailInfoQry(lot, tool);
                        bool issample = Char.IsLetter(lot, 0) && !lot.ToLower().StartsWith("p");

                        if (!IsToolExist(tool))
                        {
                            RegistTool(tool, lot, issample);
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
                            inputTemp.TrackinUserId = user.UserId;

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
                                rcvlist.Add(lot);
                                //await ExecuteRcv(lot);
                            }
                            else if (lotcount != 0)
                            {
                                if (MessageBox.Show("중복된 LOT가 있습니다 추가하시겠습니까?", "중복 LOT 확인", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                                {
                                    context.TbUvWorkorder.AddAsync(inputTemp);
                                    context.SaveChanges();
                                    rcvlist.Add(lot);
                                    //await ExecuteRcv(lot);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    UpdateGridRcv();
                }

            }
            if (rcvlist.Count == 1)
                ExecuteRcv(lot);
            else if (rcvlist.Count > 1)
                ExecuteMultiRcv(rcvlist);

            UpdateGridRcv();

            BtnAddOnly.IsEnabled = true;
            BtnExeRcv.IsEnabled = true;
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
                var ldrillinfo = _depkgHelper.MesHoleInfoQry(lot, tool);
                var ldrillinfoSeq = ldrillinfo.Where(x => x.ProcName.ToLower().Contains("uv")).GroupBy(x => x.ProcSeq).Select(x => x.Key).ToList<string>();


                foreach (var seq in ldrillinfoSeq)
                {
                    if (dbtools.Where(x => x.MesSeqCode == seq).Count() == 0)
                    {
                        var layerfr = ldrillinfo.Where(x => x.ProcSeq == seq)
                            .Select(x => x.TotalLayerFrom).FirstOrDefault();
                        var layerto = ldrillinfo.Where(x => x.ProcSeq == seq)
                            .Select(x => x.TotalLayerTo).FirstOrDefault();
                        var tempTool = GetTbUvToolinfo_DEPKG(tool, lot, seq, layerfr, layerto, issample);
                        context.TbUvToolinfo.AddAsync(tempTool);
                        context.SaveChanges();
                        //MainWindow._mainwindowViewModel.ToolInfos = new ObservableCollection<TbUvToolinfo>(context.TbUvToolinfo);
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
        private TbUvToolinfo GetTbUvToolinfo_DEPKG(string tool, string lot, string seq, string from, string to, bool issample)
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
                var holeinfoqry = _depkgHelper.MesHoleInfoQryDS(lot, tool);
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

                var lotinfo = _depkgHelper.MesLotInfoQry(lot, tool);
                var specinfo = _depkgHelper.MesSpecInfoQry(lot, tool);
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
                tempTool.CreateDate = specinfo.CreateDate;

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
                        insulinfo += item.GetValue(linfo.MaterialInfo, null).ToString() + ",";
                    }
                    tempTool.InsulInfo = insulinfo;
                    tempTool.Depth = Convert.ToDecimal(linfo.InsulThickness) + Convert.ToDecimal(linfo.CopperThickness);
                    tempTool.MainHoleSize = ldrillinfo2.Where(t => t.ProcSeq == seq).First().HoleSize.Trim();
                }

                else if (layerinfo.Where(x => x.MaterialInfo.FromLayer == from).Select(x => x.MaterialInfo.MaterialType)
                        .FirstOrDefault().Contains("C/F"))
                {
                    var bom = (Convert.ToInt16(layerinfo.Where(x => x.MaterialInfo.FromLayer == from).First().MaterialInfo.BomNumber) + 1).ToString().PadLeft(3, '0');
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

                    if (tempTool.PrcName.Contains("BVH"))
                        tempTool.HoleCount = "CS:" + tempTool.HoleCount1 + " SS:" + tempTool.HoleCount2;
                    else if (tempTool.PrcName.Contains("PTH"))
                        tempTool.HoleCount = tempTool.HoleCount2;
                }


                if (issample == true)
                { tempTool.Sample = true; }
                else if (issample != true)
                { tempTool.Sample = false; }




                var thisyear = DateTime.Now.Year.ToString().Substring(2) + "-DEP-UV-";
                //var prdidNo = context.TbUvToolinfo.Where(x => x.ProductId.Contains(thisyear)).Count() + 1;
                var prdidNo = context.TbUvToolinfo.Where(x => x.ProductId.Contains(thisyear)).OrderBy(s => s.ProductId).Select(s => Convert.ToInt16(s.ProductId.Substring(10, 4))).LastOrDefault() + 1;

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


        private void TboxLot_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (_reDelot.IsMatch(e.Key.ToString())) e.Handled = true;
            var lot = _reDelot.Match(TboxLot.Text).Value;

            foreach (var item in TrackinDepkgViewmodel.RcvLotList)
            {
                var record = item;
                if (lot.Contains("-"))
                {
                    lot.Replace("-", "");
                }
                if (record.LOTID == lot)
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
