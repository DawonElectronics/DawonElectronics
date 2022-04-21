using DAWON_UV_INVENTORY_PROTO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using ConnectorDEMS.Models;

namespace DAWON_UV_INVENTORY_PROTO.ViewModels
{
    public class TrackInWindowDemsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public DataTable? SegementDataTable { get; set; }
        public string WorkcenterId { get; set; }
        private List<DeMsRcvModelAfterValidation>? _rcvLotlist;
        public List<DeMsRcvModelAfterValidation>? RcvLotList
        {
            get { return _rcvLotlist; }
            set
            {
                _rcvLotlist = value;
                OnPropertyChanged(nameof(RcvLotList));
            }
        }

        public TrackInWindowDemsViewModel()
        {
            RcvLotList = new List<DeMsRcvModelAfterValidation>();
        }
        private void OnPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));

        }
    }
}
