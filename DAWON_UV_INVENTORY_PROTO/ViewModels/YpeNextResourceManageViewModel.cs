using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAWON_UV_INVENTORY_PROTO.ViewModels
{
    public class YpeNextResourceManageViewModel
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        private List<YpeNextResourceModel>? _nextResourceModels;
        public List<YpeNextResourceModel>? NextResourceModels
        {
            get { return _nextResourceModels; }
            set
            {
                _nextResourceModels = value;
                OnPropertyChanged(nameof(NextResourceModels));
            }
        }

        private YpeNextResourceModel? _selectedGridItem;
        public YpeNextResourceModel? SelectedGridItem
        {
            get { return _selectedGridItem; }
            set
            {
                _selectedGridItem = value;
                OnPropertyChanged(nameof(SelectedGridItem));
            }
        }


    }

    public class YpeNextResourceModel
    {
        public string ProductId { get; set; }
        public string CustModelName { get; set; }
        public string? CustRevision { get; set; }
        public string CustToolno { get; set; }
        public string YpNextResourcelist { get; set; }
        public string MesPrcName { get; set; }
        public string? YpNextResourceDefault { get; set; }
    }
}
