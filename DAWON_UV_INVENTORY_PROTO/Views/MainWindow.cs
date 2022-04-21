using System.ComponentModel;
using System.Linq;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.Windows.Controls.Input;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DAWON_UV_INVENTORY_PROTO.Models;
using SelectionChangedEventArgs = System.Windows.Controls.SelectionChangedEventArgs;

namespace DAWON_UV_INVENTORY_PROTO
{
    public partial class MainWindow
    {
        private GridTemplateColumn MachineColumn(string mapping, string header)
        {
            //CellTemplate creation.
            DataTemplate cellTemplate = new DataTemplate();
            FrameworkElementFactory frameworkElement1 = new FrameworkElementFactory(typeof(TextBlock));
            Binding displayBinding = new Binding() { Path = new PropertyPath("CustomerID") };
            frameworkElement1.SetValue(TextBlock.TextProperty, displayBinding);
            cellTemplate.VisualTree = frameworkElement1;

            //EditTemplate creation.
            DataTemplate editTemplate = new DataTemplate();
            FrameworkElementFactory frameworkElement2 = new FrameworkElementFactory(typeof(TextBox));
            Binding editBinding = new Binding() { Path = new PropertyPath("CustomerID"), Mode = BindingMode.TwoWay };
            frameworkElement2.SetValue(TextBox.TextProperty, editBinding);
            editTemplate.VisualTree = frameworkElement2;
            return new GridTemplateColumn() { HeaderText = "Customer ID", MappingName = "CustomerID", CellTemplate = cellTemplate, EditTemplate = editTemplate, SetCellBoundValue = false };
        }
        //업체별 컬럼 정의
        private void GridWipColumnDems()
        {
            var trackoutcol = new GridTemplateColumn() { MappingName = "ExecuteTrackout", HeaderText = "출고", Width = 70 };
            trackoutcol.CellTemplate = this.FindResource("trackoutTemplate") as DataTemplate;

            var createtimecol = new GridDateTimeColumn() { MappingName = "CreateTime", HeaderText = "생성일", Width = 80, CustomPattern = "yyyy/MM/dd hh:mm", Pattern = Syncfusion.Windows.Shared.DateTimePattern.ShortDate, TextAlignment = TextAlignment.Center,AllowSorting = true};
            var lotcol = new GridTextColumn() { MappingName = "Lotid", HeaderText = "LOT", Width = 120, TextAlignment = TextAlignment.Center, AllowFiltering = false, AllowSorting = true };
            var modelnamecol = new GridTextColumn() { MappingName = "CustModelname", HeaderText = "모델명", AllowFiltering = true, Width = 170, TextAlignment = TextAlignment.Center, AllowSorting = true };
            var revcol = new GridTextColumn() { MappingName = "CustRevision", HeaderText = "REV", Width = 60, TextAlignment = TextAlignment.Center };
            var toolcol = new GridTextColumn() { MappingName = "CustToolno", HeaderText = "TOOL", AllowFiltering = true, Width = 110, TextAlignment = TextAlignment.Center, AllowSorting = true };
            var mesprcnamecol = new GridTextColumn() { MappingName = "MesPrcName", HeaderText = "공정명", AllowFiltering = true, Width = 80, TextAlignment = TextAlignment.Center, AllowSorting = true };
            var prcnamecol = new GridTextColumn() { MappingName = "PrcName", HeaderText = "공법", Width = 80, TextAlignment = TextAlignment.Center, AllowSorting = true };
            var pnlqtycol = new GridTextColumn() { MappingName = "Pnlqty", HeaderText = "수량", Width = 50, TextAlignment = TextAlignment.Center, AllowEditing = true };
            var trackintimecol = new GridDateTimeColumn() { MappingName = "TrackinTime", HeaderText = "입고시간", Width = 80, CustomPattern = "MM/dd HH:mm", Pattern = Syncfusion.Windows.Shared.DateTimePattern.CustomPattern, TextAlignment = TextAlignment.Center, AllowSorting = true };
            var trackinusercol = new GridTextColumn() { MappingName = "TrackinUsername", HeaderText = "입고자", Width = 60, TextAlignment = TextAlignment.Center, AllowSorting = true };
            var lotnotescol = new GridTextColumn()
            {
                MappingName = "LotNotes",
                HeaderText = "특이사항",
                AllowEditing = true,
                TextAlignment = TextAlignment.Center,
                MaximumWidth = 450,
                MinimumWidth = 250,
                TextWrapping = TextWrapping.Wrap
            };

            var machinecscol = new GridTextColumn() { MappingName = "MachineCs", HeaderText = "CS호기", AllowEditing = true, Width = 90, TextAlignment = TextAlignment.Center };
            var machinesscol = new GridTextColumn() { MappingName = "MachineSs", HeaderText = "SS호기", AllowEditing = true, Width = 90, TextAlignment = TextAlignment.Center };
            var totallayercol = new GridTextColumn() { MappingName = "Layer", HeaderText = "층수", Width = 40, TextAlignment = TextAlignment.Center };
            var prclayer1col = new GridTextColumn() { MappingName = "PrcLayer1", HeaderText = "가공층1", Width = 60, TextAlignment = TextAlignment.Center };
            var prclayer2col = new GridTextColumn() { MappingName = "PrcLayer2", HeaderText = "가공층2", Width = 60, TextAlignment = TextAlignment.Center };
            var holesizecol = new GridTextColumn() { MappingName = "MainHoleSize", HeaderText = "홀사이즈", Width = 80, TextAlignment = TextAlignment.Center };
            var depthcol = new GridTextColumn() { MappingName = "Depth", HeaderText = "뎁스", Width = 80, TextAlignment = TextAlignment.Center };
            var holecountcol = new GridTextColumn() { MappingName = "HoleCount", HeaderText = "홀수", Width = 120, TextAlignment = TextAlignment.Center };
            var endcustomercol = new GridTextColumn() { MappingName = "EndCustomer", HeaderText = "납품처", Width = 160, TextAlignment = TextAlignment.Center };
            var idcol = new GridTextColumn() { MappingName = "Id", IsHidden = true };
            var waittrackoutcol = new GridCheckBoxColumn() { MappingName = "WaitTrackout", IsHidden = true };
            var custnamecol = new GridCheckBoxColumn() { MappingName = "CustName", IsHidden = true };

            GridWip.Columns.Add(trackoutcol);
            GridWip.Columns.Add(createtimecol);
            GridWip.Columns.Add(lotcol);
            GridWip.Columns.Add(modelnamecol);
            GridWip.Columns.Add(revcol);
            GridWip.Columns.Add(toolcol);
            GridWip.Columns.Add(mesprcnamecol);
            GridWip.Columns.Add(prcnamecol);
            GridWip.Columns.Add(pnlqtycol);
            GridWip.Columns.Add(trackintimecol);
            GridWip.Columns.Add(trackinusercol);
            GridWip.Columns.Add(lotnotescol);
            GridWip.Columns.Add(machinecscol);
            GridWip.Columns.Add(machinesscol);
            GridWip.Columns.Add(totallayercol);
            GridWip.Columns.Add(prclayer1col);
            GridWip.Columns.Add(prclayer2col);
            GridWip.Columns.Add(holesizecol);
            GridWip.Columns.Add(depthcol);
            GridWip.Columns.Add(holecountcol);
            GridWip.Columns.Add(endcustomercol);
            GridWip.Columns.Add(idcol);
            GridWip.Columns.Add(waittrackoutcol);
            GridWip.Columns.Add(custnamecol);

            GridWip.Columns["CustName"].FilterPredicates.Clear();
            GridWip.Columns["CustName"].FilterPredicates.Add(new Syncfusion.Data.FilterPredicate() { FilterType = Syncfusion.Data.FilterType.Equals, FilterValue = "대덕전자(MS)" });

            GridWip.SortColumnDescriptions.Clear();
            GridWip.SortColumnDescriptions.Add(new SortColumnDescription() { ColumnName = "WaitTrackout", SortDirection = ListSortDirection.Descending });
            GridWip.SortColumnDescriptions.Add(new SortColumnDescription() { ColumnName = "Lotid", SortDirection = ListSortDirection.Ascending });
        }
        private void GridWipColumnDepkg()
        {
            var trackoutcol = new GridTemplateColumn() { MappingName = "ExecuteTrackout", HeaderText = "출고", Width = 70 };
            trackoutcol.CellTemplate = this.FindResource("trackoutTemplate") as DataTemplate;

            var createtimecol = new GridDateTimeColumn() { MappingName = "CreateTime", HeaderText = "생성일", Width = 80, CustomPattern = "yyyy/MM/dd hh:mm", Pattern = Syncfusion.Windows.Shared.DateTimePattern.ShortDate, TextAlignment = TextAlignment.Center, AllowSorting = true };
            var lotcol = new GridTextColumn() { MappingName = "Lotid", HeaderText = "LOT", Width = 120, TextAlignment = TextAlignment.Center, AllowFiltering = false, AllowSorting = true };
            var modelnamecol = new GridTextColumn() { MappingName = "CustModelname", HeaderText = "모델명", AllowFiltering = true, Width = 170, TextAlignment = TextAlignment.Center, AllowSorting = true };
            var revcol = new GridTextColumn() { MappingName = "CustRevision", HeaderText = "REV", Width = 60, TextAlignment = TextAlignment.Center, AllowSorting = true };
            var toolcol = new GridTextColumn() { MappingName = "CustToolno", HeaderText = "TOOL", AllowFiltering = true, Width = 110, TextAlignment = TextAlignment.Center, AllowSorting = true };
            var mesprcnamecol = new GridTextColumn() { MappingName = "MesPrcName", HeaderText = "공정명", AllowFiltering = true, Width = 80, TextAlignment = TextAlignment.Center, AllowSorting = true };
            var prcnamecol = new GridTextColumn() { MappingName = "PrcName", HeaderText = "공법", Width = 80, TextAlignment = TextAlignment.Center, AllowSorting = true };
            var pnlqtycol = new GridTextColumn() { MappingName = "Pnlqty", HeaderText = "수량", Width = 50, TextAlignment = TextAlignment.Center, AllowEditing = true, AllowSorting = true };
            var trackintimecol = new GridDateTimeColumn() { MappingName = "TrackinTime", HeaderText = "입고시간", Width = 80, CustomPattern = "MM/dd HH:mm", Pattern = Syncfusion.Windows.Shared.DateTimePattern.CustomPattern, TextAlignment = TextAlignment.Center, AllowSorting = true };
            var trackinusercol = new GridTextColumn() { MappingName = "TrackinUsername", HeaderText = "입고자", Width = 60, TextAlignment = TextAlignment.Center, AllowSorting = true };
            var lotnotescol = new GridTextColumn()
            {
                MappingName = "LotNotes",
                HeaderText = "특이사항",
                AllowEditing = true,
                TextAlignment = TextAlignment.Center,
                MaximumWidth = 450,
                MinimumWidth = 250,
                TextWrapping = TextWrapping.Wrap
            };
            var machinecscol = new GridTextColumn() { MappingName = "MachineCs", HeaderText = "CS호기", AllowEditing = true, Width = 90, TextAlignment = TextAlignment.Center };
            var machinesscol = new GridTextColumn() { MappingName = "MachineSs", HeaderText = "SS호기", AllowEditing = true, Width = 90, TextAlignment = TextAlignment.Center };
            var totallayercol = new GridTextColumn() { MappingName = "Layer", HeaderText = "층수", Width = 40, TextAlignment = TextAlignment.Center };
            var prclayer1col = new GridTextColumn() { MappingName = "PrcLayer1", HeaderText = "가공층1", Width = 60, TextAlignment = TextAlignment.Center };
            var prclayer2col = new GridTextColumn() { MappingName = "PrcLayer2", HeaderText = "가공층2", Width = 60, TextAlignment = TextAlignment.Center };
            var holesizecol = new GridTextColumn() { MappingName = "MainHoleSize", HeaderText = "홀사이즈", Width = 80, TextAlignment = TextAlignment.Center };
            var depthcol = new GridTextColumn() { MappingName = "Depth", HeaderText = "뎁스", Width = 80, TextAlignment = TextAlignment.Center };
            var holecountcol = new GridTextColumn() { MappingName = "HoleCount", HeaderText = "홀수", Width = 120, TextAlignment = TextAlignment.Center };
            var endcustomercol = new GridTextColumn() { MappingName = "EndCustomer", HeaderText = "납품처", Width = 160, TextAlignment = TextAlignment.Center };
            var idcol = new GridTextColumn() { MappingName = "Id", IsHidden = true };
            var waittrackoutcol = new GridCheckBoxColumn() { MappingName = "WaitTrackout", IsHidden = true };
            var prdtypecol = new GridTextColumn() { MappingName = "ProductType", HeaderText = "제품타입", Width = 100, TextAlignment = TextAlignment.Center };
            var custnamecol = new GridCheckBoxColumn() { MappingName = "CustName", IsHidden = true };

            GridWip.Columns.Add(trackoutcol);
            GridWip.Columns.Add(createtimecol);
            GridWip.Columns.Add(lotcol);
            GridWip.Columns.Add(modelnamecol);
            GridWip.Columns.Add(revcol);
            GridWip.Columns.Add(toolcol);
            GridWip.Columns.Add(mesprcnamecol);
            GridWip.Columns.Add(prcnamecol);
            GridWip.Columns.Add(pnlqtycol);
            GridWip.Columns.Add(trackintimecol);
            GridWip.Columns.Add(trackinusercol);
            GridWip.Columns.Add(lotnotescol);
            GridWip.Columns.Add(machinecscol);
            GridWip.Columns.Add(machinesscol);
            GridWip.Columns.Add(prdtypecol);
            GridWip.Columns.Add(totallayercol);
            GridWip.Columns.Add(prclayer1col);
            GridWip.Columns.Add(prclayer2col);
            GridWip.Columns.Add(holesizecol);
            GridWip.Columns.Add(depthcol);
            GridWip.Columns.Add(holecountcol);
            GridWip.Columns.Add(endcustomercol);
            GridWip.Columns.Add(idcol);
            GridWip.Columns.Add(waittrackoutcol);
            GridWip.Columns.Add(custnamecol);

            GridWip.Columns["CustName"].FilterPredicates.Clear();
            GridWip.Columns["CustName"].FilterPredicates.Add(new Syncfusion.Data.FilterPredicate() { FilterType = Syncfusion.Data.FilterType.Equals, FilterValue = "대덕전자(PKG)" });

            GridWip.SortColumnDescriptions.Clear();
            GridWip.SortColumnDescriptions.Add(new SortColumnDescription() { ColumnName = "WaitTrackout", SortDirection = ListSortDirection.Descending });
            GridWip.SortColumnDescriptions.Add(new SortColumnDescription() { ColumnName = "Lotid", SortDirection = ListSortDirection.Ascending });

        }

        private void GridWipColumnYpe()
        {
            var trackoutcol = new GridTemplateColumn() { MappingName = "ExecuteTrackout", HeaderText = "출고", Width = 60 };
            trackoutcol.CellTemplate = this.FindResource("trackoutTemplate") as DataTemplate;

            var createtimecol = new GridDateTimeColumn() { MappingName = "CreateTime", HeaderText = "생성일", Width = 80, CustomPattern = "yyyy/MM/dd hh:mm", Pattern = Syncfusion.Windows.Shared.DateTimePattern.ShortDate, TextAlignment = TextAlignment.Center, AllowSorting = true };
            var lotcol = new GridTextColumn() { MappingName = "Lotid", HeaderText = "LOT", Width = 170, TextAlignment = TextAlignment.Center, AllowFiltering = false, AllowSorting = true };
            //var ypshortlotcol = new GridTextColumn() { MappingName = "YpShortlot", HeaderText = "LOT2", Width = 60, TextAlignment = TextAlignment.Center, AllowFiltering = false };
            var modelnamecol = new GridTextColumn() { MappingName = "CustModelname", HeaderText = "모델명", AllowFiltering = true, Width = 170, TextAlignment = TextAlignment.Center, AllowSorting = true };
            var revcol = new GridTextColumn() { MappingName = "CustRevision", HeaderText = "REV", Width = 50, TextAlignment = TextAlignment.Center, AllowSorting = true };
            //var ypdatarevcol = new GridTextColumn() { MappingName = "YpeDatarev", HeaderText = "데이터", Width = 55, TextAlignment = TextAlignment.Center,AllowSorting = true };
            var toolcol = new GridTextColumn() { MappingName = "CustToolno", HeaderText = "TOOL", AllowFiltering = true, Width = 80, TextAlignment = TextAlignment.Center, AllowSorting = true };
            var mesprcnamecol = new GridTextColumn() { MappingName = "MesPrcName", HeaderText = "공정명", AllowFiltering = true, Width = 130, TextAlignment = TextAlignment.Center, AllowSorting = true };
            var prcnamecol = new GridTextColumn() { MappingName = "PrcName", HeaderText = "공법", Width = 80, TextAlignment = TextAlignment.Center, AllowSorting = true };
            prcnamecol.CellStyle = this.FindResource("PrcTypeBgStyle") as Style; 
            var pnlqtycol = new GridTextColumn() { MappingName = "Pnlqty", HeaderText = "수량", Width = 50, TextAlignment = TextAlignment.Center, AllowEditing = true, AllowSorting = true };
            var rtrpnlqtycol = new GridTextColumn() { MappingName = "Rtrpnlqty", HeaderText = "롤수량", Width = 60, TextAlignment = TextAlignment.Center, AllowEditing = true, AllowSorting = true };
            var trackintimecol = new GridDateTimeColumn() { MappingName = "TrackinTime", HeaderText = "입고시간", Width = 80, CustomPattern = "MM/dd HH:mm", Pattern = Syncfusion.Windows.Shared.DateTimePattern.CustomPattern, TextAlignment = TextAlignment.Center, AllowSorting = true };
            var trackinusercol = new GridTextColumn() { MappingName = "TrackinUsername", HeaderText = "입고자", Width = 60, TextAlignment = TextAlignment.Center, AllowSorting = true };
            var lotnotescol = new GridTextColumn()
            {
                MappingName = "LotNotes",
                HeaderText = "특이사항",
                AllowEditing = true,
                TextAlignment = TextAlignment.Center,
                MaximumWidth = 450,
                MinimumWidth = 100,
                TextWrapping = TextWrapping.Wrap
            };
            
            var machinecscol = new GridTextColumn() { MappingName = "MachineCs", HeaderText = "CS호기", AllowEditing = true, Width = 80, TextAlignment = TextAlignment.Center };
            var machinesscol = new GridTextColumn() { MappingName = "MachineSs", HeaderText = "SS호기", AllowEditing = true, Width = 80, TextAlignment = TextAlignment.Center };
            var toolnotescol = new GridTextColumn() { MappingName = "ToolNotes", HeaderText = "작지전달사항",ShowToolTip = true, AllowEditing = false, Width = 450, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap };
            var totallayercol = new GridTextColumn() { MappingName = "Layer", HeaderText = "층수", Width = 40, TextAlignment = TextAlignment.Center };
            var prclayer1col = new GridTextColumn() { MappingName = "PrcLayer1", HeaderText = "가공층1", Width = 60, TextAlignment = TextAlignment.Center };
            var prclayer2col = new GridTextColumn() { MappingName = "PrcLayer2", HeaderText = "가공층2", Width = 60, TextAlignment = TextAlignment.Center };
            var holesizecol = new GridTextColumn() { MappingName = "MainHoleSize", HeaderText = "홀사이즈", Width = 80, TextAlignment = TextAlignment.Center };
            var holecountcol = new GridTextColumn() { MappingName = "HoleCount", HeaderText = "홀수", Width = 120, TextAlignment = TextAlignment.Center };
            var endcustomercol = new GridTextColumn() { MappingName = "EndCustomer", HeaderText = "납품처", Width = 160, TextAlignment = TextAlignment.Center };
            var idcol = new GridTextColumn() { MappingName = "Id", IsHidden = true };
            var waittrackoutcol = new GridCheckBoxColumn() { MappingName = "WaitTrackout", IsHidden = true };
            var custnamecol = new GridCheckBoxColumn() { MappingName = "CustName", IsHidden = true };
            var prdtypecol = new GridTextColumn() { MappingName = "ProductType", HeaderText = "제품타입", Width = 70, TextAlignment = TextAlignment.Center };
            var nextresourcecol = new GridTextColumn()
                {MappingName = "YpNextResourceDefault", HeaderText = "인계처", Width = 150, TextAlignment = TextAlignment.Center, ShowToolTip = true };
           

            GridWip.Columns.Add(trackoutcol);
            GridWip.Columns.Add(createtimecol);
            GridWip.Columns.Add(lotcol);
            //GridWip.Columns.Add(ypshortlotcol);
            GridWip.Columns.Add(modelnamecol);
            GridWip.Columns.Add(toolcol);
            GridWip.Columns.Add(revcol);
            //GridWip.Columns.Add(ypdatarevcol);
            
            GridWip.Columns.Add(mesprcnamecol);
            GridWip.Columns.Add(prcnamecol);
            GridWip.Columns.Add(pnlqtycol);
            GridWip.Columns.Add(rtrpnlqtycol); 
            GridWip.Columns.Add(trackintimecol);
            GridWip.Columns.Add(trackinusercol);
            GridWip.Columns.Add(lotnotescol);
            GridWip.Columns.Add(nextresourcecol);
            GridWip.Columns.Add(machinecscol);
            GridWip.Columns.Add(machinesscol);
            GridWip.Columns.Add(toolnotescol);
            GridWip.Columns.Add(prdtypecol);
            GridWip.Columns.Add(totallayercol);
            GridWip.Columns.Add(prclayer1col);
            GridWip.Columns.Add(prclayer2col);
            GridWip.Columns.Add(holesizecol);
            GridWip.Columns.Add(holecountcol);
            GridWip.Columns.Add(endcustomercol);
            GridWip.Columns.Add(idcol);
            GridWip.Columns.Add(waittrackoutcol);
            GridWip.Columns.Add(custnamecol);

            GridWip.Columns["CustName"].FilterPredicates.Clear();
            GridWip.Columns["CustName"].FilterPredicates.Add(new Syncfusion.Data.FilterPredicate() { FilterType = Syncfusion.Data.FilterType.Equals, FilterValue = "영풍전자" });

            GridWip.SortColumnDescriptions.Clear();
            GridWip.SortColumnDescriptions.Add(new SortColumnDescription() { ColumnName = "WaitTrackout", SortDirection = ListSortDirection.Descending });
            GridWip.SortColumnDescriptions.Add(new SortColumnDescription() { ColumnName = "Lotid", SortDirection = ListSortDirection.Ascending });

        }


        private async void YpNextResourceCombo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_mainwindowViewModel.SelectedGridWip != null)
            {
                var record = _mainwindowViewModel.SelectedGridWip;

                using (var db = new Db_Uv_InventoryContext())
                {
                    var result = db.TbUvWorkorder.SingleOrDefault(x => x.Id == record.Id);

                    result.YpNextResource = record.YpNextResource;
                    await db.SaveChangesAsync();
                }
            }
           
        }
    }
}
