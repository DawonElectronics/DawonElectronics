using DAWON_UV_INVENTORY_PROTO.Models;
using DAWON_UV_INVENTORY_PROTO.ViewModels;
using Syncfusion.Windows.Shared;
using System.Linq;
using System.Windows;

namespace DAWON_UV_INVENTORY_PROTO.Views
{
    /// <summary>
    /// Interaction logic for MachineChooserWindow.xaml
    /// </summary>
    public partial class MachineChooserWindow : ChromelessWindow
    {
        private MachineChooserViewModel _mcviewmodel;
        public MachineChooserWindow(object obj)
        {
            InitializeComponent();
            _mcviewmodel = new MachineChooserViewModel();
            _mcviewmodel.WorkorderRecord = (ViewUvWorkorder)obj;
            chkListBox.DataContext = _mcviewmodel;


            _mcviewmodel.DbMachineString = _mcviewmodel.WorkorderRecord.MachineCs;
            lbl_lotid.Content = ((ViewUvWorkorder)obj).Lotid;



        }



        private void RdoCs_OnChecked(object sender, RoutedEventArgs e)
        {
            if (_mcviewmodel != null)
                _mcviewmodel.DbMachineString = _mcviewmodel.WorkorderRecord.MachineCs;
        }

        private void RdoSs_OnChecked(object sender, RoutedEventArgs e)
        {
            if (_mcviewmodel != null)
                _mcviewmodel.DbMachineString = _mcviewmodel.WorkorderRecord.MachineSs;
        }
        private void BtnCsAssign_OnClick(object sender, RoutedEventArgs e)
        {
            if (_mcviewmodel.SelectedMachine != null)
            {
               
                var rowdata = _mcviewmodel.WorkorderRecord;
                var qryid = rowdata.Id;
                var changevalue = _mcviewmodel.DbMachineString.ToString();
                using (var db = new Db_Uv_InventoryContext())
                {
                    var result = db.TbUvWorkorder.SingleOrDefault(x => x.Id == qryid);
                    if (result != null)
                    {
                        result.MachineCs = changevalue;
                        _mcviewmodel.WorkorderRecord.MachineCs = changevalue;
                        db.SaveChanges();
                        using (var mw = new MainWindow())
                        {                            
                            mw.UpdateFiltered_WorkorderList();
                        }
                        lbl_csResult.Content = "CS: 처리완료";
                    }
                }
            }
        }
        private void BtnSsAssign_OnClick(object sender, RoutedEventArgs e)
        {
            if (_mcviewmodel.SelectedMachine != null)
            {
                
                var rowdata = _mcviewmodel.WorkorderRecord;
                var qryid = rowdata.Id;
                var changevalue = _mcviewmodel.DbMachineString.ToString();
                using (var db = new Db_Uv_InventoryContext())
                {
                    var result = db.TbUvWorkorder.SingleOrDefault(x => x.Id == qryid);
                    if (result != null)
                    {
                        result.MachineSs = changevalue;
                        _mcviewmodel.WorkorderRecord.MachineSs = changevalue;
                        db.SaveChanges();
                        using (var mw = new MainWindow())
                        {
                            mw.UpdateFiltered_WorkorderList();
                        }
                        lbl_ssResult.Content = "SS: 처리완료";
                    }
                }
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _mcviewmodel.SelectedMachine.Clear();
        }
    }
}
