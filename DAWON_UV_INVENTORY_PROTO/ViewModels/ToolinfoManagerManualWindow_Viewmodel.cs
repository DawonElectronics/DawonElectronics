using DAWON_UV_INVENTORY_PROTO.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace DAWON_UV_INVENTORY_PROTO.ViewModels
{
    public class ToolinfoManagerManualWindowViewmodel : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));

        }

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
        private string? _selectedIsPrcType;

        public string? SelectedIsPrcType
        {
            get
            { return _selectedIsPrcType; }

            set
            {
                _selectedIsPrcType = value;
                OnPropertyChanged(nameof(SelectedIsPrcType));
            }
        }

        private string? _inputModelName;

        public string? InputModelName
        {
            get
            { return _inputModelName; }

            set
            {
                _inputModelName = value;
                OnPropertyChanged(nameof(InputModelName));
            }
        }
        private string? _inputRevision;

        public string? InputRevision
        {
            get
            { return _inputRevision; }

            set
            {
                _inputRevision = value;
                OnPropertyChanged(nameof(InputRevision));
            }
        }

        private string? _inputToolNumber;

        public string? InputToolNumber
        {
            get
            { return _inputToolNumber; }

            set
            {
                _inputToolNumber = value;
                OnPropertyChanged(nameof(InputToolNumber));
            }
        }
        private string? _inputLayer1Left;

        public string? InputLayer1Left
        {
            get
            { return _inputLayer1Left; }

            set
            {
                _inputLayer1Left = value;
                OnPropertyChanged(nameof(InputLayer1Left));
            }
        }
        private string? _inputLayer1Right;

        public string? InputLayer1Right
        {
            get
            { return _inputLayer1Right; }

            set
            {
                _inputLayer1Right = value;
                OnPropertyChanged(nameof(InputLayer1Right));
            }
        }

        private string? _inputLayer2Left;

        public string? InputLayer2Left
        {
            get
            { return _inputLayer2Left; }

            set
            {
                _inputLayer2Left = value;
                OnPropertyChanged(nameof(InputLayer2Left));
            }
        }
        private string? _inputLayer2Right;

        public string? InputLayer2Right
        {
            get
            { return _inputLayer2Right; }

            set
            {
                _inputLayer2Right = value;
                OnPropertyChanged(nameof(InputLayer2Right));
            }
        }
        private string? _inputHoleCount1;

        public string? InputHoleCount1
        {
            get
            { return _inputHoleCount1; }

            set
            {
                _inputHoleCount1 = value;
                OnPropertyChanged(nameof(InputHoleCount1));
            }
        }
        private string? _inputHoleCount2;

        public string? InputHoleCount2
        {
            get
            { return _inputHoleCount2; }

            set
            {
                _inputHoleCount2 = value;
                OnPropertyChanged(nameof(InputHoleCount2));
            }
        }
        private string? _inputHoleCountPth;

        public string? InputHoleCountPth
        {
            get
            { return _inputHoleCountPth; }

            set
            {
                _inputHoleCountPth = value;
                OnPropertyChanged(nameof(InputHoleCountPth));
            }
        }
        private string? _inputHoleSize;

        public string? InputHoleSize
        {
            get
            { return _inputHoleSize; }

            set
            {
                _inputHoleSize = value;
                OnPropertyChanged(nameof(InputHoleSize));
            }
        }
        private string? _inputDepth;

        public string? InputDepth
        {
            get
            { return _inputDepth; }

            set
            {
                _inputDepth = value;
                OnPropertyChanged(nameof(InputDepth));
            }
        }
        private string? _inputWorksizeX;

        public string? InputWorksizeX
        {
            get
            { return _inputWorksizeX; }

            set
            {
                _inputWorksizeX = value;
                OnPropertyChanged(nameof(InputWorksizeX));
            }
        }
        private string? _inputWorksizeY;

        public string? InputWorksizeY
        {
            get
            { return _inputWorksizeY; }

            set
            {
                _inputWorksizeY = value;
                OnPropertyChanged(nameof(InputWorksizeY));
            }
        }
        private string? _inputMesSeqCode;

        public string? InputMesSeqCode
        {
            get
            { return _inputMesSeqCode; }

            set
            {
                _inputMesSeqCode = value;
                OnPropertyChanged(nameof(InputMesSeqCode));
            }
        }

        private string? _inputMesPrcName;

        public string? InputMesPrcName
        {
            get
            { return _inputMesPrcName; }

            set
            {
                _inputMesPrcName = value;
                OnPropertyChanged(nameof(InputMesPrcName));
            }
        }

        private string? _inputPcsPerPnl;

        public string? InputPcsPerPnl
        {
            get
            { return _inputPcsPerPnl; }

            set
            {
                _inputPcsPerPnl = value;
                OnPropertyChanged(nameof(InputPcsPerPnl));
            }
        }
        private string? _inputBlkPerPnl;

        public string? InputBlkPerPnl
        {
            get
            { return _inputBlkPerPnl; }

            set
            {
                _inputBlkPerPnl = value;
                OnPropertyChanged(nameof(InputBlkPerPnl));
            }
        }

        private string? _inputMesPrcCode;

        public string? InputMesPrcCode
        {
            get
            { return _inputMesPrcCode; }

            set
            {
                _inputMesPrcCode = value;
                OnPropertyChanged(nameof(InputMesPrcCode));
            }
        }
        private string? _inputEndCustomer;

        public string? InputEndCustomer
        {
            get
            { return _inputEndCustomer; }

            set
            {
                _inputEndCustomer = value;
                OnPropertyChanged(nameof(InputEndCustomer));
            }
        }

        #region IDataErrorInfo Members

        private string _error;

        public string Error
        {
            get => _error;
            set
            {
                if (_error != value)
                {
                    _error = value;
                    OnPropertyChanged("Error");
                }
            }
        }

        public string this[string columnName]
        {
            get
            {
                return OnValidate(columnName);
            }
        }

        private string OnValidate(string columnName)
        {
            string result = string.Empty;
            if (columnName == "InputModelName")
            {
                if (string.IsNullOrEmpty(InputModelName))
                {
                    result = "모델명을 입력해주세요";
                }              
            }
            if (columnName == "InputToolNumber")
            {
                if (string.IsNullOrEmpty(InputToolNumber))
                {
                    result = "툴 넘버를 입력해주세요";
                }
            }
            if (columnName == "InputRevision")
            {
                if (string.IsNullOrEmpty(InputRevision))
                {
                    result = "리비전을 입력해주세요";
                }
            }
            if (columnName == "InputWorksizeX")
            {
                if (string.IsNullOrEmpty(InputWorksizeX))
                {
                    result = "워크사이즈-X 를 입력해주세요";
                }
            }
            if (columnName == "InputWorksizeY")
            {
                if (string.IsNullOrEmpty(InputWorksizeY))
                {
                    result = "워크사이즈-Y 를 입력해주세요";
                }
            }
            if (columnName == "InputModelName")
            {
                if (string.IsNullOrEmpty(InputModelName))
                {
                    result = "모델명을 입력해주세요";
                }
            }

           
            if (result == null)
            {
                Error = null;
            }
            else
            {
                Error = "Error";
            }
            return result;
        }

        #endregion
    }
}
