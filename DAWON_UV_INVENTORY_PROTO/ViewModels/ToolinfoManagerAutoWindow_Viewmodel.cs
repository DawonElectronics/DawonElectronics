using DAWON_UV_INVENTORY_PROTO.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;

namespace DAWON_UV_INVENTORY_PROTO.ViewModels
{
    public class ToolinfoManagerAutoWindowViewmodel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public DataTable? SegementDataTable { get; set; }
        public string? WorkcenterId { get; set; }

        private List<TbCustomer>? _customer;
        public List<TbCustomer>? Customer { get; set; }

        private string? _selectedCustomerToolreg;
        public string? SelectedCustomerToolreg
        {
            get { return _selectedCustomerToolreg; }
            set
            {
                _selectedCustomerToolreg = value;
                OnPropertyChanged(nameof(SelectedCustomerToolreg));
            }
        }

        private void OnPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));

        }
    }
}
