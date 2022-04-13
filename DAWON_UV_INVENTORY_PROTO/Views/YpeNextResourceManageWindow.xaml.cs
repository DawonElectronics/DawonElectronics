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
using DAWON_UV_INVENTORY_PROTO.ViewModels;
using Syncfusion.Windows.Shared;

namespace DAWON_UV_INVENTORY_PROTO.Views
{
    /// <summary>
    /// Interaction logic for YpeNextResourceManageWindow.xaml
    /// </summary>
    public partial class YpeNextResourceManageWindow : ChromelessWindow
    {
        private YpeNextResourceManageViewModel _viewmodel=new YpeNextResourceManageViewModel();
        public YpeNextResourceManageWindow()
        {
            InitializeComponent();
            using (var db = new Db_Uv_InventoryContext())
            {
                var ypsrcqry =
                    from wo in db.TbUvWorkorder.Where(w => w.TrackinTime > DateTime.Now.AddMonths(-2) && w.CustId =="UV_03").Select(s=>s.ProductId)
                    join toolinfo in db.TbUvToolinfo.Where(w => w.CustId == "UV_03")
                        on wo equals toolinfo.ProductId
                    select new YpeNextResourceModel() {MesPrcName = toolinfo.MesPrcName, ProductId = toolinfo.ProductId, CustModelName = toolinfo.CustModelname, CustRevision = toolinfo.CustRevision, CustToolno = toolinfo.CustToolno,YpNextResourcelist = toolinfo.YpNextResourcelist,YpNextResourceDefault = toolinfo.YpNextResourceDefault};
                
                _viewmodel.NextResourceModels = ypsrcqry.ToList();

                this.DataContext = _viewmodel;
            }
                
        }

        private async void YpNextResourceCombo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_viewmodel.SelectedGridItem != null)
            {
                using (var db = new Db_Uv_InventoryContext())
                {
                    var record = _viewmodel.SelectedGridItem;

                    var result = db.TbUvToolinfo.Where(w => w.ProductId == record.ProductId).FirstOrDefault();

                    if (result != null)
                    {
                        result.YpNextResourceDefault = record.YpNextResourceDefault;

                        await db.SaveChangesAsync();
                    }
                }
            }
           
        }
    }

}
