using DAWON_UV_INVENTORY_PROTO.Models;
using Syncfusion.Windows.Controls.Input;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Syncfusion.UI.Xaml.Grid;

namespace DAWON_UV_INVENTORY_PROTO.ViewModels
{
    public partial class MainWindowViewModel : INotifyPropertyChanged
    {
        Regex _reDelot = new Regex(@".[0-9]{6}-[0-9]{1}.[0-9]{2}.");

        private MainWindow mainWindow;

        public void OnViewInitialized(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }
        public MainWindowViewModel()
        {
            AutoCompleteLoaded = new DelegateCommand(AutoCompleteLoadedMethod);
            WipLotSearchCommand = new DelegateCommand(OnWipLotSearchRecordClicked);
            ToolInfos = new ObservableCollection<TbUvToolinfo>();
            WorkOrderList = new ObservableCollection<ViewUvWorkorder>();
            _selectedDateFromWoSearch = DateTime.Now.AddMonths(-1);
            _selectedDateToWoSearch = DateTime.Now;
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
                if (((selData.PrcLayer2.Length>1)&&(selData.PrcName.Contains("BVH"))))
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
                                mainWindow.UpdateFiltered_WorkorderList();
                                
                                MessageBox.Show("처리되었습니다");
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
                            mainWindow.UpdateFiltered_WorkorderList();
                            MessageBox.Show("처리되었습니다");

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
                            mainWindow.UpdateFiltered_WorkorderList();
                            MessageBox.Show("처리되었습니다");
                        }
                    }
                }

                else if (selData.MachineCs == null && selData.MachineSs == null &&
                         (selData.SampleOrder == null || selData.SampleOrder == false))
                {
                    MessageBox.Show("호기 선택 바랍니다");
                }
            }
        }
        #endregion


        public ICommand AutoCompleteLoaded { get; set; }

        private ObservableCollection<TbPrctype>? _prctypes;
        public ObservableCollection<TbPrctype>? PrcTypes
        {
            get { return _prctypes; }
            set
            {
                _prctypes = value;
                OnPropertyChanged(nameof(PrcTypes));
            }
        }

        private ObservableCollection<TbUvToolinfo>? _toolinfos;
        public ObservableCollection<TbUvToolinfo>? ToolInfos
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

        private ObservableCollection<ViewUvWorkorderDone>? _workOrderlistSearch;

        public ObservableCollection<ViewUvWorkorderDone>? WorkOrderListSearch
        {
            get { return _workOrderlistSearch; }
            set
            {
                _workOrderlistSearch = value;
                OnPropertyChanged(nameof(WorkOrderListSearch));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }



        
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
        private ICommand _lottypeChangedCommand;

        public ICommand LottypeChangedCommand
        {
            get => _lottypeChangedCommand;
            set
            {
                _lottypeChangedCommand = value;
                OnPropertyChanged(nameof(LottypeChangedCommand));
            }
        }

        private ObservableCollection<TbCustomer>? _customer;
        public ObservableCollection<TbCustomer>? Customer 
        {
            get { return _customer; }
            set
            {
                _customer = value;
                OnPropertyChanged(nameof(Customer));
            }
        }

        private ObservableCollection<TbUsers>? _userList;
        public ObservableCollection<TbUsers>? UserList
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

        private ObservableCollection<TbMachine>? _machines;
        public ObservableCollection<TbMachine>? Machines
        {
            get { return _machines; }
            set
            {
                _machines = value;
                OnPropertyChanged(nameof(Machines));
            }
        }


        private ObservableCollection<object>? _selectedMachineCs;
        public ObservableCollection<object>? SelectedMachineCs
        {
            get { return _selectedMachineCs; }
            set
            {
                _selectedMachineCs = value;
                OnPropertyChanged(nameof(SelectedMachineCs));
            }
        }

        private ObservableCollection<object>? _selectedMachineSs;
        public ObservableCollection<object>? SelectedMachineSs
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
            get { return _toolno2pid;}
            set
            {
                _toolno2pid = ToolInfos.Where(w => w.CustToolno == value).Select(s => s.ProductId).FirstOrDefault();
                OnPropertyChanged(nameof(Toolno2Pid));
            }
        }
    }

    

}
