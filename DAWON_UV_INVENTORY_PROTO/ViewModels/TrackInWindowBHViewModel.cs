using DAWON_UV_INVENTORY_PROTO.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;


namespace DAWON_UV_INVENTORY_PROTO.ViewModels
{
    public class TrackInWindowBHViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
       
       
        //private WipModelAfterToolValidation? _selectedItem;
        //public WipModelAfterToolValidation? SelectedItem
        //{
        //    get { return _selectedItem; }
        //    set
        //    {
        //        _selectedItem = value;
        //        OnPropertyChanged(nameof(SelectedItem));
        //    }
        //}


        public TrackInWindowBHViewModel()
        {
            //RcvLotList = new List<WipModelAfterToolValidation>();
           
        }
        private void OnPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));

        }
    }
}