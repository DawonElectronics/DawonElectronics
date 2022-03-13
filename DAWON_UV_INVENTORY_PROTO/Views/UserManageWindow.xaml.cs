using DAWON_UV_INVENTORY_PROTO.Models;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.Windows.Shared;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;

namespace DAWON_UV_INVENTORY_PROTO.Views
{
    /// <summary>
    /// UserManageWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UserManageWindow : ChromelessWindow
    {
        public UserManageWindow()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
            this.GridUsers.AutoGeneratingColumn += Grid_AutoGeneratingColumn;
        }
        private void Grid_AutoGeneratingColumn(object? sender, AutoGeneratingColumnArgs e)
        {
            e.Column.TextAlignment = TextAlignment.Center;
        }
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new Db_Uv_InventoryContext())
                {

                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(Newtonsoft.Json.JsonConvert.SerializeObject(context.TbUsers));
                    dt.Columns["UserId"].ColumnName = "사용자 ID";
                    dt.Columns["UserName"].ColumnName = "사용자명";
                    dt.Columns["UserGroup"].ColumnName = "소속";
                    dt.Columns["UserRole"].ColumnName = "업무";

                    GridUsers.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

        }

        private void qry_user()
        {
            try
            {
                using (var context = new Db_Uv_InventoryContext())
                {

                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(Newtonsoft.Json.JsonConvert.SerializeObject(context.TbUsers));
                    dt.Columns["UserId"].ColumnName = "사용자 ID";
                    dt.Columns["UserName"].ColumnName = "사용자명";
                    dt.Columns["UserGroup"].ColumnName = "소속";
                    dt.Columns["UserRole"].ColumnName = "담당업무";

                    GridUsers.ItemsSource = dt.DefaultView;

                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            // Release all managed resources
            if (this.GridUsers != null)
            {
                this.GridUsers.Dispose();
                this.GridUsers = null;
            }
            if (this.TboxInputUsername != null)
                this.TboxInputUsername = null;
            if (this.TboxInputUserid != null)
                this.TboxInputUserid = null;
            if (this.TboxInputUserrole != null)
                this.TboxInputUserrole = null;

            base.OnClosing(e);
        }

        private void btn_qry_user_Click(object sender, RoutedEventArgs e)
        {
            qry_user();
        }

        private void btn_add_user_Click(object sender, RoutedEventArgs e)
        {
            var dept = String.Empty;
            try
            {
                using (var context = new Db_Uv_InventoryContext())
                {
                    var userlist = context.TbUsers.ToList<TbUsers>();

                    if (CmbDept.SelectedItem != null)
                        dept = CmbDept.Text;


                    if (userlist.Where(w => w.UserId == TboxInputUserid.Text).Count() > 0)
                    {
                        goto EXIT;
                    }

                    var inputTemp = new TbUsers();
                    if (TboxInputUserid != null)
                        inputTemp.UserId = TboxInputUserid.Text;
                    if (TboxInputUsername != null)
                        inputTemp.UserName = TboxInputUsername.Text;
                    if (TboxInputUserrole != null)
                        inputTemp.UserRole = TboxInputUserrole.Text;
                    inputTemp.UserGroup = dept;

                    context.TbUsers.AddAsync(inputTemp);
                    context.SaveChanges();
                    TboxInputUserid.Clear();
                    TboxInputUsername.Clear();
                    TboxInputUserrole.Clear();
                    qry_user();
                    MainWindow._mainwindowViewModel.Users = context.TbUsers.Select(x => x.UserName).ToList<string>();
                EXIT:
                    MessageBox.Show("이미 있는 사용자 ID 입니다 다른 ID를 입력해주세요");

                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

    }
}
