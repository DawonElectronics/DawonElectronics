using DAWON_UV_INVENTORY_PROTO.Models;
using Syncfusion.UI.Xaml.Grid;
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
    /// Interaction logic for EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : ChromelessWindow
    {
        ViewUvWorkorder record;
        List<TbUsers> _users = new List<TbUsers>();
        GridRecordContextMenuInfo objsender = new GridRecordContextMenuInfo();
         public EditUserWindow(object obj)
        {
            InitializeComponent();
            
            record = (ViewUvWorkorder)(obj as GridRecordContextMenuInfo).Record;
            objsender = (obj as GridRecordContextMenuInfo);
            _users = MainWindow._mainwindowViewModel.UserList;
            ComboTrackinUserList.ItemsSource = _users.Where(x=>x.IsRetired==false).Select(x=>x.UserName).ToList<string>();
            ComboTrackoutUserList.ItemsSource = _users.Where(x => x.IsRetired == false).Select(x => x.UserName).ToList<string>();

            if (record != null)
            {
                if (record.IsDone == true)
                {
                    ComboTrackinUserList.SelectedValue = record.TrackinUsername;
                    ComboTrackoutUserList.SelectedValue = record.TrackoutUsername;
                }
                else if (record.IsDone == false)
                {
                    ComboTrackinUserList.SelectedValue = record.TrackinUsername;
                }
            }
           
        }

        private void BtnEditUser_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new Db_Uv_InventoryContext())
            {
                var result = db.TbUvWorkorder.SingleOrDefault(x => x.Id == record.Id);
                if (result != null)
                {
                    if (record.IsDone == true)
                    {
                        var inuser = ComboTrackinUserList.SelectedValue.ToString();
                        var outuser = ComboTrackoutUserList.SelectedValue.ToString();

                        result.TrackinUserId = _users.Where(x => x.UserName == inuser).Select(x => x.UserId).FirstOrDefault();
                        result.TrackoutUserId = _users.Where(x => x.UserName == outuser).Select(x => x.UserId).FirstOrDefault();
                        MainWindow._mainwindowViewModel.SelectedGridWip.TrackinUsername = inuser;
                        MainWindow._mainwindowViewModel.SelectedGridWip.TrackoutUsername = outuser;
                        objsender.DataGrid.View.Refresh();
                        db.SaveChanges();
                    }
                    else if (record.IsDone == false)
                    {
                        var inuser = ComboTrackinUserList.SelectedValue.ToString();
                        

                        result.TrackinUserId = _users.Where(x => x.UserName == inuser).Select(x => x.UserId).FirstOrDefault();
                        
                        MainWindow._mainwindowViewModel.SelectedGridWip.TrackinUsername = inuser;
                        objsender.DataGrid.View.Refresh();
                        db.SaveChanges();
                    }

                }
            }

            if (MessageBox.Show("변경되었습니다", "변경완료",
                   MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
            {
               this.Close();
            }
        }

       
    }
}
