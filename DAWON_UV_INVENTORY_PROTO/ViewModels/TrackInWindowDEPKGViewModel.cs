using ConnectorDEPKG.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;



namespace DAWON_UV_INVENTORY_PROTO.ViewModels
{
    public class TrackInWindowDepkgViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public DataTable? SegementDataTable { get; set; }
        public string WorkcenterId { get; set; }
        private List<DePkgRcvModelAfterValidation>? _rcvLotlist;
        public List<DePkgRcvModelAfterValidation>? RcvLotList
        {
            get { return _rcvLotlist; }
            set
            {
                _rcvLotlist = value;
                OnPropertyChanged(nameof(RcvLotList));
            }
        }

        public TrackInWindowDepkgViewModel()
        {
            RcvLotList = new List<DePkgRcvModelAfterValidation>();
        }
        private void OnPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));

        }
    }
}