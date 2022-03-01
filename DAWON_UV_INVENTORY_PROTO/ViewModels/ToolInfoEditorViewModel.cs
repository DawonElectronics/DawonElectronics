using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAWON_UV_INVENTORY_PROTO.Models;
using Syncfusion.Data.Extensions;

namespace DAWON_UV_INVENTORY_PROTO.ViewModels
{
    public class ToolInfoEditorViewModel : INotifyPropertyChanged
    {
        public object SelectedTool { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
        
        

    }
}
