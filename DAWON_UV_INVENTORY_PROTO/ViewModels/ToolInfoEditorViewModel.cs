using System;
using System.ComponentModel;

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
