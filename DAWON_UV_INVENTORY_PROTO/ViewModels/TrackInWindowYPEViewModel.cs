using DAWON_UV_INVENTORY_PROTO.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using ConnectorYPE.Models;

namespace DAWON_UV_INVENTORY_PROTO.ViewModels
{
    public class TrackInWindowYPEViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
       
        private List<WipModelAfterToolValidation>? _rcvLotlist;
        public List<WipModelAfterToolValidation>? RcvLotList
        {
            get { return _rcvLotlist; }
            set
            {
                _rcvLotlist = value;
                OnPropertyChanged(nameof(RcvLotList));
            }
        }
        private WipModelAfterToolValidation? _selectedItem;
        public WipModelAfterToolValidation? SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }


        public TrackInWindowYPEViewModel()
        {
            RcvLotList = new List<WipModelAfterToolValidation>();
           
        }
        private void OnPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));

        }
    }
}