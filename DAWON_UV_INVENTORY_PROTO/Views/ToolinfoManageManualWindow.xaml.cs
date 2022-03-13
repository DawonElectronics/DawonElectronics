using DAWON_UV_INVENTORY_PROTO.ViewModels;
using Syncfusion.Windows.Shared;
using System.Windows;

namespace DAWON_UV_INVENTORY_PROTO.Views
{
    /// <summary>
    /// ToolinfoManageWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ToolinfoManageManualWindow : ChromelessWindow
    {
        public static ToolinfoManagerManualWindowViewmodel _viewmodel = new ToolinfoManagerManualWindowViewmodel();
        public ToolinfoManageManualWindow()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;

        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = _viewmodel;
            _viewmodel.Customer = MainWindow._mainwindowViewModel.Customer;
            _viewmodel.PrcTypes = MainWindow._mainwindowViewModel.PrcTypes;
            _viewmodel.UserList = MainWindow._mainwindowViewModel.UserList;
            _viewmodel.SelectedCustomerWo = MainWindow._mainwindowViewModel.SelectedCustomerWo;
            _viewmodel.SelectedUser = MainWindow._mainwindowViewModel.SelectedUser;
            _viewmodel.SelectedIsSampleWo = MainWindow._mainwindowViewModel.SelectedIsSampleWo;
        }

        private void BtnRegist_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
