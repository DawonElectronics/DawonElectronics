using DAWON_UV_INVENTORY_PROTO.ViewModels;
using Syncfusion.Windows.Shared;
using System;
using System.Linq;
using System.Windows;

namespace DAWON_UV_INVENTORY_PROTO.Views
{
    /// <summary>
    /// Interaction logic for TrackinWindowGeneral.xaml
    /// </summary>
    public partial class TrackinWindowGeneral : ChromelessWindow
    {
        private TrackInWindowGeneralViewModel _viewmodel;
        public TrackinWindowGeneral()
        {
            InitializeComponent();
            _viewmodel = new TrackInWindowGeneralViewModel();

            _viewmodel.ToolInfos = MainWindow._mainwindowViewModel.ToolInfos;
            _viewmodel.Customer = MainWindow._mainwindowViewModel.Customer;
            cmb_input_cust.SelectedValue = MainWindow._mainwindowViewModel.SelectedCustomerWo;

            this.DataContext = _viewmodel;

        }

        private void BtnAddLot_Click(object sender, RoutedEventArgs e)
        {
            //1.샘플 여부
            var sample = chkbox_issample.IsChecked;

            //양산
            if (sample != true)
            {
                var temprecord = new TrackinModel();

                var toolinfo = _viewmodel.ToolInfos.Where(w => w.ProductId == _viewmodel.Toolno2Pid).FirstOrDefault();
                temprecord.Lotid = tbox_lotid.Text;
                temprecord.Pnlqty = Convert.ToInt16(tbox_pnlqty.Text);
                temprecord.Sample = sample;
                temprecord.CustName = _viewmodel.SelectedCustomerTrackinForm;
                temprecord.CustModelname = toolinfo.CustModelname;
                temprecord.CustRevision = toolinfo.CustRevision;
                temprecord.CustToolno = toolinfo.CustToolno;
                temprecord.MesPrcName = toolinfo.MesPrcName;
                temprecord.PrcName = toolinfo.PrcName;
                temprecord.MesSeqCode = toolinfo.MesSeqCode;
                temprecord.LotNotes = tbox_lot_notes.Text;

                _viewmodel.TrackinList.Add(temprecord);
            }
            else if (sample == true)
            {
                var temprecord = new TrackinModel();

                var toolinfo = _viewmodel.ToolInfos.Where(w => w.ProductId == _viewmodel.Toolno2Pid).FirstOrDefault();
                temprecord.Lotid = tbox_lotid.Text;
                temprecord.Pnlqty = Convert.ToInt16(tbox_pnlqty.Text);
                temprecord.Sample = sample;
                temprecord.SampleDept = TboxSampleDept.Text;
                temprecord.CustName = _viewmodel.SelectedCustomerTrackinForm;
                temprecord.CustModelname = toolinfo.CustModelname;
                temprecord.CustRevision = toolinfo.CustRevision;
                temprecord.CustToolno = toolinfo.CustToolno;
                temprecord.MesPrcName = toolinfo.MesPrcName;
                temprecord.PrcName = toolinfo.PrcName;
                temprecord.MesSeqCode = toolinfo.MesSeqCode;
                temprecord.LotNotes = tbox_lot_notes.Text;

                _viewmodel.TrackinList.Add(temprecord);
            }
        }
    }
}
