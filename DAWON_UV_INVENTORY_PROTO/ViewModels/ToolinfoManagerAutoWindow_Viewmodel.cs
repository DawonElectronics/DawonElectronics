using DAWON_UV_INVENTORY_PROTO.Models;
using System;
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

        private ObservableCollection<TbCustomer>? _customer;
        public ObservableCollection<TbCustomer>? Customer { get; set; }

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
