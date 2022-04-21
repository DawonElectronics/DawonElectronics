using DAWON_UV_INVENTORY_PROTO.Models;
using DAWON_UV_INVENTORY_PROTO.Views;
using Syncfusion.Data.Extensions;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace DAWON_UV_INVENTORY_PROTO.ViewModels
{
    public static class GridWipContextMenuCommands
    {
        #region 인수취소(받기취소처리-db처리)

        static BaseCommand _cancelTrackinRecordCommand;
        public static BaseCommand CancelTrackinRecordCommand
        {
            get
            {
                _cancelTrackinRecordCommand = new BaseCommand(CancelTrackinRecord);
                return _cancelTrackinRecordCommand;
            }
        }
        private static void CancelTrackinRecord(object obj)
        {
            if (obj is GridRecordContextMenuInfo)
            {

                var gridcontext = ((obj as GridRecordContextMenuInfo).DataGrid.DataContext as MainWindowViewModel);
                var record = (obj as GridRecordContextMenuInfo).Record as ViewUvWorkorder;
                var lot = record.Lotid;
                var cancelTrackinMsg = MessageBox.Show(" 장부에서 반납처리할까요?\n 예(반납으로 이력 남김)/ 아니오(장부 이력 삭제)", "입고취소", MessageBoxButton.YesNoCancel, MessageBoxImage.Information);

                if (record != null)
                {
                    using (var mw = new MainWindow())
                    {
                        if (cancelTrackinMsg == MessageBoxResult.Yes)
                        {
                            //장부이력 반납처리
                            using (var db = new Db_Uv_InventoryContext())
                            {
                                var result = db.TbUvWorkorder.SingleOrDefault(x => x.Id == record.Id);

                                if (result != null)
                                {
                                    var trackoutuser = db.TbUsers.Where(x => x.UserName == MainWindow._mainwindowViewModel.SelectedUser).FirstOrDefault();

                                    result.TrackoutTime = DateTime.Now;
                                    result.TrackoutUser = trackoutuser;
                                    result.IsDone = true;
                                    result.LotType = "반납";
                                    db.SaveChanges();
                                    MainWindow._mainwindowViewModel.WorkOrderList.Remove(record);

                                    mw.GetWipCount();

                                    MessageBox.Show("처리되었습니다");
                                }
                            }
                        }

                        else if (cancelTrackinMsg == MessageBoxResult.No)
                        {
                            //장부이력 삭제
                            using (var db = new Db_Uv_InventoryContext())
                            {
                                var result = db.TbUvWorkorder.SingleOrDefault(x => x.Id == record.Id);
                                if (result != null)
                                {
                                    db.Remove(result);
                                    db.SaveChanges();
                                    MainWindow._mainwindowViewModel.WorkOrderList.Remove(record);

                                    mw.GetWipCount();

                                    MessageBox.Show("처리되었습니다");
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 출하 대기 처리

        static BaseCommand _executeWaitTrackoutCommand;
        public static BaseCommand ExecuteWaitTrackoutCommand
        {
            get
            {
                _executeWaitTrackoutCommand = new BaseCommand(ExecuteWaitTrackout);
                return _executeWaitTrackoutCommand;
            }
        }
        private static async void ExecuteWaitTrackout(object obj)
        {
            if (obj is GridRecordContextMenuInfo)
            {
                var record = (obj as GridRecordContextMenuInfo).Record as ViewUvWorkorder;
                var lot = record.Lotid;
                var grid = (obj as GridRecordContextMenuInfo).DataGrid as SfDataGrid;
                if (record != null)
                {
                    //장부이력 반납처리
                    using (var db = new Db_Uv_InventoryContext())
                    {
                        var result = db.TbUvWorkorder.SingleOrDefault(x => x.Id == record.Id);

                        if (result != null)
                        {
                            MainWindow._mainwindowViewModel.SelectedGridWip.WaitTrackout = true;

                            Binding bindbg = new Binding();
                            bindbg.Converter = new GridWipColorConverter();
                            Binding bindfg = new Binding();
                            bindfg.Converter = new GridWipFGConverter();
                            Binding bindbold = new Binding();
                            bindbold.Converter = new GridWipBoldConverter();

                            var rowStyle = new Style { TargetType = typeof(VirtualizingCellsControl) };

                            rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.BackgroundProperty, bindbg));
                            rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.ForegroundProperty, bindfg));
                            rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.FontWeightProperty, bindbold));

                            (obj as GridRecordContextMenuInfo).DataGrid.RowStyle = rowStyle;
                            (obj as GridRecordContextMenuInfo).DataGrid.SortColumnDescriptions.Clear();
                            (obj as GridRecordContextMenuInfo).DataGrid.SortColumnDescriptions.Add(new SortColumnDescription { ColumnName = "WaitTrackout", SortDirection = ListSortDirection.Descending });
                            (obj as GridRecordContextMenuInfo).DataGrid.SortColumnDescriptions.Add(new SortColumnDescription { ColumnName = "TrackinTime", SortDirection = ListSortDirection.Ascending });
                            (obj as GridRecordContextMenuInfo).DataGrid.View.RefreshFilter();


                            result.WaitTrackout = true;
                            await db.SaveChangesAsync();

                        }
                    }
                }

            }
        }
        #endregion

        #region 출하 대기 취소 처리

        static BaseCommand _cancelWaitTrackoutCommand;
        public static BaseCommand CancelWaitTrackoutCommand
        {
            get
            {
                _cancelWaitTrackoutCommand = new BaseCommand(CancelWaitTrackout);
                return _cancelWaitTrackoutCommand;
            }
        }
        private static async void CancelWaitTrackout(object obj)
        {
            if (obj is GridRecordContextMenuInfo)
            {
                var record = (obj as GridRecordContextMenuInfo).Record as ViewUvWorkorder;
                var lot = record.Lotid;

                if (record != null)
                {
                    using (var db = new Db_Uv_InventoryContext())
                    {
                        var result = db.TbUvWorkorder.SingleOrDefault(x => x.Id == record.Id);

                        if (result != null)
                        {
                            MainWindow._mainwindowViewModel.SelectedGridWip.WaitTrackout = false;

                            Binding bindbg = new Binding();
                            bindbg.Converter = new GridWipColorConverter();
                            Binding bindfg = new Binding();
                            bindfg.Converter = new GridWipFGConverter();
                            Binding bindbold = new Binding();
                            bindbold.Converter = new GridWipBoldConverter();

                            var rowStyle = new Style { TargetType = typeof(VirtualizingCellsControl) };

                            rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.BackgroundProperty, bindbg));
                            rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.ForegroundProperty, bindfg));
                            rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.FontWeightProperty, bindbold));

                            (obj as GridRecordContextMenuInfo).DataGrid.RowStyle = rowStyle;

                            (obj as GridRecordContextMenuInfo).DataGrid.SortColumnDescriptions.Clear();
                            (obj as GridRecordContextMenuInfo).DataGrid.SortColumnDescriptions.Add(new SortColumnDescription { ColumnName = "WaitTrackout", SortDirection = ListSortDirection.Descending });
                            (obj as GridRecordContextMenuInfo).DataGrid.SortColumnDescriptions.Add(new SortColumnDescription { ColumnName = "TrackinTime", SortDirection = ListSortDirection.Ascending });
                            (obj as GridRecordContextMenuInfo).DataGrid.View.RefreshFilter();

                            result.WaitTrackout = false;
                            await db.SaveChangesAsync();

                        }
                    }
                }
            }
        }
        #endregion

        #region 설비 배정

        static BaseCommand _assignMachineCsCommand;
        public static BaseCommand AssignMachineCsCommand
        {
            get
            {
                _assignMachineCsCommand = new BaseCommand(AssignMachineCs);
                return _assignMachineCsCommand;
            }
        }
        private static void AssignMachineCs(object obj)
        {
            if (obj is GridRecordContextMenuInfo)
            {
                var record = (obj as GridRecordContextMenuInfo).Record as ViewUvWorkorder;

                if (!Application.Current.Windows.OfType<MachineChooserWindow>().Any())
                {
                    var mcWindow = new MachineChooserWindow(record);
                    mcWindow.Topmost = true;
                    mcWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                    mcWindow.Show();
                }
            }
        }
        #endregion

        #region CCL 클립보드 복사

        static BaseCommand _copyCCLInfoCommand;
        public static BaseCommand CopyCCLInfoCommand
        {
            get
            {
                _copyCCLInfoCommand = new BaseCommand(CopyCCLInfo);
                return _copyCCLInfoCommand;
            }
        }
        private static void CopyCCLInfo(object obj)
        {
            if (obj is GridRecordContextMenuInfo && MainWindow._mainwindowViewModel.SelectedCustomerWo.Contains("PKG"))
            {
                var record = (obj as GridRecordContextMenuInfo).Record as ViewUvWorkorder;
                var insulinfo = MainWindow._mainwindowViewModel.ToolInfos.Where(x => x.ProductId == record.ProductId).Select(x => x.InsulInfo).First().Split(',');


                var cclinfo = string.Format("{0} {1} {2}T {3}", insulinfo[3], insulinfo[5], Convert.ToDouble(insulinfo[10]), insulinfo[9]);
                try
                {
                    Clipboard.SetText(cclinfo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion

        #region 초도품 캠 완료처리

        static BaseCommand _executeCamFinishCommand;
        public static BaseCommand ExecuteCamFinishCommand
        {
            get
            {
                _executeCamFinishCommand = new BaseCommand(ExecuteCamFinish);
                return _executeCamFinishCommand;
            }
        }
        private static void ExecuteCamFinish(object obj)
        {
            if (obj is GridRecordContextMenuInfo)
            {
                var record = (obj as GridRecordContextMenuInfo).Record as ViewUvWorkorder;


                if (record != null)
                {
                    if (record.CustName.Contains("영풍"))
                    {
                        if (!Application.Current.Windows.OfType<CamFinishYpe>().Any() && record != null && record.CustName == "영풍전자")
                        {
                            var mcWindow = new CamFinishYpe(obj);
                            mcWindow.Topmost = true;
                            mcWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                            mcWindow.Show();
                        }
                    }
                    else
                    {
                        using (var db = new Db_Uv_InventoryContext())
                        {
                            var result = db.TbUvToolinfo.SingleOrDefault(x => x.ProductId == record.ProductId);

                            if (result != null)
                            {
                                MainWindow._mainwindowViewModel.SelectedGridWip.CamFinished = true;

                                Binding bindbg = new Binding();
                                bindbg.Converter = new GridWipColorConverter();
                                Binding bindfg = new Binding();
                                bindfg.Converter = new GridWipFGConverter();
                                Binding bindbold = new Binding();
                                bindbold.Converter = new GridWipBoldConverter();

                                var rowStyle = new Style { TargetType = typeof(VirtualizingCellsControl) };

                                rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.BackgroundProperty, bindbg));
                                rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.ForegroundProperty, bindfg));
                                rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.FontWeightProperty, bindbold));

                                (obj as GridRecordContextMenuInfo).DataGrid.RowStyle = rowStyle;

                                result.CamFinished = true;
                                db.SaveChanges();
                            }
                        }
                    }
                   
                }
            }
        }
        #endregion

        #region 행 글씨 굵게

        static BaseCommand _rowFontBoldCommand;
        public static BaseCommand RowFontBoldCommand
        {
            get
            {
                _rowFontBoldCommand = new BaseCommand(RowFontBold);
                return _rowFontBoldCommand;
            }
        }
        private static async void RowFontBold(object obj)
        {
            if (obj is GridRecordContextMenuInfo)
            {
                var record = (obj as GridRecordContextMenuInfo).Record as ViewUvWorkorder;
                var qryid = Convert.ToInt64(record.Id);


                if (record != null)
                {
                    using (var db = new Db_Uv_InventoryContext())
                    {
                        var result = db.TbUvWorkorder.SingleOrDefault(x => x.Id == qryid);

                        if (result != null)
                        {
                            if (result.FormatBold == true)
                            {
                                result.FormatBold = false;
                                MainWindow._mainwindowViewModel.SelectedGridWip.FormatBold = false;
                            }
                            else if (result.FormatBold == false || result.FormatBold == null)
                            {
                                result.FormatBold = true;
                                MainWindow._mainwindowViewModel.SelectedGridWip.FormatBold = true;
                            }

                            Binding bindbg = new Binding();
                            bindbg.Converter = new GridWipColorConverter();
                            Binding bindfg = new Binding();
                            bindfg.Converter = new GridWipFGConverter();
                            Binding bindbold = new Binding();
                            bindbold.Converter = new GridWipBoldConverter();

                            var rowStyle = new Style { TargetType = typeof(VirtualizingCellsControl) };

                            rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.BackgroundProperty, bindbg));
                            rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.ForegroundProperty, bindfg));
                            rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.FontWeightProperty, bindbold));

                            (obj as GridRecordContextMenuInfo).DataGrid.RowStyle = rowStyle;

                            await db.SaveChangesAsync();

                        }
                    }
                }
            }
        }

        #endregion

        #region 행 색상 변경

        static BaseCommand _rowColor1Command;
        public static BaseCommand RowColor1Command
        {
            get
            {
                _rowColor1Command = new BaseCommand(RowColor1);
                return _rowColor1Command;
            }
        }
        private static async void RowColor1(object obj)
        {
            if (obj is GridRecordContextMenuInfo)
            {
                var record = (obj as GridRecordContextMenuInfo).Record as ViewUvWorkorder;
                var qryid = Convert.ToInt64(record.Id);


                if (record != null)
                {
                    using (var db = new Db_Uv_InventoryContext())
                    {
                        var result = db.TbUvWorkorder.SingleOrDefault(x => x.Id == qryid);

                        if (result != null)
                        {
                            MainWindow._mainwindowViewModel.SelectedGridWip.FormatBg = Colors.Red.ToString();
                            MainWindow._mainwindowViewModel.SelectedGridWip.FormatFg = Colors.White.ToString();

                            Binding bindbg = new Binding();
                            bindbg.Converter = new GridWipColorConverter();
                            Binding bindfg = new Binding();
                            bindfg.Converter = new GridWipFGConverter();
                            Binding bindbold = new Binding();
                            bindbold.Converter = new GridWipBoldConverter();

                            var rowStyle = new Style { TargetType = typeof(VirtualizingCellsControl) };

                            rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.BackgroundProperty, bindbg));
                            rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.ForegroundProperty, bindfg));
                            rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.FontWeightProperty, bindbold));

                            (obj as GridRecordContextMenuInfo).DataGrid.RowStyle = rowStyle;


                            result.FormatBg = Colors.Red.ToString();
                            result.FormatFg = Colors.White.ToString();

                            await db.SaveChangesAsync();

                        }
                    }
                }
            }
        }

        static BaseCommand _rowColor2Command;
        public static BaseCommand RowColor2Command
        {
            get
            {
                _rowColor2Command = new BaseCommand(RowColor2);
                return _rowColor2Command;
            }
        }
        private static async void RowColor2(object obj)
        {
            if (obj is GridRecordContextMenuInfo)
            {
                var record = (obj as GridRecordContextMenuInfo).Record as ViewUvWorkorder;
                var qryid = Convert.ToInt64(record.Id);


                if (record != null)
                {
                    
                    using (var db = new Db_Uv_InventoryContext())
                    {
                        var result = db.TbUvWorkorder.SingleOrDefault(x => x.Id == qryid);

                        if (result != null)
                        {
                            MainWindow._mainwindowViewModel.SelectedGridWip.FormatBg = Colors.White.ToString();
                            MainWindow._mainwindowViewModel.SelectedGridWip.FormatFg = Colors.Black.ToString();

                            Binding bindbg = new Binding();
                            bindbg.Converter = new GridWipColorConverter();
                            Binding bindfg = new Binding();
                            bindfg.Converter = new GridWipFGConverter();
                            Binding bindbold = new Binding();
                            bindbold.Converter = new GridWipBoldConverter();

                            var rowStyle = new Style { TargetType = typeof(VirtualizingCellsControl) };

                            rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.BackgroundProperty, bindbg));
                            rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.ForegroundProperty, bindfg));
                            rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.FontWeightProperty, bindbold));

                            (obj as GridRecordContextMenuInfo).DataGrid.RowStyle = rowStyle;


                            result.FormatBg = Colors.White.ToString();
                            result.FormatFg = Colors.Black.ToString();

                            await db.SaveChangesAsync();

                        }
                    }
                }
            }
        }


        static BaseCommand _rowColor3Command;
        public static BaseCommand RowColor3Command
        {
            get
            {
                _rowColor3Command = new BaseCommand(RowColor3);
                return _rowColor3Command;
            }
        }
        private static async void RowColor3(object obj)
        {
            if (obj is GridRecordContextMenuInfo)
            {
                var record = (obj as GridRecordContextMenuInfo).Record as ViewUvWorkorder;
                var qryid = Convert.ToInt64(record.Id);


                if (record != null)
                {
                    
                    using (var db = new Db_Uv_InventoryContext())
                    {
                        var result = db.TbUvWorkorder.SingleOrDefault(x => x.Id == qryid);

                        if (result != null)
                        {
                            MainWindow._mainwindowViewModel.SelectedGridWip.FormatBg = Colors.Yellow.ToString();
                            MainWindow._mainwindowViewModel.SelectedGridWip.FormatFg = Colors.Black.ToString();

                            Binding bindbg = new Binding();
                            bindbg.Converter = new GridWipColorConverter();
                            Binding bindfg = new Binding();
                            bindfg.Converter = new GridWipFGConverter();
                            Binding bindbold = new Binding();
                            bindbold.Converter = new GridWipBoldConverter();

                            var rowStyle = new Style { TargetType = typeof(VirtualizingCellsControl) };

                            rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.BackgroundProperty, bindbg));
                            rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.ForegroundProperty, bindfg));
                            rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.FontWeightProperty, bindbold));

                            (obj as GridRecordContextMenuInfo).DataGrid.RowStyle = rowStyle;


                            result.FormatBg = Colors.Yellow.ToString();
                            result.FormatFg = Colors.Black.ToString();

                            await db.SaveChangesAsync();

                        }
                    }
                }
            }
        }


        static BaseCommand _rowColor4Command;
        public static BaseCommand RowColor4Command
        {
            get
            {
                _rowColor4Command = new BaseCommand(RowColor4);
                return _rowColor4Command;
            }
        }
        private static async void RowColor4(object obj)
        {
            if (obj is GridRecordContextMenuInfo)
            {
                var record = (obj as GridRecordContextMenuInfo).Record as ViewUvWorkorder;
                var qryid = Convert.ToInt64(record.Id);


                if (record != null)
                {
                    
                    using (var db = new Db_Uv_InventoryContext())
                    {
                        var result = db.TbUvWorkorder.SingleOrDefault(x => x.Id == qryid);

                        if (result != null)
                        {
                            MainWindow._mainwindowViewModel.SelectedGridWip.FormatBg = Colors.Green.ToString();
                            MainWindow._mainwindowViewModel.SelectedGridWip.FormatFg = Colors.White.ToString();

                            Binding bindbg = new Binding();
                            bindbg.Converter = new GridWipColorConverter();
                            Binding bindfg = new Binding();
                            bindfg.Converter = new GridWipFGConverter();
                            Binding bindbold = new Binding();
                            bindbold.Converter = new GridWipBoldConverter();

                            var rowStyle = new Style { TargetType = typeof(VirtualizingCellsControl) };

                            rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.BackgroundProperty, bindbg));
                            rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.ForegroundProperty, bindfg));
                            rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.FontWeightProperty, bindbold));

                            (obj as GridRecordContextMenuInfo).DataGrid.RowStyle = rowStyle;


                            result.FormatBg = Colors.Green.ToString();
                            result.FormatFg = Colors.White.ToString();

                            await db.SaveChangesAsync();

                        }
                    }
                }
            }
        }


        static BaseCommand _rowColor5Command;
        public static BaseCommand RowColor5Command
        {
            get
            {
                _rowColor5Command = new BaseCommand(RowColor5);
                return _rowColor5Command;
            }
        }
        private static async void RowColor5(object obj)
        {
            if (obj is GridRecordContextMenuInfo)
            {
                var record = (obj as GridRecordContextMenuInfo).Record as ViewUvWorkorder;
                var qryid = Convert.ToInt64(record.Id);


                if (record != null)
                {
                   
                    using (var db = new Db_Uv_InventoryContext())
                    {
                        var result = db.TbUvWorkorder.SingleOrDefault(x => x.Id == qryid);

                        if (result != null)
                        {
                            MainWindow._mainwindowViewModel.SelectedGridWip.FormatBg = Colors.White.ToString();
                            MainWindow._mainwindowViewModel.SelectedGridWip.FormatFg = Colors.Black.ToString();



                            Binding bindbg = new Binding();
                            bindbg.Converter = new GridWipColorConverter();
                            Binding bindfg = new Binding();
                            bindfg.Converter = new GridWipFGConverter();
                            Binding bindbold = new Binding();
                            bindbold.Converter = new GridWipBoldConverter();

                            var rowStyle = new Style { TargetType = typeof(VirtualizingCellsControl) };

                            rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.BackgroundProperty, bindbg));
                            rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.ForegroundProperty, bindfg));
                            rowStyle.Setters.Add(new Setter(VirtualizingCellsControl.FontWeightProperty, bindbold));

                            (obj as GridRecordContextMenuInfo).DataGrid.RowStyle = rowStyle;


                            result.FormatBg = Colors.White.ToString();
                            result.FormatFg = Colors.Black.ToString();

                            await db.SaveChangesAsync();

                        }
                    }
                }
            }
        }




        #endregion

        #region 입고 작업자 수정

        static BaseCommand _editUserCommand;
        public static BaseCommand EditUserCommand
        {
            get
            {
                _editUserCommand = new BaseCommand(EditUser);
                return _editUserCommand;
            }
        }
        private static void EditUser(object obj)
        {
            if (obj is GridRecordContextMenuInfo)
            {
                var record = (obj as GridRecordContextMenuInfo).Record as ViewUvWorkorder;

                if (!Application.Current.Windows.OfType<EditUserWindowWip>().Any() && record != null)
                {
                    var mcWindow = new EditUserWindowWip(obj);
                    mcWindow.Topmost = true;
                    mcWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                    mcWindow.Show();
                }
            }
        }
        #endregion

        #region 영풍 인계처 관리

        static BaseCommand _ypeNextResourceManageCommand;
        public static BaseCommand YpeNextResourceManageCommand
        {
            get
            {
                _ypeNextResourceManageCommand = new BaseCommand(YpeNextResourceManage);
                return _ypeNextResourceManageCommand;
            }
        }
        private static void YpeNextResourceManage(object obj)
        {
            if (obj is GridRecordContextMenuInfo)
            {
                var record = (obj as GridRecordContextMenuInfo).Record as ViewUvWorkorder;

                if (!Application.Current.Windows.OfType<YpeNextResourceManageWindow>().Any() && record.CustName.Contains("영풍"))
                {
                    var mcWindow = new YpeNextResourceManageWindow();
                    mcWindow.Topmost = true;
                    mcWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                    mcWindow.Show();
                }
            }
        }
        #endregion

        #region RTR 로스/추가 입력

        static BaseCommand _rtrLossInputWindowCommand;
        public static BaseCommand RtrLossInputWindowCommand
        {
            get
            {
                _rtrLossInputWindowCommand = new BaseCommand(RtrLossInputWindow);
                return _rtrLossInputWindowCommand;
            }
        }
        private static void RtrLossInputWindow(object obj)
        {
            if (obj is GridRecordContextMenuInfo )
            {
                var record = (obj as GridRecordContextMenuInfo).Record as ViewUvWorkorder;

                if (!Application.Current.Windows.OfType<RtrLossInputWindow>().Any() && record.PrcName.Contains("RTR"))
                {
                    var mcWindow = new RtrLossInputWindow(record);
                    mcWindow.Topmost = true;
                    mcWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                    mcWindow.Show();
                }
            }
        }
        #endregion

    }

}

