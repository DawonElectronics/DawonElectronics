using Syncfusion.Windows.Shared;
using System.Windows;

namespace DAWON_UV_INVENTORY_PROTO.Views
{
    /// <summary>
    /// Interaction logic for UserSelectPopup.xaml
    /// </summary>
    public partial class UserSelectPopup : ChromelessWindow
    {
        public UserSelectPopup()
        {
            InitializeComponent();

            CmbUserlist.ItemsSource = MainWindow._mainwindowViewModel.Users;
            CmbUserlist.SelectedIndex = 0;
        }

        private void btn_select_user_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._mainwindowViewModel.SelectedUser = CmbUserlist.SelectedValue.ToString();
            this.Close();
        }
    }
}
