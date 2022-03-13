using DAWON_UV_INVENTORY_PROTO.Models;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;

namespace DAWON_UV_INVENTORY_PROTO.Views
{
    /// <summary>
    /// CustomerManageWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CustomerManageWindow : ChromelessWindow
    {
        public CustomerManageWindow()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
            this.GridCustomer.AutoGeneratingColumn += Grid_AutoGeneratingColumn;
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


                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(Newtonsoft.Json.JsonConvert.SerializeObject(context.TbCustomer));
                    dt.Columns["CustId"].ColumnName = "고객사 아이디";
                    dt.Columns["CustName"].ColumnName = "고객사명";
                    dt.Columns["CustCode"].SetOrdinal(1);
                    dt.Columns["CustCode"].ColumnName = "고객사코드";

                    GridCustomer.ItemsSource = dt.DefaultView;


                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

        }

        private void qry_customer()
        {
            try
            {
                using (var context = new Db_Uv_InventoryContext())
                {

                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(Newtonsoft.Json.JsonConvert.SerializeObject(context.TbCustomer));
                    dt.Columns["CustId"].ColumnName = "고객사 아이디";
                    dt.Columns["CustName"].ColumnName = "고객사명";
                    dt.Columns["CustCode"].SetOrdinal(1);
                    dt.Columns["CustCode"].ColumnName = "고객사코드";

                    GridCustomer.ItemsSource = dt.DefaultView;

                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            // Release all managed resources
            if (this.GridCustomer != null)
            {
                this.GridCustomer.Dispose();
                this.GridCustomer = null;
            }
            if (this.TboxInputCustcode != null)
                this.TboxInputCustcode = null;
            if (this.TboxInputCustname != null)
                this.TboxInputCustname = null;

            base.OnClosing(e);
        }

        private void btn_qry_customer_Click(object sender, RoutedEventArgs e)
        {
            qry_customer();
        }

        private void btn_add_customer_Click(object sender, RoutedEventArgs e)
        {
            var dept = String.Empty;
            try
            {
                using (var context = new Db_Uv_InventoryContext())
                {
                    var custlist = context.TbCustomer.ToList<TbCustomer>();
                    if (CmbDept.SelectedItem != null)
                        dept = CmbDept.Text;
                    var custidCount = custlist.Where(w => w.CustId.Contains(dept)).Count();

                    if (custlist.Where(w => w.CustCode.Contains(TboxInputCustcode.Text)).Count() > 0)
                    {
                        goto EXIT;
                    }

                    var inputTemp = new TbCustomer();
                    inputTemp.CustId = dept + "_" + (custidCount).ToString();
                    if (TboxInputCustname != null)
                        inputTemp.CustName = TboxInputCustname.Text;
                    if (TboxInputCustcode != null)
                        inputTemp.CustCode = TboxInputCustcode.Text;

                    context.TbCustomer.AddAsync(inputTemp);
                    context.SaveChanges();

                    TboxInputCustname.Clear();
                    TboxInputCustcode.Clear();
                    qry_customer();
                    MainWindow._mainwindowViewModel.Customer = new ObservableCollection<TbCustomer>(context.TbCustomer);
                EXIT:
                    MessageBox.Show("이미 있는 고객사 코드입니다 다른 코드를 입력해주세요");

                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }


    }
}
