﻿using DAWON_UV_INVENTORY_PROTO.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;

namespace DAWON_UV_INVENTORY_PROTO.ViewModels
{
    public class TrackInWindowDemsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public DataTable? SegementDataTable { get; set; }
        public string WorkcenterId { get; set; }
        private ObservableCollection<DemsRcvList>? _rcvLotlist;
        public ObservableCollection<DemsRcvList>? RcvLotList
        {
            get { return _rcvLotlist; }
            set { _rcvLotlist = value; }
        }

        public TrackInWindowDemsViewModel()
        {
            RcvLotList = new ObservableCollection<DemsRcvList>();
        }
        private void OnPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));

        }
    }
}
