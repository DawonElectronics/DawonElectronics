using DAWON_UV_INVENTORY_PROTO.Models;
using DAWON_UV_INVENTORY_PROTO.ViewModels;
using Syncfusion.Windows.Shared;
using System.Collections.Generic;

namespace DAWON_UV_INVENTORY_PROTO.Views
{
    /// <summary>
    /// Interaction logic for MachineChooserWindow.xaml
    /// </summary>
    public partial class ToolInfoEditorWindow : ChromelessWindow
    {
        private ToolInfoEditorViewModel _viewmodel;
        public ToolInfoEditorWindow(object obj)
        {
            InitializeComponent();
            var toolinfosrc = new List<TbUvToolinfo>();
            toolinfosrc.Add(obj as TbUvToolinfo);
            GridToolinfoEdit.ItemsSource = toolinfosrc;


            lbl_pid.Content = ((TbUvToolinfo)obj).ProductId;
        }

    }
}
