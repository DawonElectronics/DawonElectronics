using DAWON_UV_INVENTORY_PROTO.Models;
using Syncfusion.Data.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DAWON_UV_INVENTORY_PROTO.ViewModels
{
    public class CamFinishYpeViewModel : INotifyPropertyChanged
    {
       
        public CamFinishYpeViewModel()
        {
        }
        private List<string>? _ypnextresourceList;
        public List<string>? YpNextResourceList
        {
            get { return _ypnextresourceList; }
            set
            {
                _ypnextresourceList = value;
                OnPropertyChanged(nameof(YpNextResourceList));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        private string _selectedNextResource;
        public string SelectedNextResource
        {
            get { return _selectedNextResource; }
            set
            {
                _selectedNextResource = value;
                OnPropertyChanged(nameof(SelectedNextResource));
            }

        }

      

    }
}
