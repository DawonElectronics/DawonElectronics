using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using ConnectorYPE;
using ConnectorYPE.Models;
using DAWON_UV_INVENTORY_PROTO.ViewModels;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.Windows.Shared;
using ConnectorBHE;

namespace DAWON_UV_INVENTORY_PROTO.Views
{
    /// <summary>
    /// Interaction logic for TrackinWindowBH.xaml
    /// </summary>
    public partial class TrackinWindowBH : ChromelessWindow
    {
        Regex re_yplot = new Regex(@"[0-9]{6}.[0-9]{3}.[0-9].[A-Z]{2}[0-9]{2}.[0-9]{3}.[0-9]{3}");
        public TrackInWindowBHViewModel _viewmodel = new TrackInWindowBHViewModel();
        private BheHelper BheHelper = new BheHelper();
        public TrackinWindowBH()
        {
            InitializeComponent();
            var rcvdt = BheHelper.GetWip(DateTime.Now.AddMonths(-1), DateTime.Today);
            GridRcv.ItemsSource = rcvdt.DefaultView;
        }

        private async void GridRcv_OnItemsSourceChanged(object? sender, GridItemsSourceChangedEventArgs e)
        {
            await Task.Delay(1900);
            sfBusyIndicator.IsBusy = false;
        }
        private void GridRcv_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            //if (Keyboard.IsKeyDown(Key.LeftCtrl))
            //{
            //    if (re_yplot.IsMatch(e.Key.ToString())) e.Handled = true;
            //    var a = Clipboard.GetText();
            //    foreach (var item in _viewmodel.RcvLotList)
            //    {
            //        WipModelAfterToolValidation record = item;

            //        if (record.lotid == a)
            //        {
            //            //TrackinYpeViewmodel.SelectedItem = item;
            //            GridRcv.SelectedItems.Add(item);
            //            break;
            //        }

            //    }
            //}
        }
        private void TboxLot_OnKeyUp(object sender, KeyEventArgs e)
        {
            //if (re_yplot.IsMatch(e.Key.ToString())) e.Handled = true;
            //var lot = re_yplot.Match(tboxLot.Text).Value;

            //foreach (var item in _viewmodel.RcvLotList)
            //{
            //    WipModelAfterToolValidation record = item;

            //    if (record.lotid == lot)
            //    {
            //        GridRcv.SelectedItems.Add(item);
            //        tboxLot.Text = string.Empty;
            //        tboxLot.Focus();
            //        break;
            //    }

            //}
        }
    }
}
