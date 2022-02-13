using DAWON_UV_INVENTORY_PROTO.ViewModels;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            CmbUserlist.ItemsSource = MainWindow.MainwindowViewModel.Users;
            CmbUserlist.SelectedIndex = 0;
        }

        private void btn_select_user_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainwindowViewModel.SelectedUser = CmbUserlist.SelectedValue.ToString();
            this.Close();
        }
    }
}
