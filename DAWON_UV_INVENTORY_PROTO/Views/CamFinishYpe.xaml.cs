using DAWON_UV_INVENTORY_PROTO.Models;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DAWON_UV_INVENTORY_PROTO.ViewModels;

namespace DAWON_UV_INVENTORY_PROTO.Views
{
    /// <summary>
    /// Interaction logic for CamFinishYpe.xaml
    /// </summary>
    public partial class CamFinishYpe : ChromelessWindow
    {
        ViewUvWorkorder record;
        GridRecordContextMenuInfo objsender = new GridRecordContextMenuInfo();
        
        public CamFinishYpe(object obj)
        {
            record = (ViewUvWorkorder)(obj as GridRecordContextMenuInfo).Record;
            objsender = (obj as GridRecordContextMenuInfo);
            CamFinishYpeViewModel viewmodel = new CamFinishYpeViewModel();
            InitializeComponent();

            if (record != null)
            {
                
                TblkModelName.Text = record.CustModelname;
                TblkTool.Text = record.CustToolno;
                TblkMesPrcName.Text = record.MesPrcName;
                TboxCsHoleCount.Text = MainWindow._mainwindowViewModel.ToolInfos
                    .Where(w => w.ProductId == record.ProductId).Select(s => s.HoleCount1).First();
                TboxSsHoleCount.Text = MainWindow._mainwindowViewModel.ToolInfos
                    .Where(w => w.ProductId == record.ProductId).Select(s => s.HoleCount2).First();

                if (record.PrcLayer2.Length < 2 && record.PrcLayer1.Length < 2)
                {
                    TboxSsHoleCount.IsEnabled = false;
                    TboxCsHoleCount.IsEnabled = false;
                }
                else if (record.PrcLayer2.Length < 2)
                {
                    TboxSsHoleCount.IsEnabled = false;
                }
                else if (record.PrcLayer1.Length < 2)
                {
                    TboxCsHoleCount.IsEnabled = false;
                }

                viewmodel.YpNextResourceList = DelimitedString2List(record.YpNextResourcelist);
            }

            this.DataContext = viewmodel;
        }

        private List<string> DelimitedString2List(string txt)
        {
            var result = new List<string>();
            if (txt.Contains(","))
            {
                foreach (var item in txt.Split(',').ToList())
                {
                    result.Add(item);
                }
            }
            return result;

        }

        private void BtnComplete_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new Db_Uv_InventoryContext())
                {
                    var result = db.TbUvToolinfo.SingleOrDefault(x => x.ProductId == record.ProductId);
                    if (result != null)
                    {
                        result.HoleCount1 = TboxCsHoleCount.Text;
                        result.HoleCount2 = TboxSsHoleCount.Text;
                        if (TboxCsHoleCount.Text.Length > 1 && TboxSsHoleCount.Text.Length > 1)
                        {
                            result.HoleCount = "CS: " + TboxCsHoleCount.Text + " SS: " + TboxSsHoleCount.Text;
                        }
                        else if (TboxCsHoleCount.Text.Length > 1 && TboxSsHoleCount.Text.Length < 1)
                        {
                            result.HoleCount = "CS: " + TboxCsHoleCount.Text;
                        }
                        else if (TboxCsHoleCount.Text.Length < 1 && TboxSsHoleCount.Text.Length > 1)
                        {
                            result.HoleCount = "SS: " + TboxSsHoleCount.Text;
                        }

                        result.YpNextResourceDefault = CmbNextResource.SelectedValue.ToString();
                        result.CamFinished = true;
                        db.SaveChanges();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnReset_OnClick(object sender, RoutedEventArgs e)
        {
            TboxCsHoleCount.Text = string.Empty;
            TboxSsHoleCount.Text = string.Empty;
            CmbNextResource.SelectedItem = null;
        }
    }
    
}
