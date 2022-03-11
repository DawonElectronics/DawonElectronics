using DAWON_UV_INVENTORY_PROTO.Models;
using DAWON_UV_INVENTORY_PROTO.ViewModels;
using DAWON_UV_INVENTORY_PROTO.Views;
using Microsoft.Win32;
using ObjectsComparer;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Cells;
using Syncfusion.UI.Xaml.Grid.Converter;
using Syncfusion.UI.Xaml.Grid.Helpers;
using Syncfusion.Windows.Tools.Controls;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using MessageBox = System.Windows.MessageBox;

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
        DateTime starttime1;
        DateTime endtime1;
        public static MainWindowViewModel _mainwindowViewModel = new MainWindowViewModel();
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
            _mainwindowViewModel.OnViewInitialized(this);

            System.Collections.ObjectModel.ObservableCollection<Syncfusion.Windows.Tools.Controls.CustomColor> colors = new System.Collections.ObjectModel.ObservableCollection<Syncfusion.Windows.Tools.Controls.CustomColor>();
            colors.Add(new CustomColor() { Color = Colors.White, ColorName = "White" });
            colors.Add(new CustomColor() { Color = Colors.Yellow, ColorName = "Yellow" });
            colors.Add(new CustomColor() { Color = Colors.LightCoral, ColorName = "Light Coral" });
            colors.Add(new CustomColor() { Color = Color.FromRgb(255, 192, 0) });
            colors.Add(new CustomColor() { Color = Color.FromRgb(83, 141, 213) });
            colors.Add(new CustomColor() { Color = Color.FromRgb(220, 120, 120) });

            ((ColorPickerPalette)GridWip.RecordContextMenu.Items[7]).CustomColorsCollection = colors;

        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            using (var context = new Db_Uv_InventoryContext())
            {
                _mainwindowViewModel.PrcTypes = new ObservableCollection<TbPrctype>(context.TbPrctype);
                _mainwindowViewModel.Customer = new ObservableCollection<TbCustomer>(context.TbCustomer);
                _mainwindowViewModel.UserList = new ObservableCollection<TbUsers>(context.TbUsers);
                _mainwindowViewModel.Users = _mainwindowViewModel.UserList.Where(w => w.IsRetired == false).Select(x => x.UserName)
                    .ToList<string>();
                _mainwindowViewModel.Machines = new ObservableCollection<TbMachine>(context.TbMachine);
                _mainwindowViewModel.ToolInfos = new ObservableCollection<TbUvToolinfo>(context.TbUvToolinfo);
                _mainwindowViewModel.RefreshDate();
                _mainwindowViewModel.SelectedCustomerWoSearch = "대덕전자(MS)";
            }

            //sftbox_product_id.LostFocus += Sftbox_product_id_LostFocus;
            this.GridToolinfo.AutoGeneratingColumn += Grid_AutoGeneratingColumn;

            this.GridWip.SelectionController = new GridCellSelectionControllerExt(GridWip);
            this.DataContext = _mainwindowViewModel;
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

            GridWip.ItemsSourceChanged += GridWip_ItemsSourceChanged;

        }

        private void GridWip_ItemsSourceChanged(object? sender, GridItemsSourceChangedEventArgs e)
        {
            var time2 = DateTime.Now;

            Debug.WriteLine("grid rendered  " + (time2 - starttime1).TotalMilliseconds);

        }



        void update_db()
        {
            Timer = new DispatcherTimer(DispatcherPriority.Background);
            //framerate of 10fps

            Timer.Interval = TimeSpan.FromSeconds(120);

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
            if (((SfDataGrid)sender).GridColumnSizer.GetAutoRowHeight(e.RowIndex, _gridRowResizingOptions,
                    out _autoHeight))
            {
                if (_autoHeight > 20 && e.RowIndex > 0)
                {
                    e.Height = _autoHeight;
                    e.Handled = true;
                }
            }
        }

        //수기 입력 버튼
        private void btn_input_proceed_Click(object sender, RoutedEventArgs e)
        {
            btn_input_proceed.IsEnabled = false;
            var user = MainWindow._mainwindowViewModel.UserList.Where(x => x.UserName == MainWindow._mainwindowViewModel.SelectedUser).First();
            var isvalid = true;
            using (var context = new Db_Uv_InventoryContext())
            {
                var input_temp = new TbUvWorkorder();

                //고객사 선택, 필수항목으로 입력 여부 체크
                var cust = new TbCustomer();

                cust = _mainwindowViewModel.Customer.Where(x => x.CustName == _mainwindowViewModel.SelectedCustomerWo).FirstOrDefault();
                input_temp.CustId = cust.CustId;

                input_temp.TrackinUserId = user.UserId;

                //LOT입력, 필수항목으로 입력 여부 체크
                if (tbox_lotid.Text != null && tbox_lotid.Text.Length > 2)
                {
                    input_temp.Lotid = tbox_lotid.Text;
                }
                else if (tbox_lotid.Text == null || tbox_lotid.Text.Length < 2)
                {
                    MessageBox.Show("LOT를 입력해주세요");
                    isvalid = false;
                    btn_input_proceed.IsEnabled = true;
                    return;
                }

                //툴입력, 필수항목으로 입력 여부 체크
                if (tblk_productid.Text != null && tblk_productid.Text.Length > 7)
                {
                    input_temp.ProductId = tblk_productid.Text;
                }
                else if (tblk_productid.Text.Length < 7) 
                {
                    MessageBox.Show("툴번호를 입력해주세요. 없는 경우 신규 등록 필요"); 
                    isvalid = false;
                    btn_input_proceed.IsEnabled = true;
                    return; 
                }

                if (!tblk_productid.Text.Contains(cust.CustCode))
                {
                    MessageBox.Show("로트 업체명과 툴 업체가 일치 하지 않습니다");
                    isvalid = false;
                    return;
                }

                if (chkbox_issample.IsChecked == true)
                { input_temp.SampleOrder = true; }
                else if (chkbox_issample.IsChecked != true)
                { input_temp.SampleOrder = false; }

                if(tbox_pnlqty.Text.Length>0) input_temp.Pnlqty = Convert.ToInt16(tbox_pnlqty.Text);
                input_temp.LotNotes = tbox_lot_notes.Text;
                input_temp.CreateTime = DateTime.Now;
                input_temp.TrackinTime = DateTime.Now;
                //Search(tbox_lotid.Text);

                var pid = tblk_productid.Text;
                var lot = tbox_lotid.Text;

                var lotcount = context.TbUvWorkorder.Where(x => x.Lotid == lot && x.IsDone == false && x.ProductId == pid).Count();
                if (lotcount == 0 && isvalid)
                {
                    context.TbUvWorkorder.AddAsync(input_temp);
                    context.SaveChanges();
                }
                else if (lotcount != 0 && isvalid)
                {
                    if (MessageBox.Show("중복된 LOT가 있습니다 추가하시겠습니까?", "중복 LOT 확인", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        context.TbUvWorkorder.AddAsync(input_temp);
                        context.SaveChanges();
                    }
                }
                tbox_lotid.Text = string.Empty;
                UpdateFiltered_WorkorderList();
            }
            btn_input_proceed.IsEnabled = true;
        }



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
                _mainwindowViewModel.ToolInfos = new ObservableCollection<TbUvToolinfo>(context.TbUvToolinfo);
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
                    object? rowdata = ((SfDataGrid)sender).View.Records.GetItemAt(e.RowColumnIndex.RowIndex - 1);
                    string? mappingName = ((SfDataGrid)sender).Columns[e.RowColumnIndex.ColumnIndex - 1].MappingName;
                    var qryid = Convert.ToInt64(((SfDataGrid)e.OriginalSender).View.GetPropertyAccessProvider()
                        .GetValue(rowdata, "Id"));
                    ;
                    var newCellValue = ((SfDataGrid)e.OriginalSender).View.GetPropertyAccessProvider()
                        .GetValue(rowdata, mappingName);
                    if (newCellValue != null)
                    {
                        using (var db = new Db_Uv_InventoryContext())
                        {
                            var result = db.TbUvWorkorder.SingleOrDefault(x => x.Id == qryid);

                            if (result != null && mappingName == "LotNotes")
                            {
                                result.LotNotes = newCellValue.ToString();
                                db.SaveChanges();
                                UpdateFiltered_WorkorderList();
                            }

                            //호기 규칙(D/I/R 00) 또는 빈칸(배정삭제)시 적용
                            else if (result != null && mappingName == "MachineCs")
                            {

                                Regex regex = new Regex("^[DIR][0-9]{2}");

                                if (regex.IsMatch(newCellValue.ToString()) || newCellValue.ToString().Length < 1)
                                {
                                    result.MachineCs = newCellValue.ToString();
                                    db.SaveChanges();
                                }

                                UpdateFiltered_WorkorderList();
                            }

                            else if (result != null && mappingName == "MachineSs")
                            {
                                Regex regex = new Regex("^[DIR][0-9]{2}");

                                if (regex.IsMatch(newCellValue.ToString()) || newCellValue.ToString().Length < 1)
                                {
                                    result.MachineSs = newCellValue.ToString();
                                    db.SaveChanges();
                                }

                                UpdateFiltered_WorkorderList();
                            }

                            else if (result != null && mappingName == "Pnlqty")
                            {
                                result.LotHistory += "\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss") + "\t" + _mainwindowViewModel.SelectedUser + "\t[수량변경]\t변경 전:" + result.Pnlqty + "\t변경 후:" + Convert.ToInt16(newCellValue.ToString());
                                result.Pnlqty = Convert.ToInt16(newCellValue.ToString());

                                db.SaveChanges();
                                UpdateFiltered_WorkorderList();
                            }
                        }
                    }

                    IsCellEditing = false;
                    GridWip.GetVisualContainer().InvalidateMeasureInfo();
                }
            }), System.Windows.Threading.DispatcherPriority.Normal);
        }

        public void UpdateFiltered_WorkorderList()
        {
            starttime1 = DateTime.Now;
            using (var db = new Db_Uv_InventoryContext())
            {
                var customer = _mainwindowViewModel.SelectedCustomerWo;
                var issample = false;
                if (_mainwindowViewModel.SelectedIsSampleWo == "양산") issample = false;
                else if (_mainwindowViewModel.SelectedIsSampleWo == "샘플") issample = true;
                Debug.WriteLine("양산샘플  " + (DateTime.Now - starttime1).TotalMilliseconds);
                var tmpwolist = db.ViewUvWorkorder.ToList<ViewUvWorkorder>();
                Debug.WriteLine("dbcontext  " + (DateTime.Now - starttime1).TotalMilliseconds);
                _mainwindowViewModel.WipCount_Dems = tmpwolist.Where(w => w.CustName == "대덕전자(MS)").Count().ToString();
                _mainwindowViewModel.WipCount_Depkg = tmpwolist.Where(w => w.CustName == "대덕전자(PKG)").Count().ToString();
                _mainwindowViewModel.WipCount_Yp = tmpwolist.Where(w => w.CustName == "영풍전자").Count().ToString();
                _mainwindowViewModel.WipCount_Bh = tmpwolist.Where(w => w.CustName == "BHFLEX").Count().ToString();
                _mainwindowViewModel.WipCount_Ifx = tmpwolist.Where(w => w.CustName == "인터플렉스").Count().ToString();
                _mainwindowViewModel.WipCount_Semco = tmpwolist.Where(w => w.CustName == "삼성전기").Count().ToString();
                _mainwindowViewModel.WipCount_Nft = tmpwolist.Where(w => w.CustName == "뉴프렉스").Count().ToString();
                _mainwindowViewModel.WipCount_Si = tmpwolist.Where(w => w.CustName == "SIFLEX").Count().ToString();
                Debug.WriteLine("wipcount  " + (DateTime.Now - starttime1).TotalMilliseconds);
                if (GridWip != null && GridWip.View != null)
                {


                    var dbWorkOrderList = new ObservableCollection<ViewUvWorkorder>(tmpwolist.Where(x => x.CustName == customer && x.SampleOrder == issample).OrderBy(x => x.TrackinTime));

                    var comparer = new ObjectsComparer.Comparer<ObservableCollection<ViewUvWorkorder>>();

                    //Compare objects
                    IEnumerable<Difference> differences;
                    var isEqual = comparer.Compare(dbWorkOrderList, _mainwindowViewModel.WorkOrderList, out differences);

                    //Print results
                    Debug.WriteLine(isEqual ? "Objects are equal" : string.Join(Environment.NewLine, differences));

                    if (!isEqual)
                    {
                        GridWip.View.BeginInit();
                        _mainwindowViewModel.WorkOrderList = dbWorkOrderList;
                        GridWip.View.EndInit();
                    }

                }


                Debug.WriteLine("workorderlist update  " + (DateTime.Now - starttime1).TotalMilliseconds);
                if (GridWip != null && GridWip.View != null)
                {
                    GridWip.View.BeginInit();
                    GridWip.Columns.Clear();

                    if (_mainwindowViewModel.SelectedCustomerWo == "대덕전자(MS)")
                    {
                        GridWipColumnDems();
                    }
                    else if (_mainwindowViewModel.SelectedCustomerWo == "대덕전자(PKG)")
                    {
                        GridWipColumnDepkg();
                    }
                    GridWip.View.EndInit();

                }

                var time2 = DateTime.Now;

                Debug.WriteLine(_mainwindowViewModel.WipCount_Dems + "  " + (time2 - starttime1).TotalMilliseconds);
            }
        }

        public void UpdateFiltered_WorkorderSearchList()
        {
            using (var db = new Db_Uv_InventoryContext())
            {
                var customer = _mainwindowViewModel.SelectedCustomerWoSearch;
                var issample = false;
                var datefrom = _mainwindowViewModel.SelectedDateFromWoSearch.AddDays(-1);
                var dateto = _mainwindowViewModel.SelectedDateToWoSearch.AddDays(1);
                if (_mainwindowViewModel.SelectedIsSampleWoSearch == "양산") issample = false;
                else if (_mainwindowViewModel.SelectedIsSampleWoSearch == "샘플") issample = true;

                var rcvdata = new ObservableCollection<ViewUvWorkorderDone>(db.ViewUvWorkorderDone.Where(x =>
                    x.CustName == customer && x.SampleOrder == issample && x.CreateTime >= datefrom &&
                    x.CreateTime <= dateto));

                _mainwindowViewModel.WorkOrderListSearch = rcvdata;
            }
        }

        public void UpdateSpecFiltered_WorkorderSearchList()
        {
            using (var db = new Db_Uv_InventoryContext())
            {
                var lot = _mainwindowViewModel.SrchInputLot;
                var tool = _mainwindowViewModel.SrchInputToolNo;
                var modelname = _mainwindowViewModel.SrchInputModelName;
                var datefrom = DateTime.Parse(DtpickWipsearchFrom.SelectedDate.ToString()).AddDays(-1);
                var dateto = DateTime.Parse(DtpickWipsearchTo.SelectedDate.ToString()).AddDays(1);

                bool isLot = lot.Count() > 1 ? true : false;
                bool isTool = tool.Count() > 3 ? true : false;
                bool isModelName = modelname.Count() > 3 ? true : false;

                if (isLot && isTool && isModelName)
                {
                    _mainwindowViewModel.WorkOrderListSearch = new ObservableCollection<ViewUvWorkorderDone>(
                        db.ViewUvWorkorderDone.Where(
                            x => x.Lotid == lot && x.CustToolno == tool && x.CustModelname == modelname &&
                                 x.CreateTime >= datefrom && x.CreateTime <= dateto));
                }

                else if (isLot && isTool)
                {
                    _mainwindowViewModel.WorkOrderListSearch = new ObservableCollection<ViewUvWorkorderDone>(
                        db.ViewUvWorkorderDone.Where(
                            x => x.Lotid == lot && x.CustToolno == tool && x.CreateTime >= datefrom &&
                                 x.CreateTime <= dateto));
                }

                else if (isLot && isModelName)
                {
                    _mainwindowViewModel.WorkOrderListSearch = new ObservableCollection<ViewUvWorkorderDone>(
                        db.ViewUvWorkorderDone.Where(
                            x => x.Lotid == lot && x.CustModelname == modelname && x.CreateTime >= datefrom &&
                                 x.CreateTime <= dateto));
                }

                else if (isTool && isModelName)
                {
                    _mainwindowViewModel.WorkOrderListSearch = new ObservableCollection<ViewUvWorkorderDone>(
                        db.ViewUvWorkorderDone.Where(
                            x => x.CustToolno == tool && x.CustModelname == modelname && x.CreateTime >= datefrom &&
                                 x.CreateTime <= dateto));
                }

                else if (isTool)
                {
                    _mainwindowViewModel.WorkOrderListSearch = new ObservableCollection<ViewUvWorkorderDone>(
                        db.ViewUvWorkorderDone.Where(
                            x => x.CustToolno == tool && x.CreateTime >= datefrom && x.CreateTime <= dateto));
                }

                else if (isModelName)
                {
                    _mainwindowViewModel.WorkOrderListSearch = new ObservableCollection<ViewUvWorkorderDone>(
                        db.ViewUvWorkorderDone.Where(
                            x => x.CustModelname == modelname && x.CreateTime >= datefrom && x.CreateTime <= dateto));
                }
                else if (isLot)
                {
                    _mainwindowViewModel.WorkOrderListSearch = new ObservableCollection<ViewUvWorkorderDone>(
                        db.ViewUvWorkorderDone.Where(
                            x => x.Lotid == lot && x.CreateTime >= datefrom && x.CreateTime <= dateto));
                }

            }
        }

        public void UpdateToolInfoList()
        {
            using (var db = new Db_Uv_InventoryContext())
            {
                _mainwindowViewModel.ToolInfos =
                    new ObservableCollection<TbUvToolinfo>(db.TbUvToolinfo);
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
            _mainwindowViewModel.RefreshDate();
        }

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

        
        #region 업체별 인수등록
        private void BtnInputLot_OnClick(object sender, RoutedEventArgs e)
        {
            if (_mainwindowViewModel.SelectedCustomerWo == "대덕전자(MS)")
            {
                if (!Application.Current.Windows.OfType<TrackInWindowDems>().Any())
                {
                    var trackinwindow = new TrackInWindowDems();
                    trackinwindow.Topmost = true;
                    trackinwindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                    trackinwindow.Show();
                }
            }
            else if (_mainwindowViewModel.SelectedCustomerWo == "대덕전자(PKG)")
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
        #endregion


        #region 업체별 재공현황 버튼

        private async void BtnWipDEMS_OnClick(object sender, RoutedEventArgs e)
        {
            _mainwindowViewModel.SelectedCustomerWo = "대덕전자(MS)";           
            
        }

        private async void BtnWipDEPKG_OnClick(object sender, RoutedEventArgs e)
        {
            _mainwindowViewModel.SelectedCustomerWo = "대덕전자(PKG)";
           
        }

        private void BtnWipYPE_OnClick(object sender, RoutedEventArgs e)
        {
            _mainwindowViewModel.SelectedCustomerWo = "영풍전자";
            
        }

        private void BtnWipBH_OnClick(object sender, RoutedEventArgs e)
        {
            _mainwindowViewModel.SelectedCustomerWo = "BHFLEX";
            
        }

        private void BtnWipIFC_OnClick(object sender, RoutedEventArgs e)
        {
            _mainwindowViewModel.SelectedCustomerWo = "인터플렉스";
            
        }

        private void BtnWipSEMCO_OnClick(object sender, RoutedEventArgs e)
        {
            _mainwindowViewModel.SelectedCustomerWo = "삼성전기";
            
        }

        private void BtnWipNFT_OnClick(object sender, RoutedEventArgs e)
        {
            _mainwindowViewModel.SelectedCustomerWo = "뉴프렉스";
           
        }

        private void BtnWipSI_OnClick(object sender, RoutedEventArgs e)
        {
            _mainwindowViewModel.SelectedCustomerWo = "SIFLEX";
            
        }

        #endregion

        #region 업체별 입출고 이력 버튼

        private void BtnWipSearchDEMS_OnClick(object sender, RoutedEventArgs e)
        {
            _mainwindowViewModel.SelectedCustomerWoSearch = "대덕전자(MS)";
            UpdateFiltered_WorkorderSearchList();
        }

        private void BtnWipSearchDEPKG_OnClick(object sender, RoutedEventArgs e)
        {
            _mainwindowViewModel.SelectedCustomerWoSearch = "대덕전자(PKG)";
            UpdateFiltered_WorkorderSearchList();
        }

        private void BtnWipSearchYPE_OnClick(object sender, RoutedEventArgs e)
        {
            _mainwindowViewModel.SelectedCustomerWoSearch = "영풍전자";
            UpdateFiltered_WorkorderSearchList();
        }

        private void BtnWipSearchBH_OnClick(object sender, RoutedEventArgs e)
        {
            _mainwindowViewModel.SelectedCustomerWoSearch = "BHFLEX";
            UpdateFiltered_WorkorderSearchList();
        }

        private void BtnWipSearchIFC_OnClick(object sender, RoutedEventArgs e)
        {
            _mainwindowViewModel.SelectedCustomerWoSearch = "인터플렉스";
            UpdateFiltered_WorkorderSearchList();
        }

        private void BtnWipSearchSEMCO_OnClick(object sender, RoutedEventArgs e)
        {
            _mainwindowViewModel.SelectedCustomerWoSearch = "삼성전기";
            UpdateFiltered_WorkorderSearchList();
        }

        private void BtnWipSearchNFT_OnClick(object sender, RoutedEventArgs e)
        {
            _mainwindowViewModel.SelectedCustomerWoSearch = "뉴프렉스";
            UpdateFiltered_WorkorderSearchList();
        }

        private void BtnWipSearchSI_OnClick(object sender, RoutedEventArgs e)
        {
            _mainwindowViewModel.SelectedCustomerWoSearch = "SIFLEX";
            UpdateFiltered_WorkorderSearchList();
        }

        private void BtnWipSearchALL_OnClick(object sender, RoutedEventArgs e)
        {
            _mainwindowViewModel.SelectedCustomerWoSearch = "전체조회";
            using (var db = new Db_Uv_InventoryContext())
            {
                var issample = false;
                var datefrom = DateTime.Parse(DtpickWipsearchFrom.SelectedDate.ToString()).AddDays(-1);
                var dateto = DateTime.Parse(DtpickWipsearchTo.SelectedDate.ToString());
                if (_mainwindowViewModel.SelectedIsSampleWoSearch == "양산") issample = false;
                else if (_mainwindowViewModel.SelectedIsSampleWoSearch == "샘플") issample = true;
                _mainwindowViewModel.WorkOrderListSearch = new ObservableCollection<ViewUvWorkorderDone>(
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
                if (_mainwindowViewModel.SelectedCustomerWoSearch.Contains("대덕") && _reDelot.IsMatch(tempKeyword))
                    keyword = TboxSearchKeyWord.Text.Replace("-", "");
                else
                    keyword = TboxSearchKeyWord.Text;

                var datefrom = DateTime.Parse(DtpickWipsearchFrom.SelectedDate.ToString()).AddDays(-1);
                var dateto = DateTime.Parse(DtpickWipsearchTo.SelectedDate.ToString()).AddDays(1);

                _mainwindowViewModel.WorkOrderListSearch = new ObservableCollection<ViewUvWorkorderDone>(
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
        
        //로트검색 초기화
        private void ButtonWipSrchClear_OnClick(object sender, RoutedEventArgs e)
        {
            tblksearchlot.Text = string.Empty;
            PerformSearchGridWip();
        }

        #region 재공현황 로트 검색
        private void Tblksearchlot_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (tblksearchlot.Text.Length > 2)
                {
                    if (_mainwindowViewModel.SelectedCustomerWo.Contains("대덕") && tblksearchlot.Text.Length > 5)
                    {
                        tblksearchlot.Text = tblksearchlot.Text.Replace("-", "");
                    }
                    PerformSearchGridWip();
                }
                else if (tblksearchlot.Text.Length < 2)
                {
                    this.GridWip.SearchHelper.ClearSearch();
                }
            }
        }

        private void PerformSearchGridWip()
        {
            if (this.GridWip.SearchHelper.SearchText.Equals(tblksearchlot.Text))
                return;

            var text = tblksearchlot.Text;
            this.GridWip.SearchHelper.AllowCaseSensitiveSearch = false;
            this.GridWip.SearchHelper.AllowFiltering = true;
            this.GridWip.SearchHelper.Search(text);
        }
        #endregion

        private void BtnToolInfoInput_OnClick(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.Windows.OfType<ToolinfoManageManualWindow>().Any())
            {
                var toolmanualwindow = new ToolinfoManageManualWindow();
                toolmanualwindow.Topmost = true;
                toolmanualwindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                toolmanualwindow.Show();
            }
        }


        //행색상변경
        private void ColorPickerPalette1_OnSelectedBrushChanged(object? sender, SelectedBrushChangedEventArgs e)
        {
            if (_mainwindowViewModel.SelectedGridWip != null)
            {
                var rowdata = _mainwindowViewModel.SelectedGridWip;
                var qryid = Convert.ToInt64(rowdata.Id);

                using (var db = new Db_Uv_InventoryContext())
                {
                    var result = db.TbUvWorkorder.SingleOrDefault(x => x.Id == qryid);

                    if (result != null)
                    {
                        result.FormatBg = e.NewColor.ToString();
                        db.SaveChanges();
                        UpdateFiltered_WorkorderList();
                    }
                }
            }
        }


    }

}
