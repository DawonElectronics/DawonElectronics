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
using DAWON_UV_INVENTORY_PROTO.Models;
using Syncfusion.Windows.Shared;

namespace DAWON_UV_INVENTORY_PROTO.Views
{
    /// <summary>
    /// Interaction logic for RtrLossInputWindow.xaml
    /// </summary>
    
    public partial class RtrLossInputWindow : ChromelessWindow
    {
        ViewUvWorkorder _record;
        public RtrLossInputWindow(ViewUvWorkorder record)
        {
            this._record = record;

            InitializeComponent();
        }

        private void BtnComplete_OnClick(object sender, RoutedEventArgs e)
        {
            var rtr_additional_pnl = Convert.ToInt16(tbox_additional.Text);
            var rtr_loss_test_pnl = Convert.ToInt16(TboxLoss1.Text);
            var rtr_loss_seam_pnl = Convert.ToInt16(TboxLoss2.Text);
            var rtr_loss_wrinkle_pnl = Convert.ToInt16(TboxLoss3.Text);
            var rtr_loss_approval_pnl = Convert.ToInt16(TboxLoss4.Text);
            var rtr_loss_etc_pnl = Convert.ToInt16(TboxLoss5.Text);

            //using (var db = new Db_Uv_InventoryContext())
            //    var result = db.TbUvToolinfo.Where(w => w.ProductId == record.ProductId).FirstOrDefault();

            //    if (result != null)
            //    {
            //        result.YpNextResourceDefault = record.YpNextResourceDefault;

            //        await db.SaveChangesAsync();
            //    }
            
        }
    }
}
