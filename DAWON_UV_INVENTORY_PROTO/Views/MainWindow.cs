using Syncfusion.UI.Xaml.Grid;
using Syncfusion.Windows.Controls.Input;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DAWON_UV_INVENTORY_PROTO
{
    public partial class MainWindow
    {
        //업체별 컬럼 정의
        private void GridWipColumnDems()
        {
            var trackoutcol = new GridTemplateColumn() { MappingName = "ExecuteTrackout", HeaderText = "출고", Width = 70 };
            trackoutcol.CellTemplate = this.FindResource("trackoutTemplate") as DataTemplate;

            var createtimecol = new GridDateTimeColumn() { MappingName = "CreateTime", HeaderText = "생성일", Width = 80, CustomPattern = "yyyy/MM/dd hh:mm", Pattern = Syncfusion.Windows.Shared.DateTimePattern.ShortDate, TextAlignment = TextAlignment.Center };
            var lotcol = new GridTextColumn() { MappingName = "Lotid", HeaderText = "LOT", Width = 120, TextAlignment = TextAlignment.Center, AllowFiltering = false };
            var modelnamecol = new GridTextColumn() { MappingName = "CustModelname", HeaderText = "모델명", AllowFiltering = true, Width = 170, TextAlignment = TextAlignment.Center };
            var revcol = new GridTextColumn() { MappingName = "CustRevision", HeaderText = "REV", Width = 60, TextAlignment = TextAlignment.Center };
            var toolcol = new GridTextColumn() { MappingName = "CustToolno", HeaderText = "TOOL", AllowFiltering = true, Width = 110, TextAlignment = TextAlignment.Center };
            var mesprcnamecol = new GridTextColumn() { MappingName = "MesPrcName", HeaderText = "공정명", AllowFiltering = true, Width = 80, TextAlignment = TextAlignment.Center };
            var prcnamecol = new GridTextColumn() { MappingName = "PrcName", HeaderText = "공법", Width = 80, TextAlignment = TextAlignment.Center };
            var pnlqtycol = new GridTextColumn() { MappingName = "Pnlqty", HeaderText = "수량", Width = 50, TextAlignment = TextAlignment.Center, AllowEditing = true };
            var trackintimecol = new GridDateTimeColumn() { MappingName = "TrackinTime", HeaderText = "입고시간", Width = 80, CustomPattern = "MM/dd HH:mm", Pattern = Syncfusion.Windows.Shared.DateTimePattern.CustomPattern, TextAlignment = TextAlignment.Center };
            var trackinusercol = new GridTextColumn() { MappingName = "TrackinUsername", HeaderText = "입고자", Width = 60, TextAlignment = TextAlignment.Center };
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
            //var machinecscol = new GridTemplateColumn() { MappingName = "MachineCs", HeaderText = "CS호기", Width = 100, AllowEditing = true, TextAlignment = TextAlignment.Center };
            //machinecscol.CellTemplate = this.FindResource("CsComboTemplate") as DataTemplate;


            //var machinesscol = new GridTemplateColumn() { MappingName = "MachineSs", HeaderText = "SS호기", Width = 100, AllowEditing=true, TextAlignment = TextAlignment.Center };
            //machinecscol.CellTemplate = this.FindResource("SsComboTemplate") as DataTemplate;


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
        }
        private GridTemplateColumn MachineColumn(string mapping,string header)
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
        private void GridWipColumnDepkg()
        {
            var trackoutcol = new GridTemplateColumn() { MappingName = "ExecuteTrackout", HeaderText = "출고", Width = 70 };
            trackoutcol.CellTemplate = this.FindResource("trackoutTemplate") as DataTemplate;

            var createtimecol = new GridDateTimeColumn() { MappingName = "CreateTime", HeaderText = "생성일", Width = 80, CustomPattern = "yyyy/MM/dd hh:mm", Pattern = Syncfusion.Windows.Shared.DateTimePattern.ShortDate, TextAlignment = TextAlignment.Center };
            var lotcol = new GridTextColumn() { MappingName = "Lotid", HeaderText = "LOT", Width = 120, TextAlignment = TextAlignment.Center, AllowFiltering = false };
            var modelnamecol = new GridTextColumn() { MappingName = "CustModelname", HeaderText = "모델명", AllowFiltering = true, Width = 170, TextAlignment = TextAlignment.Center };
            var revcol = new GridTextColumn() { MappingName = "CustRevision", HeaderText = "REV", Width = 60, TextAlignment = TextAlignment.Center };
            var toolcol = new GridTextColumn() { MappingName = "CustToolno", HeaderText = "TOOL", AllowFiltering = true, Width = 110, TextAlignment = TextAlignment.Center };
            var mesprcnamecol = new GridTextColumn() { MappingName = "MesPrcName", HeaderText = "공정명", AllowFiltering = true, Width = 80, TextAlignment = TextAlignment.Center };
            var prcnamecol = new GridTextColumn() { MappingName = "PrcName", HeaderText = "공법", Width = 80, TextAlignment = TextAlignment.Center };
            var pnlqtycol = new GridTextColumn() { MappingName = "Pnlqty", HeaderText = "수량", Width = 50, TextAlignment = TextAlignment.Center, AllowEditing = true };
            var trackintimecol = new GridDateTimeColumn() { MappingName = "TrackinTime", HeaderText = "입고시간", Width = 80, CustomPattern = "MM/dd HH:mm", Pattern = Syncfusion.Windows.Shared.DateTimePattern.CustomPattern, TextAlignment = TextAlignment.Center };
            var trackinusercol = new GridTextColumn() { MappingName = "TrackinUsername", HeaderText = "입고자", Width = 60, TextAlignment = TextAlignment.Center };
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

            var aaa = new SfTextBoxExt();
            

        }
    }
}
