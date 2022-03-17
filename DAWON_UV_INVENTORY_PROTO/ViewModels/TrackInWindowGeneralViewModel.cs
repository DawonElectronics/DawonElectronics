using DAWON_UV_INVENTORY_PROTO.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace DAWON_UV_INVENTORY_PROTO.ViewModels
{
    public class TrackInWindowGeneralViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;    
        
        private void OnPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public TrackInWindowGeneralViewModel()
        {
            TrackinList = new ObservableCollection<TrackinModel>();
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

        private string? _selectedCustomerTrackinForm;
        public string? SelectedCustomerTrackinForm
        {
            get { return _selectedCustomerTrackinForm; }
            set
            {
                _selectedCustomerTrackinForm = value;
                OnPropertyChanged(nameof(SelectedCustomerTrackinForm));
            }
        }

        private string? _selectedIsSampleTrackinForm;

        public string? SelectedIsSampleTrackinForm
        {
            get
            { return _selectedIsSampleTrackinForm; }

            set
            {
                _selectedIsSampleTrackinForm = value;
                OnPropertyChanged(nameof(SelectedIsSampleTrackinForm));
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

        private ObservableCollection<TrackinModel>? _trackinList;
        public ObservableCollection<TrackinModel>? TrackinList
        {
            get { return _trackinList; }
            set
            {
                _trackinList = value;
                OnPropertyChanged(nameof(TrackinList));
            }
        }
    }
    public class TrackinModel
    {
        public string EndCustomer { get; set; }
        public string Lotid { get; set; }
        public string? SampleDept { get; set; }
        public string CustModelname { get; set; }
        public string CustRevision { get; set; }
        public string CustToolno { get; set; }
        public string MesPrcName { get; set; }
        public string MesSeqCode { get; set; }
        public string PrcName { get; set; }       
        public short? Pnlqty { get; set; }
        
        public string LotType { get; set; }        
       
        public string LotNotes { get; set; }
        
        public bool? Sample { get; set; }
        
        public int? Layer { get; set; }
       
        public string PrcLayer1 { get; set; }
        public string PrcLayer2 { get; set; }  
      
        public string PrcCode { get; set; }

        public string CustName { get; set; }
        
        
    }

}
