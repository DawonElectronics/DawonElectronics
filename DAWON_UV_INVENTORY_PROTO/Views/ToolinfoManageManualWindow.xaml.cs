using DAWON_UV_INVENTORY_PROTO.Models;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using System.Windows.Shapes;

namespace DAWON_UV_INVENTORY_PROTO.Views
{
    /// <summary>
    /// ToolinfoManageWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ToolinfoManageManualWindow : ChromelessWindow
    {
        public ToolinfoManageManualWindow()
        {
            InitializeComponent();
            CmbInputCustomer.ItemsSource = DAWON_UV_INVENTORY_PROTO.MainWindow.Tbcustomer.Select(x => x.CustName);
        }
    }
}
