using DAWON_UV_INVENTORY_PROTO.Models;
using DAWON_UV_INVENTORY_PROTO.ViewModels;
using DAWON_UV_INVENTORY_PROTO.Views;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Cells;
using Syncfusion.UI.Xaml.Grid.Helpers;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using MessageBox = System.Windows.MessageBox;
using Syncfusion.UI.Xaml.Grid.Converter;
using Syncfusion.XlsIO;
using Microsoft.Win32;
using Syncfusion.Windows.Shared;
using Group = Syncfusion.Data.Group;


namespace DAWON_UV_INVENTORY_PROTO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        private double _scrollbarValue;
        Regex _reDelot = new Regex(@".[0-9]{6}-[0-9]{1}.[0-9]{2}.");

        public double Scrollbarvalue
        {
            get { return _scrollbarValue; }
            set { _scrollbarValue = value; }
        }

        public static MainWindowViewModel MainwindowViewModel = new MainWindowViewModel();
        public List<TbMachine> Tbmachine = new List<TbMachine>();
        public List<TbPrctype> Tbprctype = new List<TbPrctype>();
        public static List<TbCustomer> Tbcustomer = new List<TbCustomer>();

        public static bool IsCellEditing = false;
        double _autoHeight = double.NaN;
        GridRowSizingOptions _gridRowResizingOptions = new GridRowSizingOptions();


        private DispatcherTimer? Timer { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
            MainwindowViewModel.OnViewInitialized(this);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            using (var context = new Db_Uv_InventoryContext())
            {
                MainwindowViewModel.PrcTypes = new ObservableCollection<TbPrctype>(context.TbPrctype);
                MainwindowViewModel.Customer = new ObservableCollection<TbCustomer>(context.TbCustomer);
                MainwindowViewModel.Users = context.TbUsers.Where(w => w.IsRetired == false).Select(x => x.UserName)
                    .ToList<string>();
                MainwindowViewModel.Machines = new ObservableCollection<TbMachine>(context.TbMachine);
                MainwindowViewModel.ToolInfos =  new ObservableCollection<TbUvToolinfo>(context.TbUvToolinfo);
                MainwindowViewModel.RefreshDate();
                MainwindowViewModel.SelectedCustomerWoSearch = "대덕전자(MS)";
            }

            //sftbox_product_id.LostFocus += Sftbox_product_id_LostFocus;
            this.GridToolinfo.AutoGeneratingColumn += Grid_AutoGeneratingColumn;

            this.GridWip.SelectionController = new GridCellSelectionControllerExt(GridWip);
            this.DataContext = MainwindowViewModel;
            //this.GridWip.SelectionController = new GridSelectionControllerExt(GridWip);

            update_db();
            btn_rib_wiplist_Click(this, e);

            if (!Application.Current.Windows.OfType<UserSelectPopup>().Any())
            {
                var userSelectPopup = new UserSelectPopup();
                userSelectPopup.Topmost = true;
                userSelectPopup.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                userSelectPopup.Show();
                CmbWipCust.SelectedIndex = 1;
                CmbWipOrdertype.SelectedIndex = 0;
            }

        }

        void update_db()
        {
            Timer = new DispatcherTimer(DispatcherPriority.Background);
            //framerate of 10fps

            Timer.Interval = TimeSpan.FromSeconds(180);

            Timer.Tick += new EventHandler((object? s, EventArgs a) =>
            {
                try
                {
                    if (TabWip.IsVisible == true)
                    {
                        if (IsCellEditing == false)
                        {
                            UpdateFiltered_WorkorderList();
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            });
            Timer.Start();
        }


        private void Grid_AutoGeneratingColumn(object? sender, AutoGeneratingColumnArgs e)
        {
            e.Column.TextAlignment = TextAlignment.Center;
        }

        private void btn_rib_wiplist_Click(object sender, RoutedEventArgs e)
        {
            if (TabWip.Visibility == Visibility.Hidden || TabWip.Visibility == Visibility.Collapsed)
            {
                TabWip.Visibility = Visibility.Visible;
            }

            TabControl.SelectedItem = TabWip;
            UpdateFiltered_WorkorderList();
        }

        void dataGrid_QueryRowHeight(object sender, QueryRowHeightEventArgs e)
        {
            if (((SfDataGrid) sender).GridColumnSizer.GetAutoRowHeight(e.RowIndex, _gridRowResizingOptions,
                    out _autoHeight))
            {
                if (_autoHeight > 20 && e.RowIndex > 0)
                {
                    e.Height = _autoHeight;
                    e.Handled = true;
                }
            }
        }
        //private void btn_input_proceed_Click(object sender, RoutedEventArgs e)
        //{
        //    using (var context = new Db_Uv_InventoryContext())
        //    {
        //        var input_temp = new TbUvWorkorder();

        //        //고객사 선택, 필수항목으로 입력 여부 체크
        //        if (cmb_input_cust.SelectedItem != null)
        //        {
        //            var cust = new TbCustomer();
        //            var selcust = cmb_input_cust.SelectedValue.ToString();
        //            cust = context.TbCustomer.Where(x => x.CustName == selcust).FirstOrDefault();
        //            input_temp.CustId = cust.CustId;
        //        }
        //        else if (cmb_input_cust.SelectedItem == null)
        //        { MessageBox.Show("고객사를 선택해주세요"); }

        //        //작성자 선택, 필수항목으로 입력 여부 체크
        //        if (cmb_input_user.SelectedItem != null)
        //        {
        //            var usr = new TbUsers();
        //            var seluser = cmb_input_user.SelectedValue.ToString();
        //            usr = context.TbUsers.Where(x => x.UserName == seluser).FirstOrDefault();
        //            input_temp.TrackinUser = usr;
        //        }
        //        else if (cmb_input_user.SelectedItem == null)
        //        { MessageBox.Show("작성자를 선택해주세요"); }

        //        //LOT입력, 필수항목으로 입력 여부 체크
        //        if (tbox_lotid.Text != null)
        //        {
        //            input_temp.Lotid = tbox_lotid.Text;
        //        }
        //        else if (tbox_lotid.Text != null) { MessageBox.Show("LOT를 입력해주세요"); }

        //        //툴입력, 필수항목으로 입력 여부 체크
        //        if (tblk_productid.Text != null)
        //        {
        //            input_temp.ProductId = tblk_productid.Text;
        //        }
        //        else if (tblk_productid.Text != null) { MessageBox.Show("툴번호를 입력해주세요. 없는 경우 신규 등록 필요"); }


        //        if (chkbox_issample.IsChecked == true)
        //        { input_temp.SampleOrder = true; }
        //        else if (chkbox_issample.IsChecked != true)
        //        { input_temp.SampleOrder = false; }

        //        if (cmb_input_cs_machine.SelectedItem != null)
        //            input_temp.MachineCs = cmb_input_cs_machine.SelectedItem.ToString();
        //        if (cmb_input_ss_machine.SelectedItem != null)
        //            input_temp.MachineSs = cmb_input_ss_machine.SelectedItem.ToString();

        //        input_temp.Pnlqty = Convert.ToInt16(tbox_pnlqty.Text);
        //        input_temp.LotNotes = tbox_lot_notes.Text;
        //        input_temp.CreateTime = DateTime.Now;
        //        input_temp.TrackinTime = DateTime.Now;
        //        //Search(tbox_lotid.Text);


        //        var lotcount = dt_wip.AsEnumerable().Where(x => x.Field<string>("LOT") == tbox_lotid.Text).Count();
        //        if (lotcount == 0)
        //        {
        //            context.TbUvWorkorder.AddAsync(input_temp);
        //            context.SaveChanges();
        //        }
        //        else if (lotcount != 0)
        //        {
        //            if (MessageBox.Show("중복된 LOT가 있습니다 추가하시겠습니까?", "중복 LOT 확인", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        //            {
        //                context.TbUvWorkorder.AddAsync(input_temp);
        //                context.SaveChanges();
        //            }


        //        }

        //        clearform();
        //        btn_rib_wiplist_Click(this, e);
        //    }

        //}

        private void btn_rib_customer_manage_Click(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.Windows.OfType<CustomerManageWindow>().Any())
            {
                var customerManager = new CustomerManageWindow();
                customerManager.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                customerManager.Show();
            }
        }

        private void btn_rib_user_manage_Click(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.Windows.OfType<UserManageWindow>().Any())
            {
                var userManager = new UserManageWindow();
                userManager.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                userManager.Show();
            }
        }

        private void btn_rib_machine_manage_Click(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.Windows.OfType<MachineManageWindow>().Any())
            {
                var machineManager = new MachineManageWindow();
                machineManager.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                machineManager.Show();
            }
        }

        private void btn_rib_model_query_Click(object sender, RoutedEventArgs e)
        {
            if (TabToolinfo.Visibility == Visibility.Hidden || TabToolinfo.Visibility == Visibility.Collapsed)
            {
                TabToolinfo.Visibility = Visibility.Visible;

            }

            TabControl.SelectedItem = TabToolinfo;
            using (var context = new Db_Uv_InventoryContext())
            {
                MainwindowViewModel.ToolInfos = new ObservableCollection<TbUvToolinfo>(context.TbUvToolinfo);
            }
        }


        private void btn_rib_model_regist_manual_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_rib_model_regist_auto_Click(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.Windows.OfType<ToolinfoManageAutoWindow>().Any())
            {
                var toolinfoautoManager = new ToolinfoManageAutoWindow();
                toolinfoautoManager.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                toolinfoautoManager.Show();
            }
        }


        private void grid_wip_CurrentCellEndEdit(object sender, CurrentCellEndEditEventArgs e)
        {

            System.Windows.Threading.Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() =>
            {
                var grid = sender as SfDataGrid;
                //getting GridCell 
                var cell = (grid.SelectionController.CurrentCellManager.CurrentCell.Renderer as GridCellRendererBase)
                    .CurrentCellElement;


                if (cell != null)
                {
                    object? rowdata = ((SfDataGrid) sender).View.Records.GetItemAt(e.RowColumnIndex.RowIndex - 1);
                    string? mappingName = ((SfDataGrid) sender).Columns[e.RowColumnIndex.ColumnIndex - 1].MappingName;
                    var qryid = Convert.ToInt64(((SfDataGrid) e.OriginalSender).View.GetPropertyAccessProvider()
                        .GetValue(rowdata, "Id"));
                    ;
                    var newCellValue = ((SfDataGrid) e.OriginalSender).View.GetPropertyAccessProvider()
                        .GetValue(rowdata, mappingName);
                    if (newCellValue != null)
                    {
                        using (var db = new Db_Uv_InventoryContext())
                        {
                            var result = db.TbUvWorkorder.SingleOrDefault(x => x.Id == qryid);

                            if (result != null)
                            {
                                //var tmp_result = result.GetType().GetProperty(mappingName).GetValue(result, null);

                                result.LotNotes = newCellValue.ToString();
                                db.SaveChanges();
                                UpdateFiltered_WorkorderList();
                            }
                        }
                    }

                    IsCellEditing = false;
                    //GridWip.InvalidateRowHeight(e.RowColumnIndex.RowIndex);
                    GridWip.GetVisualContainer().InvalidateMeasureInfo();
                }
            }), System.Windows.Threading.DispatcherPriority.Normal);
        }

        public void UpdateFiltered_WorkorderList()
        {
            using (var db = new Db_Uv_InventoryContext())
            {
                var customer = MainwindowViewModel.SelectedCustomerWo;
                var issample = false;
                if (MainwindowViewModel.SelectedIsSampleWo == "양산") issample = false;
                else if (MainwindowViewModel.SelectedIsSampleWo == "샘플") issample = true;
                MainwindowViewModel.WorkOrderList =
                    new ObservableCollection<ViewUvWorkorder>(db.ViewUvWorkorder.Where(x =>
                        x.CustName == customer && x.SampleOrder == issample));
            }

            //if (isGridWIPSelectionChanged)
            //    GridWip.SelectedIndex = grid_wip_selectedRowIdx;
        }

        public void UpdateFiltered_WorkorderSearchList()
        {
            using (var db = new Db_Uv_InventoryContext())
            {
                var customer = MainwindowViewModel.SelectedCustomerWoSearch;
                var issample = false;
                var datefrom = MainwindowViewModel.SelectedDateFromWoSearch.AddDays(-1);
                var dateto = MainwindowViewModel.SelectedDateToWoSearch.AddDays(1);
                if (MainwindowViewModel.SelectedIsSampleWoSearch == "양산") issample = false;
                else if (MainwindowViewModel.SelectedIsSampleWoSearch == "샘플") issample = true;

                var rcvdata = new ObservableCollection<ViewUvWorkorderDone>(db.ViewUvWorkorderDone.Where(x =>
                    x.CustName == customer && x.SampleOrder == issample && x.CreateTime >= datefrom &&
                    x.CreateTime <= dateto));

                MainwindowViewModel.WorkOrderListSearch = rcvdata;
            }
        }

        public void UpdateSpecFiltered_WorkorderSearchList()
        {
            using (var db = new Db_Uv_InventoryContext())
            {
                var lot = MainwindowViewModel.SrchInputLot;
                var tool = MainwindowViewModel.SrchInputToolNo;
                var modelname = MainwindowViewModel.SrchInputModelName;
                var datefrom = DateTime.Parse(DtpickWipsearchFrom.SelectedDate.ToString()).AddDays(-1);
                var dateto = DateTime.Parse(DtpickWipsearchTo.SelectedDate.ToString()).AddDays(1);

                bool isLot = lot.Count() > 1 ? true : false;
                bool isTool = tool.Count() > 3 ? true : false;
                bool isModelName = modelname.Count() > 3 ? true : false;

                if (isLot && isTool && isModelName)
                {
                    MainwindowViewModel.WorkOrderListSearch = new ObservableCollection<ViewUvWorkorderDone>(
                        db.ViewUvWorkorderDone.Where(
                            x => x.Lotid == lot && x.CustToolno == tool && x.CustModelname == modelname &&
                                 x.CreateTime >= datefrom && x.CreateTime <= dateto));
                }

                else if (isLot && isTool)
                {
                    MainwindowViewModel.WorkOrderListSearch = new ObservableCollection<ViewUvWorkorderDone>(
                        db.ViewUvWorkorderDone.Where(
                            x => x.Lotid == lot && x.CustToolno == tool && x.CreateTime >= datefrom &&
                                 x.CreateTime <= dateto));
                }

                else if (isLot && isModelName)
                {
                    MainwindowViewModel.WorkOrderListSearch = new ObservableCollection<ViewUvWorkorderDone>(
                        db.ViewUvWorkorderDone.Where(
                            x => x.Lotid == lot && x.CustModelname == modelname && x.CreateTime >= datefrom &&
                                 x.CreateTime <= dateto));
                }

                else if (isTool && isModelName)
                {
                    MainwindowViewModel.WorkOrderListSearch = new ObservableCollection<ViewUvWorkorderDone>(
                        db.ViewUvWorkorderDone.Where(
                            x => x.CustToolno == tool && x.CustModelname == modelname && x.CreateTime >= datefrom &&
                                 x.CreateTime <= dateto));
                }

                else if (isTool)
                {
                    MainwindowViewModel.WorkOrderListSearch = new ObservableCollection<ViewUvWorkorderDone>(
                        db.ViewUvWorkorderDone.Where(
                            x => x.CustToolno == tool && x.CreateTime >= datefrom && x.CreateTime <= dateto));
                }

                else if (isModelName)
                {
                    MainwindowViewModel.WorkOrderListSearch = new ObservableCollection<ViewUvWorkorderDone>(
                        db.ViewUvWorkorderDone.Where(
                            x => x.CustModelname == modelname && x.CreateTime >= datefrom && x.CreateTime <= dateto));
                }
                else if (isLot)
                {
                    MainwindowViewModel.WorkOrderListSearch = new ObservableCollection<ViewUvWorkorderDone>(
                        db.ViewUvWorkorderDone.Where(
                            x => x.Lotid == lot && x.CreateTime >= datefrom && x.CreateTime <= dateto));
                }

            }
        }

        private void cmb_wip_cust_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UpdateFiltered_WorkorderList();
        }

        private void cmb_wip_ordertype_SelectionChanged(object sender,
            System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UpdateFiltered_WorkorderList();
        }

        private void cmb_finish_ordertype_SelectionChanged(object sender,
            System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void btn_qry_search_Click(object sender, RoutedEventArgs e)
        {

            UpdateFiltered_WorkorderSearchList();
        }

        private void btn_rib_wip_history_Click(object sender, RoutedEventArgs e)
        {
            if (TabQryFinish.Visibility == Visibility.Hidden || TabQryFinish.Visibility == Visibility.Collapsed)
            {
                TabQryFinish.Visibility = Visibility.Visible;
            }

            TabControl.SelectedItem = TabQryFinish;
            MainwindowViewModel.RefreshDate();


        }

        private void tbox_wip_selected_tool_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (((TextBox) sender).Text == string.Empty)
                return;
            else
            {
                try
                {
                    Clipboard.SetText(((TextBox) sender).Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void tbox_wip_selected_lot_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (((TextBox) sender).Text == string.Empty)
                return;
            else
            {
                try
                {
                    Clipboard.SetText(((TextBox) sender).Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void SfMultiColumnDropDownControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Combo_Lottype_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            if (GridWip.SelectedItem != null && ((ComboBox) sender).IsDropDownOpen)
            {
                var rowdata = GridWip.SelectedItem;
                var pC = GridWip.View.GetPropertyAccessProvider();

                var qryid = Convert.ToInt64(pC.GetValue(rowdata, "Id").ToString());
                var changevalue = ((ComboBoxItem) ((ComboBox) sender).SelectedItem).Content;

                using (var db = new Db_Uv_InventoryContext())
                {
                    var result = db.TbUvWorkorder.SingleOrDefault(x => x.Id == qryid);

                    if (result != null)
                    {
                        result.LotType = (string) changevalue;
                        Debug.WriteLine(changevalue);
                        db.SaveChanges();
                    }
                }
            }
        }

        //private void _combo_cs_ItemSelectionChanged(object sender,
        //    Xceed.Wpf.Toolkit.Primitives.ItemSelectionChangedEventArgs e)
        //{
        //    if (GridWip.SelectedItem != null)
        //    {
        //        var rowdata = GridWip.SelectedItem;
        //        var pC = GridWip.View.GetPropertyAccessProvider();
        //        var qryid = Convert.ToInt64(pC.GetValue(rowdata, "Id").ToString());
        //        var changevalue = ((CheckComboBox) sender).SelectedValue.ToString();
        //        //var changevalue = ((CheckComboBox)sender).SelectedValue.ToString();

        //        using (var db = new Db_Uv_InventoryContext())
        //        {
        //            var result = db.TbUvWorkorder.SingleOrDefault(x => x.Id == qryid);

        //            if (result != null)
        //            {
        //                result.MachineCs = changevalue;
        //                Debug.WriteLine(changevalue);
        //                db.SaveChanges();
        //                UpdateFiltered_WorkorderList();
        //                mainwindowViewModel.WorkOrderList =
        //                new ObservableCollection<ViewUvWorkorder>(db.ViewUvWorkorder);
        //            }
        //        }
        //    }
        //}

        //private void _combo_ss_ItemSelectionChanged(object sender,
        //    Xceed.Wpf.Toolkit.Primitives.ItemSelectionChangedEventArgs e)
        //{
        //    if (GridWip.SelectedItem != null)
        //    {
        //        var rowdata = GridWip.SelectedItem;
        //        var pC = GridWip.View.GetPropertyAccessProvider();
        //        var qryid = Convert.ToInt64(pC.GetValue(rowdata, "Id").ToString());
        //        var changevalue = ((CheckComboBox) sender).SelectedValue.ToString();
        //        using (var db = new Db_Uv_InventoryContext())
        //        {
        //            var result = db.TbUvWorkorder.SingleOrDefault(x => x.Id == qryid);
        //            if (result != null)
        //            {
        //                result.MachineSs = changevalue;
        //                Debug.WriteLine(changevalue);
        //                db.SaveChanges();
        //                UpdateFiltered_WorkorderList();
        //            }
        //        }
        //    }
        //}

        private void btn_search_wip_cond_qry_Click(object sender, RoutedEventArgs e)
        {
            UpdateSpecFiltered_WorkorderSearchList();
        }

        private void btn_grid_wip_xls_Click(object sender, RoutedEventArgs e)
        {
            var options = new ExcelExportingOptions();
            options.ExcelVersion = ExcelVersion.Excel2016;
            options.ExcludeColumns.Add("ExecuteTrackout");
            var excelEngine = GridWip.ExportToExcel(GridWip.View, options);
            var workBook = excelEngine.Excel.Workbooks[0];
            string filePath = string.Empty;



            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.InitialDirectory = System.Environment.CurrentDirectory;
            saveDlg.Filter = "Excel2013 (*.xlsx)|*.xlsx";
            saveDlg.FileName = @"재공현황_" + DateTime.Now.ToShortDateString();
            saveDlg.ShowDialog();
            workBook.SaveAs(saveDlg.FileName);

            if (MessageBox.Show("엑셀 파일을 열까요?", "파일이 생성됐습니다",
                    MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {

                //Launching the Excel file using the default Application.[MS Excel Or Free ExcelViewer]
                var p = new Process();
                p.StartInfo = new ProcessStartInfo(saveDlg.FileName)
                {
                    UseShellExecute = true
                };
                p.Start();
            }
        }

        private void btn_grid_wipsearch_xls_Click(object sender, RoutedEventArgs e)
        {
            var options = new ExcelExportingOptions();
            options.ExcelVersion = ExcelVersion.Excel2016;

            var excelEngine = GridFinish.ExportToExcel(GridFinish.View, options);
            var workBook = excelEngine.Excel.Workbooks[0];
            string filePath = string.Empty;

            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.InitialDirectory = System.Environment.CurrentDirectory;
            saveDlg.Filter = "Excel2013 (*.xlsx)|*.xlsx";
            saveDlg.FileName = @"재공이력_" + DateTime.Now.ToString("yyMMdd_HHmmsss");
            saveDlg.ShowDialog();
            workBook.SaveAs(saveDlg.FileName);

            if (MessageBox.Show("엑셀 파일을 열까요?", "파일이 생성됐습니다",
                    MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {

                //Launching the Excel file using the default Application.[MS Excel Or Free ExcelViewer]
                var p = new Process();
                p.StartInfo = new ProcessStartInfo(saveDlg.FileName)
                {
                    UseShellExecute = true
                };
                p.Start();
            }
        }

        private void BtnInputLotDEMS_OnClick(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.Windows.OfType<TrackInWindowDems>().Any())
            {
                var trackinwindow = new TrackInWindowDems();
                trackinwindow.Topmost = true;
                trackinwindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                trackinwindow.Show();
            }
        }


        private void BtnInputLot_OnClick(object sender, RoutedEventArgs e)
        {
            if (MainwindowViewModel.SelectedCustomerWo == "대덕전자(MS)")
            {
                if (!Application.Current.Windows.OfType<TrackInWindowDems>().Any())
                {
                    var trackinwindow = new TrackInWindowDems();
                    trackinwindow.Topmost = true;
                    trackinwindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                    trackinwindow.Show();
                }
            }
            else if (MainwindowViewModel.SelectedCustomerWo == "대덕전자(PKG)")
            {
                if (!Application.Current.Windows.OfType<TrackInWindowDepkg>().Any())
                {
                    var trackinwindow = new TrackInWindowDepkg();
                    trackinwindow.Topmost = true;
                    trackinwindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                    trackinwindow.Show();
                }
            }
        }



        #region 업체별 재공현황 버튼

        private void BtnWipDEMS_OnClick(object sender, RoutedEventArgs e)
        {
            MainwindowViewModel.SelectedCustomerWo = "대덕전자(MS)";
            UpdateFiltered_WorkorderList();
        }

        private void BtnWipDEPKG_OnClick(object sender, RoutedEventArgs e)
        {
            MainwindowViewModel.SelectedCustomerWo = "대덕전자(PKG)";
            UpdateFiltered_WorkorderList();
        }

        private void BtnWipYPE_OnClick(object sender, RoutedEventArgs e)
        {
            MainwindowViewModel.SelectedCustomerWo = "영풍전자";
            UpdateFiltered_WorkorderList();
        }

        private void BtnWipBH_OnClick(object sender, RoutedEventArgs e)
        {
            MainwindowViewModel.SelectedCustomerWo = "BHFLEX";
            UpdateFiltered_WorkorderList();
        }

        private void BtnWipIFC_OnClick(object sender, RoutedEventArgs e)
        {
            MainwindowViewModel.SelectedCustomerWo = "인터플렉스";
            UpdateFiltered_WorkorderList();
        }

        private void BtnWipSEMCO_OnClick(object sender, RoutedEventArgs e)
        {
            MainwindowViewModel.SelectedCustomerWo = "삼성전기";
            UpdateFiltered_WorkorderList();
        }

        private void BtnWipNFT_OnClick(object sender, RoutedEventArgs e)
        {
            MainwindowViewModel.SelectedCustomerWo = "뉴프렉스";
            UpdateFiltered_WorkorderList();
        }

        private void BtnWipSI_OnClick(object sender, RoutedEventArgs e)
        {
            MainwindowViewModel.SelectedCustomerWo = "SIFLEX";
            UpdateFiltered_WorkorderList();
        }

        #endregion

        #region 업체별 입출고 이력 버튼

        private void BtnWipSearchDEMS_OnClick(object sender, RoutedEventArgs e)
        {
            MainwindowViewModel.SelectedCustomerWoSearch = "대덕전자(MS)";
            UpdateFiltered_WorkorderSearchList();
        }

        private void BtnWipSearchDEPKG_OnClick(object sender, RoutedEventArgs e)
        {
            MainwindowViewModel.SelectedCustomerWoSearch = "대덕전자(PKG)";
            UpdateFiltered_WorkorderSearchList();
        }

        private void BtnWipSearchYPE_OnClick(object sender, RoutedEventArgs e)
        {
            MainwindowViewModel.SelectedCustomerWoSearch = "영풍전자";
            UpdateFiltered_WorkorderSearchList();
        }

        private void BtnWipSearchBH_OnClick(object sender, RoutedEventArgs e)
        {
            MainwindowViewModel.SelectedCustomerWoSearch = "BHFLEX";
            UpdateFiltered_WorkorderSearchList();
        }

        private void BtnWipSearchIFC_OnClick(object sender, RoutedEventArgs e)
        {
            MainwindowViewModel.SelectedCustomerWoSearch = "인터플렉스";
            UpdateFiltered_WorkorderSearchList();
        }

        private void BtnWipSearchSEMCO_OnClick(object sender, RoutedEventArgs e)
        {
            MainwindowViewModel.SelectedCustomerWoSearch = "삼성전기";
            UpdateFiltered_WorkorderSearchList();
        }

        private void BtnWipSearchNFT_OnClick(object sender, RoutedEventArgs e)
        {
            MainwindowViewModel.SelectedCustomerWoSearch = "뉴프렉스";
            UpdateFiltered_WorkorderSearchList();
        }

        private void BtnWipSearchSI_OnClick(object sender, RoutedEventArgs e)
        {
            MainwindowViewModel.SelectedCustomerWoSearch = "SIFLEX";
            UpdateFiltered_WorkorderSearchList();
        }

        private void BtnWipSearchALL_OnClick(object sender, RoutedEventArgs e)
        {
            MainwindowViewModel.SelectedCustomerWoSearch = "전체조회";
            using (var db = new Db_Uv_InventoryContext())
            {
                var issample = false;
                var datefrom = DateTime.Parse(DtpickWipsearchFrom.SelectedDate.ToString()).AddDays(-1);
                var dateto = DateTime.Parse(DtpickWipsearchTo.SelectedDate.ToString());
                if (MainwindowViewModel.SelectedIsSampleWoSearch == "양산") issample = false;
                else if (MainwindowViewModel.SelectedIsSampleWoSearch == "샘플") issample = true;
                MainwindowViewModel.WorkOrderListSearch = new ObservableCollection<ViewUvWorkorderDone>(
                    db.ViewUvWorkorderDone.Where(x =>
                        x.SampleOrder == issample && x.CreateTime >= datefrom && x.CreateTime <= dateto));
            }
        }

        #endregion


        private void BtnSearchWipCondQry_OnClick(object sender, RoutedEventArgs e)
        {
            using (var db = new Db_Uv_InventoryContext())
            {
                var keyword = string.Empty;
                var tempKeyword = TboxSearchKeyWord.Text;
                if (MainwindowViewModel.SelectedCustomerWoSearch.Contains("대덕전자") && _reDelot.IsMatch(tempKeyword))
                    keyword = TboxSearchKeyWord.Text.Replace("-", "");
                else
                    keyword = TboxSearchKeyWord.Text;

                var datefrom = DateTime.Parse(DtpickWipsearchFrom.SelectedDate.ToString()).AddDays(-1);
                var dateto = DateTime.Parse(DtpickWipsearchTo.SelectedDate.ToString()).AddDays(1);

                MainwindowViewModel.WorkOrderListSearch = new ObservableCollection<ViewUvWorkorderDone>(
                    db.ViewUvWorkorderDone.Where(x =>
                        (x.TrackoutTime >= datefrom && x.TrackoutTime <= dateto && x.IsDone == true) &&
                        (x.Lotid.Contains(keyword) || x.CustModelname.Contains(keyword) ||
                         x.CustToolno.Contains(keyword))));

            }
        }


        private void grid_wip_CurrentCellBeginEdit(object sender, CurrentCellBeginEditEventArgs e)
        {
            IsCellEditing = true;
        }

        private void ComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            IsCellEditing = true;
        }

        private void ComboBox_LostFocus_SS(object sender, RoutedEventArgs e)
        {
            IsCellEditing = false;

        }

        private void ComboBox_LostFocus_CS(object sender, RoutedEventArgs e)
        {
            IsCellEditing = false;

        }

        private void Cmb_lottype_OnLostFocus(object sender, RoutedEventArgs e)
        {
            IsCellEditing = false;
        }

        private void Cmb_lottype_OnGotFocus(object sender, RoutedEventArgs e)
        {
            IsCellEditing = true;
        }

        private void GridwipSsCombo_OnGotFocus(object sender, RoutedEventArgs e)
        {
            IsCellEditing = true;
        }

        //private void GridwipSsCombo_OnLostFocus(object sender, RoutedEventArgs e)
        //{
        //    if (GridWip.SelectedItem != null)
        //    {

        //        var rowdata = (e.OriginalSource as ComboBoxItemAdv);
        //        var pC = GridWip.View.GetPropertyAccessProvider();

        //        var qryid = Convert.ToInt64(pC.GetValue(rowdata, "Id").ToString());

        //        var changevalue = ((CheckComboBox)sender).SelectedValue.ToString();

        //        using (var db = new Db_Uv_InventoryContext())
        //        {
        //            var result = db.TbUvWorkorder.SingleOrDefault(x => x.Id == qryid);

        //            if (result != null)
        //            {
        //                result.MachineSs = changevalue;
        //                Debug.WriteLine(changevalue);
        //                db.SaveChanges();
        //                UpdateFiltered_WorkorderList();

        //            }
        //        }
        //    }
        //    IsCellEditing = false;
        //}

        private void GridWip_OnLostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonWipSrchLot_OnClick(object sender, RoutedEventArgs e)
        {
            GridWip.SelectedItems.Clear();
            var tempkeyword = tblksearchlot.Text;

            if (MainwindowViewModel.SelectedCustomerWoSearch.Contains("대덕전자") && _reDelot.IsMatch(tempkeyword))
                tblksearchlot.Text = tblksearchlot.Text.Replace("-", "");
            else
                tblksearchlot.Text = tblksearchlot.Text;
            
            //GridWip.SearchHelper.Search(tblksearchlot.Text);
            GridWip.SearchHelper.FindNext(tblksearchlot.Text);
            this.GridWip.SelectionController.MoveCurrentCell(this.GridWip.SearchHelper.CurrentRowColumnIndex);
            //var list = GridWip.SearchHelper.GetSearchRecords();
            //if (list != null)
            //{
            //    int recordIndex = GridWip.ResolveToRecordIndex(GridWip.ResolveToRowIndex(list[0].Record));
            //    GridWip.SelectedIndex = recordIndex;
            //    //this.GridWip.SelectionController.MoveCurrentCell(this.GridWip.SearchHelper.CurrentRowColumnIndex);

            //}


        }

        private void ButtonWipSrchClear_OnClick(object sender, RoutedEventArgs e)
        {
            tblksearchlot.Text = string.Empty;
            ButtonWipSrchLot_OnClick(sender,e);
        }
    }

}
