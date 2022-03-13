using ConnectorDEMS;
using DAWON_UV_INVENTORY_PROTO.Models;
using DAWON_UV_INVENTORY_PROTO.ViewModels;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Xml;

namespace DAWON_UV_INVENTORY_PROTO.Views
{
    /// <summary>
    /// ToolinfoManageWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ToolinfoManageAutoWindow : ChromelessWindow
    {
        public ToolinfoManagerAutoWindowViewmodel ToolinfoautoViewmodel = new ToolinfoManagerAutoWindowViewmodel();
        DemsHelper _demsClient = new DemsHelper();
        public ToolinfoManageAutoWindow()
        {
            InitializeComponent();

            ToolinfoautoViewmodel.Customer = MainWindow._mainwindowViewModel.Customer;
            ToolinfoautoViewmodel.SegementDataTable = de_ms_qry_workcenter();
            this.DataContext = ToolinfoautoViewmodel;
        }

        private void cmb_input_customer_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (CmbInputCustomer.Text.ToString() == "대덕전자(MS)")
            {


            }
        }
        private DataTable de_ms_qry_lotlist(string workcenter)
        {
            try
            {
                var client = new DemsOiClient();


                XmlDocument qryLotlist = new XmlDocument();
                var resultLotlistXml = new XmlDocument();

                qryLotlist.LoadXml(Resource1.Qry_Lotlist);

                var workcenterchange = qryLotlist.SelectSingleNode("//message//body//BINDV//WORKCENTERID");
                workcenterchange.InnerText = workcenter;


                var qryLotlistMsg = new IMesRuleService.MessageData();
                qryLotlistMsg.CODE = "true";
                qryLotlistMsg.COMMAND = "GetStoredProcedureResult";
                qryLotlistMsg.COMMANDTYPE = "true";
                qryLotlistMsg.OBJECT = qryLotlist.OuterXml;

                var resultLotlist = client.ExecCommandAsync(qryLotlistMsg);
                resultLotlistXml.LoadXml(resultLotlist.Result.OBJECT.ToString());
                //result_lotlist_xml.LoadXml(result_lotlist);
                var sr = new StringReader(resultLotlistXml["message"]["body"]["DATASET"]["DATALIST"].OuterXml);

                var dsLotlist = new DataSet();
                dsLotlist.ReadXml(sr);
                DataTable dt = dsLotlist.Tables[0];

                var dt2 = dt.DefaultView.ToTable(false, new string[] { "MESPROCESSSTATENAME", "PROCESSSEGMENTNAME", "ORDERTYPE", "LOTID", "MODEL", "REV", "SPECNR", "PANNELQTY", "VORNR" });
                dt2.Columns["MESPROCESSSTATENAME"].ColumnName = "재공상태";
                dt2.Columns["PROCESSSEGMENTNAME"].ColumnName = "공정명";
                dt2.Columns["ORDERTYPE"].ColumnName = "오더유형";
                dt2.Columns["LOTID"].ColumnName = "LOT";
                dt2.Columns["MODEL"].ColumnName = "모델명";
                dt2.Columns["REV"].ColumnName = "리비전";
                dt2.Columns["SPECNR"].ColumnName = "TOOL";
                dt2.Columns["PANNELQTY"].ColumnName = "PNL";
                dt2.Columns["VORNR"].ColumnName = "공순";


                return dt2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private DataTable de_ms_qry_lotlist_all(string workcenter)
        {
            try
            {
                var client = new DemsOiClient();


                XmlDocument qryLotlist = new XmlDocument();
                var resultLotlistXml = new XmlDocument();

                qryLotlist.LoadXml(Resource1.Qry_Lotlist);

                var workcenterchange = qryLotlist.SelectSingleNode("//message//body//BINDV//WORKCENTERID");
                workcenterchange.InnerText = workcenter;


                var qryLotlistMsg = new IMesRuleService.MessageData();
                qryLotlistMsg.CODE = "true";
                qryLotlistMsg.COMMAND = "GetStoredProcedureResult";
                qryLotlistMsg.COMMANDTYPE = "true";
                qryLotlistMsg.OBJECT = qryLotlist.OuterXml;

                var resultLotlist = client.ExecCommandAsync(qryLotlistMsg);
                resultLotlistXml.LoadXml(resultLotlist.Result.OBJECT.ToString());
                //result_lotlist_xml.LoadXml(result_lotlist);
                var sr = new StringReader(resultLotlistXml["message"]["body"]["DATASET"]["DATALIST"].OuterXml);

                var dsLotlist = new DataSet();
                dsLotlist.ReadXml(sr);
                DataTable dt = dsLotlist.Tables[0];




                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private DataTable de_ms_qry_workcenter()
        {
            try
            {
                var client = new DemsOiClient();

                XmlDocument qryWorkcenter = new XmlDocument();
                var resultWorkcenterXml = new XmlDocument();

                qryWorkcenter.LoadXml(Resource1.Qry_CboWorkCenter);

                var qryWorkcenterMsg = new IMesRuleService.MessageData();
                qryWorkcenterMsg.CODE = "true";
                qryWorkcenterMsg.COMMAND = "GetQueryResult";
                qryWorkcenterMsg.COMMANDTYPE = "true";
                qryWorkcenterMsg.OBJECT = qryWorkcenter.OuterXml;

                var resultWorkcenter = client.ExecCommandAsync(qryWorkcenterMsg);
                resultWorkcenterXml.LoadXml(resultWorkcenter.Result.OBJECT.ToString());
                //result_lotlist_xml.LoadXml(result_lotlist);
                var sr = new StringReader(resultWorkcenterXml["message"]["body"]["DATALIST"].OuterXml);

                var dsWorkcenter = new DataSet();
                dsWorkcenter.ReadXml(sr);
                DataTable dt = dsWorkcenter.Tables[0];
                return dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private void cmb_input_segment_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (CmbInputCustomer.Text.ToString() == "대덕전자(MS)")
            {
                GridMeslotlist.ItemsSource = de_ms_qry_lotlist(ToolinfoautoViewmodel.WorkcenterId).DefaultView;
            }
        }

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
        private TbUvToolinfo GetTbUvToolinfo_DE_MS_lbcut(string tool, string workcenter, string lot, string seq, bool issample)
        {
            var tempTool = new TbUvToolinfo();

            //try
            //{

            using (var context = new Db_Uv_InventoryContext())

            {
                var lotdetailinfo = _demsClient.MesLotDetailInfoQry(lot, tool);
                var lotinfo = _demsClient.MesLotInfoQry(lot);
                var specinfo = _demsClient.MesSpecInfoQry(tool, lotdetailinfo.Seqnr);
                var lbcutinfo = _demsClient.MesLBodyCutInfoQry(tool, lotdetailinfo.Seqnr);

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
                tempTool.Depth = Convert.ToDecimal(lbcutinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode).First().Depth);
                tempTool.ToolNotes = "가공면:" + lbcutinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode).First().Side + " Tool순번:" + lbcutinfo.Where(t => t.ProcSeq == tempTool.MesSeqCode).First().ToolSeq; ;

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
            //}

            //catch (Exception ex)
            //{ MessageBox.Show(ex.Message); }

            return tempTool;
        }
        private void clear_form()
        {
            TboxInputToolno.Text = string.Empty;
            ChkboxIssample.IsChecked = false;
            TboxInputLotno.Text = string.Empty;

        }
        private void grid_meslotlist_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            clear_form();

            var rowData = GridMeslotlist.SelectedItem;
            var pC = this.GridMeslotlist.View.GetPropertyAccessProvider();

            var tool = pC.GetValue(rowData, "TOOL") as String;
            var lot = pC.GetValue(rowData, "LOT") as String;
            var messeq = pC.GetValue(rowData, "공순") as String;
            var mesprcname = pC.GetValue(rowData, "공정명") as String;
            TboxInputLotno.Text = lot;
            TboxInputToolno.Text = tool;

        }

        private void btn_add_toolinfo_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            using (var context = new Db_Uv_InventoryContext())
            {
                var tool = TboxInputToolno.Text;
                var inputLot = TboxInputLotno.Text;
                var workcenter = ToolinfoautoViewmodel.WorkcenterId;
                var lot = inputLot.Contains("-") ? inputLot.Replace("-", string.Empty) : inputLot;
                var lotdetailinfo = _demsClient.MesLotDetailInfoQry(lot, tool);
                bool issample = false;
                if (Char.IsLetter(lot, 0) && !lot.ToLower().StartsWith("p"))
                {
                    issample = true;
                }
                var tools = context.TbUvToolinfo.Where(w => w.CustToolno == tool);
                var ldrillinfoSeq = _demsClient.MesHoleInfoQry(tool).Where(n => n.ProcName.Contains("Uv") || n.ProcName.Contains("L.Skive")).GroupBy(x => x.ProcSeq).Select(x => x.Key).ToList<string>();
                var lbcutinfo = _demsClient.MesLBodyCutInfoQry(tool, lotdetailinfo.Seqnr);
                var lbcutinfoSeq = lbcutinfo.GroupBy(x => x.ProcSeq).Select(x => x.Key).ToList<string>();

                foreach (var seq in ldrillinfoSeq)
                {
                    if (tools.Where(x => x.MesSeqCode == seq).Count() == 0)
                    {
                        var tempTool = GetTbUvToolinfo_DE_MS(tool, workcenter, lot, seq, issample);
                        context.TbUvToolinfo.AddAsync(tempTool);
                        context.SaveChanges();
                        MainWindow._mainwindowViewModel.ToolInfos = new ObservableCollection<TbUvToolinfo>(context.TbUvToolinfo);
                    }

                    else if (tools.Where(x => x.MesSeqCode == seq).Count() > 0)
                    {
                        continue;
                    }
                }
                if (lbcutinfoSeq[0] != "")
                {
                    foreach (var seq in lbcutinfoSeq)
                    {

                        if (tools.Where(x => x.MesSeqCode == seq).Count() == 0)
                        {
                            var tempTool = GetTbUvToolinfo_DE_MS_lbcut(tool, workcenter, lot, seq, issample);
                            context.TbUvToolinfo.AddAsync(tempTool);
                            context.SaveChanges();
                            MainWindow._mainwindowViewModel.ToolInfos = new ObservableCollection<TbUvToolinfo>(context.TbUvToolinfo);
                        }

                        else if (tools.Where(x => x.MesSeqCode == seq).Count() > 0)
                        {
                            continue;
                        }
                    }
                }

                clear_form();



            }
            //}
            //catch (Exception ex)
            //{ MessageBox.Show(ex.Message); }
        }
    }
}
