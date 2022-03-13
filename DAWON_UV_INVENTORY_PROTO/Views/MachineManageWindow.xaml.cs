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
    /// MachineManageWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MachineManageWindow : ChromelessWindow
    {
        public MachineManageWindow()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
            this.GridMachine.AutoGeneratingColumn += Grid_AutoGeneratingColumn;
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

                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(Newtonsoft.Json.JsonConvert.SerializeObject(context.TbMachine));
                    dt.Columns["MachineId"].ColumnName = "설비ID";
                    dt.Columns["MachineName"].ColumnName = "설비호기";
                    dt.Columns["Department"].ColumnName = "소속";
                    dt.Columns["MachineModelname"].ColumnName = "설비기종";
                    dt.Columns["MachineMaker"].ColumnName = "설비제조사";

                    GridMachine.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

        }
        private void qry_machine()
        {
            try
            {
                using (var context = new Db_Uv_InventoryContext())
                {

                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(Newtonsoft.Json.JsonConvert.SerializeObject(context.TbMachine));
                    dt.Columns["MachineId"].ColumnName = "설비ID";
                    dt.Columns["MachineName"].ColumnName = "설비호기";
                    dt.Columns["Department"].ColumnName = "소속";
                    dt.Columns["MachineModelname"].ColumnName = "설비기종";
                    dt.Columns["MachineMaker"].ColumnName = "설비제조사";

                    GridMachine.ItemsSource = dt.DefaultView;

                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            // Release all managed resources
            if (this.GridMachine != null)
            {
                this.GridMachine.Dispose();
                this.GridMachine = null;
            }
            if (this.TboxInputMachineName != null)
                this.TboxInputMachineName = null;
            if (this.TboxInputMachineModelname != null)
                this.TboxInputMachineModelname = null;
            if (this.TboxInputMachineMaker != null)
                this.TboxInputMachineMaker = null;

            base.OnClosing(e);
        }
        private void btn_add_machine_Click(object sender, RoutedEventArgs e)
        {
            var dept = String.Empty;
            try
            {
                using (var context = new Db_Uv_InventoryContext())
                {
                    var machinelist = context.TbMachine.ToList<TbMachine>();

                    if (CmbDept.SelectedItem != null)
                        dept = CmbDept.Text;
                    var machineidCount = machinelist.Where(w => w.MachineId.Contains(dept)).Count();

                    if (machinelist.Where(w => w.MachineName == TboxInputMachineName.Text).Count() > 0)
                    {
                        goto EXIT;
                    }

                    var inputTemp = new TbMachine();
                    if (TboxInputMachineModelname != null)
                        inputTemp.MachineModelname = TboxInputMachineModelname.Text;
                    if (TboxInputMachineMaker != null)
                        inputTemp.MachineMaker = TboxInputMachineMaker.Text;
                    if (TboxInputMachineName != null)
                        inputTemp.MachineName = TboxInputMachineName.Text;
                    inputTemp.MachineId = dept + "_" + (machineidCount).ToString("D3");

                    context.TbMachine.AddAsync(inputTemp);
                    context.SaveChanges();
                    TboxInputMachineName.Clear();
                    TboxInputMachineMaker.Clear();
                    TboxInputMachineModelname.Clear();
                    qry_machine();
                    MainWindow._mainwindowViewModel.Machines = new ObservableCollection<TbMachine>(context.TbMachine);
                EXIT:
                    MessageBox.Show("이미 있는 호기 입니다 다른 호기를 입력해주세요");

                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btn_qry_machine_Click(object sender, RoutedEventArgs e)
        {
            qry_machine();
        }
    }
}
