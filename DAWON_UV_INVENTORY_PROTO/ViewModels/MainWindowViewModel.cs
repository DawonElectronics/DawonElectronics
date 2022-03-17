using DAWON_UV_INVENTORY_PROTO.Models;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.Windows.Controls.Input;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace DAWON_UV_INVENTORY_PROTO.ViewModels
{
    public partial class MainWindowViewModel : INotifyPropertyChanged
    {
        private ConcurrentQueue<string> trackoutque;

        Regex _reDelot = new Regex(@".[0-9]{6}-[0-9]{1}.[0-9]{2}.");

        public event PropertyChangedEventHandler? PropertyChanged;
        
        private void OnPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
        private MainWindow mainWindow;

        public void OnViewInitialized(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }
        public MainWindowViewModel()
        {
            AutoCompleteLoaded = new DelegateCommand(AutoCompleteLoadedMethod);
            WipLotSearchCommand = new DelegateCommand(OnWipLotSearchRecordClicked);
            ToolInfos = new List<TbUvToolinfo>();
            Machines = new List<TbMachine>();
            
            WorkOrderList = new ObservableCollection<ViewUvWorkorder>();   

            _selectedDateFromWoSearch = DateTime.Now.AddMonths(-1);
            _selectedDateToWoSearch = DateTime.Now;
            trackoutque = new ConcurrentQueue<string>();
        }

        #region 로트검색 엔터키 명령

        public ICommand WipLotSearchCommand { get; set; }

        private void OnWipLotSearchRecordClicked(object obj)
        {
            mainWindow.GridWip.SelectedItems.Clear();
            mainWindow.GridWip.SearchHelper.ClearSearch();

            var tempkeyword = mainWindow.tblksearchlot.Text;
            if (tempkeyword.Length > 2)
            {
                if (SelectedCustomerWoSearch.Contains("대덕") && tempkeyword.Length > 5)
                {
                    mainWindow.tblksearchlot.Text = mainWindow.tblksearchlot.Text.Replace("-", "");
                }

                mainWindow.GridWip.SearchHelper.AllowFiltering = true;
                mainWindow.GridWip.SearchHelper.FindNext(mainWindow.tblksearchlot.Text);
                //mainWindow.GridWip.SelectionController.MoveCurrentCell(mainWindow.GridWip.SearchHelper.CurrentRowColumnIndex);
                var list = mainWindow.GridWip.SearchHelper.GetSearchRecords();
                if (list.Count > 0)
                {
                    int recordIndex =
                        mainWindow.GridWip.ResolveToRecordIndex(mainWindow.GridWip.ResolveToRowIndex(list[0].Record));
                    mainWindow.GridWip.SelectedIndex = recordIndex;
                }
            }

            else if (tempkeyword.Length < 2 || tempkeyword == null)
            {
                mainWindow.GridWip.SelectedItems.Clear();
                mainWindow.GridWip.SearchHelper.ClearSearch();
            }
        }
        #endregion
        #region 그리드 버튼(출고처리)

        private BaseCommand? _trackoutRecord;
        public BaseCommand? TrackoutRecord
        {
            get
            {
                if (_trackoutRecord == null)
                    _trackoutRecord = new BaseCommand(OnTrackoutRecordClicked, OnCanTrackout);
                return _trackoutRecord;
            }
        }

        private static bool OnCanTrackout(object obj)
        {
            return true;
        }

        private void OnTrackoutRecordClicked(object obj)
        {

            var selData = (obj as ViewUvWorkorder);
            if (SelectedUser == null)
            {
                MessageBox.Show("사용자 선택 바랍니다");
            }

            if (selData != null)
            {
                var gridrecord = (mainWindow.GridWip.DataContext as MainWindowViewModel).WorkOrderList.Where(x=>x.Id == selData.Id).First();
                if (((selData.PrcLayer2.Length > 1) && (selData.PrcName.Contains("BVH"))))
                {
                    if (selData.MachineSs == null || selData.MachineCs == null)
                    {
                        MessageBox.Show("호기 선택 바랍니다");
                    }

                    if (selData.MachineCs != null && selData.MachineSs != null && SelectedUser != null)
                    {
                        using (var db = new Db_Uv_InventoryContext())
                        {
                            var result = db.TbUvWorkorder.SingleOrDefault(x => x.Id == selData.Id);

                            if (result != null)
                            {
                                var trackoutuser = db.TbUsers.Where(x => x.UserName == SelectedUser).FirstOrDefault();

                                result.TrackoutTime = DateTime.Now;
                                result.TrackoutUser = trackoutuser;
                                result.IsDone = true;
                                result.LotType = "완료";
                                result.WaitTrackout = false;
                                db.SaveChanges();
                                (mainWindow.GridWip.DataContext as MainWindowViewModel).WorkOrderList.Remove(gridrecord);
                                mainWindow.GridWip.View.Refresh();

                                mainWindow.TblkExecuteStatus.Visibility = Visibility.Visible;
                                trackoutque.Enqueue($"{selData.Lotid} 출고\t");
                                ExecuteResult += $"{selData.Lotid} 출고\t";
                            }
                        }
                    }
                }

                else if ((selData.MachineCs != null || selData.MachineSs != null) && SelectedUser != null)
                {
                    using (var db = new Db_Uv_InventoryContext())
                    {
                        var result = db.TbUvWorkorder.SingleOrDefault(x => x.Id == selData.Id);

                        if (result != null)
                        {
                            var trackoutuser = db.TbUsers.Where(x => x.UserName == SelectedUser).FirstOrDefault();
                            result.TrackoutTime = DateTime.Now;
                            result.TrackoutUser = trackoutuser;
                            result.IsDone = true;
                            result.LotType = "완료";
                            result.WaitTrackout = false;
                            db.SaveChanges();
                            
                            (mainWindow.GridWip.DataContext as MainWindowViewModel).WorkOrderList.Remove(gridrecord);
                            mainWindow.GridWip.View.Refresh();
                            trackoutque.Enqueue($"{selData.Lotid} 출고\t");
                            ExecuteResult += $"{selData.Lotid} 출고\t";
                        }
                    }
                }
                else if (selData.SampleOrder == true)
                {
                    using (var db = new Db_Uv_InventoryContext())
                    {
                        var result = db.TbUvWorkorder.SingleOrDefault(x => x.Id == selData.Id);

                        if (result != null)
                        {
                            var trackoutuser = db.TbUsers.Where(x => x.UserName == SelectedUser).FirstOrDefault();
                            result.TrackoutTime = DateTime.Now;
                            result.TrackoutUser = trackoutuser;
                            result.IsDone = true;
                            result.LotType = "완료";
                            result.WaitTrackout = false;
                            db.SaveChanges();

                            (mainWindow.GridWip.DataContext as MainWindowViewModel).WorkOrderList.Remove(gridrecord);
                            mainWindow.GridWip.View.Refresh();
                            trackoutque.Enqueue($"{selData.Lotid} 출고\t");
                            ExecuteResult += $"{selData.Lotid} 출고\t";

                        }
                    }
                }

                else if (selData.MachineCs == null && selData.MachineSs == null &&
                         (selData.SampleOrder == null || selData.SampleOrder == false))
                {
                    MessageBox.Show("호기 선택 바랍니다");
                }
            }

            DelayedExecuteMsgOff();
        }
        #endregion

        public void DelayedExecuteMsgOff()
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(30) };
            timer.Start();
            timer.Tick += (sender, args) =>
            {
                timer.Stop();
                var deque = string.Empty;
                trackoutque.TryDequeue(out deque);
                ExecuteResult = ExecuteResult.Replace(deque,"");
            };
            
        }

        #region Tool 자동완성
        public ICommand AutoCompleteLoaded { get; set; }
        private void AutoCompleteLoadedMethod(object obj)
        {
            var autocomplete = obj as SfTextBoxExt;
            if (autocomplete != null)
            {
                autocomplete.Filter = CustomFilter;
            }
        }
        public bool CustomFilter(string search, object item)
        {
            var model = item as TbUvToolinfo;
            if (model != null)
            {
                if ((model.ProductId.ToLower().Contains(search.ToLower())) || ((model.CustToolno).ToString().ToLower().Contains(search.ToLower())))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        private List<TbPrctype>? _prctypes;
        public List<TbPrctype>? PrcTypes
        {
            get { return _prctypes; }
            set
            {
                _prctypes = value;
                OnPropertyChanged(nameof(PrcTypes));
            }
        }

        private List<TbUvToolinfo>? _toolinfos;
        public List<TbUvToolinfo>? ToolInfos
        {
            get { return _toolinfos; }
            set
            {
                _toolinfos = value;
                OnPropertyChanged(nameof(ToolInfos));
            }
        }

        private ObservableCollection<ViewUvWorkorder>? _workOrderlist;
        public ObservableCollection<ViewUvWorkorder>? WorkOrderList
        {
            get { return _workOrderlist; }
            set
            {

                _workOrderlist = value;
                OnPropertyChanged(nameof(WorkOrderList));
            }
        }
        private ObservableCollection<ViewUvWorkorder2>? _workOrderlist2;
        public ObservableCollection<ViewUvWorkorder2>? WorkOrderList2
        {
            get { return _workOrderlist2; }
            set
            {

                _workOrderlist2 = value;
                OnPropertyChanged(nameof(WorkOrderList2));
            }
        }

        private ViewUvWorkorder? _selectedGridWip;
        public ViewUvWorkorder? SelectedGridWip
        {
            get { return _selectedGridWip; }
            set
            {
                _selectedGridWip = value;
                OnPropertyChanged(nameof(SelectedGridWip));
            }
        }

        private List<ViewUvWorkorderDone>? _workOrderlistSearch;

        public List<ViewUvWorkorderDone>? WorkOrderListSearch
        {
            get { return _workOrderlistSearch; }
            set
            {
                _workOrderlistSearch = value;
                OnPropertyChanged(nameof(WorkOrderListSearch));
            }
        }

        private List<TbCustomer>? _customer;
        public List<TbCustomer>? Customer
        {
            get { return _customer; }
            set
            {
                _customer = value;
                OnPropertyChanged(nameof(Customer));
            }
        }

        private List<TbUsers>? _userList;
        public List<TbUsers>? UserList
        {
            get { return _userList; }
            set
            {
                _userList = value;
                OnPropertyChanged(nameof(UserList));
            }
        }

        private string? _selectedCustomerWo;
        public string? SelectedCustomerWo
        {
            get { return _selectedCustomerWo; }
            set
            {
                _selectedCustomerWo = value;
                OnPropertyChanged(nameof(SelectedCustomerWo));
            }
        }
        private string? _selectedCustomerWoSearch;
        public string? SelectedCustomerWoSearch
        {
            get { return _selectedCustomerWoSearch; }
            set
            {
                _selectedCustomerWoSearch = value;
                OnPropertyChanged(nameof(SelectedCustomerWoSearch));
            }
        }

        private DateTime _selectedDateFromWoSearch;
        public DateTime SelectedDateFromWoSearch
        {
            get { return _selectedDateFromWoSearch; }
            set
            {
                _selectedDateFromWoSearch = value;
                OnPropertyChanged(nameof(SelectedDateFromWoSearch));
            }
        }

        private DateTime _selectedDateToWoSearch;
        public DateTime SelectedDateToWoSearch
        {
            get { return _selectedDateToWoSearch; }
            set
            {
                _selectedDateToWoSearch = value;
                OnPropertyChanged(nameof(SelectedDateToWoSearch));
            }
        }

        private string? _selectedCustidToolinfo;
        public string? SelectedCustIdToolinfo
        {
            get { return _selectedCustidToolinfo; }
            set
            {
                _selectedCustidToolinfo = value;
                OnPropertyChanged(nameof(SelectedCustIdToolinfo));
            }
        }

        private string? _selectedIsSampleWo;

        public string? SelectedIsSampleWo
        {
            get
            { return _selectedIsSampleWo; }

            set
            {
                _selectedIsSampleWo = value;
                OnPropertyChanged(nameof(SelectedIsSampleWo));
            }
        }

        private string? _selectedIsSampleWoSearch;
        public string? SelectedIsSampleWoSearch
        {
            get
            { return _selectedIsSampleWoSearch; }

            set
            {
                _selectedIsSampleWoSearch = value;
                OnPropertyChanged(nameof(SelectedIsSampleWoSearch));
            }
        }
        public void RefreshDate()
        {
            SearchDateBeforeOneMonth = DateTime.UtcNow.AddDays(-30);
            OnPropertyChanged("SearchDateBeforeOneMonth");

        }
        public DateTime SearchDateBeforeOneMonth { get; private set; }
        public string? SrchInputLot { get; set; }
        public string? SrchInputModelName { get; set; }
        public string? SrchInputToolNo { get; set; }

        private string? _selectedPrccodeToolinfo;
        public string? SelectedPrcCodeToolinfo
        {
            get { return _selectedPrccodeToolinfo; }
            set
            {
                _selectedPrccodeToolinfo = value;
                OnPropertyChanged(nameof(SelectedPrcCodeToolinfo));
            }
        }

        private List<TbMachine>? _machines;
        public List<TbMachine>? Machines
        {
            get { return _machines; }
            set
            {
                _machines = value;
                MachineList = value.Select(x => x.MachineShortname).ToList<string>();
                OnPropertyChanged(nameof(Machines));
            }
        }

        private List<string>? _machineList;
        public List<string>? MachineList
        {
            get { return _machineList; }
            set
            {
                _machineList = value;
                OnPropertyChanged(nameof(MachineList));
            }
        }


        private List<object>? _selectedMachineCs;
        public List<object>? SelectedMachineCs
        {
            get { return _selectedMachineCs; }
            set
            {
                _selectedMachineCs = value;
                OnPropertyChanged(nameof(SelectedMachineCs));
            }
        }

        private List<object>? _selectedMachineSs;
        public List<object>? SelectedMachineSs
        {
            get { return _selectedMachineSs; }
            set
            {
                _selectedMachineSs = value;
                OnPropertyChanged(nameof(SelectedMachineSs));
            }
        }

        public List<string>? Users { get; set; }

        private string? _selectedUser;
        public string? SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        private bool? chktest;
        public bool? ChkTest
        {
            get { return chktest; }
            set
            {
                chktest = value;
                OnPropertyChanged(nameof(ChkTest));
            }
        }
        private string? _searchkeyword;
        public string? SearchKeyword
        {
            get { return _searchkeyword; }
            set
            {
                _searchkeyword = value;
                OnPropertyChanged(nameof(SearchKeyword));
            }
        }

        private string? _gridwipLotSearchString;
        public string? GridwipLotSearchString
        {
            get { return _gridwipLotSearchString; }
            set
            {
                _gridwipLotSearchString = value;
                OnPropertyChanged(nameof(GridwipLotSearchString));
            }
        }

        private string _toolno2pid;

        public string Toolno2Pid
        {
            get { return _toolno2pid; }
            set
            {
                _toolno2pid = ToolInfos.Where(w => w.CustToolno == value).Select(s => s.ProductId).FirstOrDefault();
                OnPropertyChanged(nameof(Toolno2Pid));
            }
        }
        private string? _executeResult;
        public string? ExecuteResult
        {
            get { return _executeResult; }
            set
            {
                _executeResult = value;
                OnPropertyChanged(nameof(ExecuteResult));
            }
        }


        #region 업체별 재공 수량 표시

        private string _wipCount_Dems;

        public string WipCount_Dems
        {
            get { return _wipCount_Dems; }
            set
            {
                _wipCount_Dems = $"대덕MS({value})";
                OnPropertyChanged(nameof(WipCount_Dems));
            }
        }

        private string _wipCount_Depkg;

        public string WipCount_Depkg
        {
            get { return _wipCount_Depkg; }
            set
            {
                _wipCount_Depkg = $"대덕PKG({value})";
                OnPropertyChanged(nameof(WipCount_Depkg));
            }
        }

        private string _wipCount_Yp;

        public string WipCount_Yp
        {
            get { return _wipCount_Yp; }
            set
            {
                _wipCount_Yp = $"영풍전자({value})";
                OnPropertyChanged(nameof(WipCount_Yp));
            }
        }
        private string _wipCount_Bh;

        public string WipCount_Bh
        {
            get { return _wipCount_Bh; }
            set
            {
                _wipCount_Bh = $"BH({value})";
                OnPropertyChanged(nameof(WipCount_Bh));
            }
        }
        private string _wipCount_Ifx;

        public string WipCount_Ifx
        {
            get { return _wipCount_Ifx; }
            set
            {
                _wipCount_Ifx = $"인터({value})";
                OnPropertyChanged(nameof(WipCount_Ifx));
            }
        }
        private string _wipCount_Semco;

        public string WipCount_Semco
        {
            get { return _wipCount_Semco; }
            set
            {
                _wipCount_Semco = $"삼성전기({value})";
                OnPropertyChanged(nameof(WipCount_Semco));
            }
        }

        private string _wipCount_Nft;

        public string WipCount_Nft
        {
            get { return _wipCount_Nft; }
            set
            {
                _wipCount_Nft = $"뉴프렉스({value})";
                OnPropertyChanged(nameof(WipCount_Nft));
            }
        }

        private string _wipCount_Si;

        public string WipCount_Si
        {
            get { return _wipCount_Si; }
            set
            {
                _wipCount_Si = $"SI({value})";
                OnPropertyChanged(nameof(WipCount_Si));
            }
        }

        #endregion

    }



}
