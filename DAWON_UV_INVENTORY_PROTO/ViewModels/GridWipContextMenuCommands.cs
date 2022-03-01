using DAWON_UV_INVENTORY_PROTO.Models;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Linq;
using System.Windows;
using DAWON_UV_INVENTORY_PROTO.Views;

namespace DAWON_UV_INVENTORY_PROTO.ViewModels
{
    public static class GridWipContextMenuCommands
    {
        #region 재공 이력 컨텍스트 메뉴(받기취소처리-db처리)

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

                var mw = new MainWindow();
                var record = (obj as GridRecordContextMenuInfo).Record as ViewUvWorkorder;
                var lot = record.Lotid;
                var cancelTrackinMsg = MessageBox.Show(" 장부에서 반납처리할까요?\n 예(반납으로 이력 남김)/ 아니오(장부 이력 삭제)", "입고취소", MessageBoxButton.YesNoCancel, MessageBoxImage.Information);

                if (record != null)
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

                                mw.UpdateFiltered_WorkorderList();
                                mw.UpdateFiltered_WorkorderSearchList();
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
                                mw.UpdateFiltered_WorkorderList();

                                MessageBox.Show("처리되었습니다");
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
        private static void ExecuteWaitTrackout(object obj)
        {
            if (obj is GridRecordContextMenuInfo)
            {
                var record = (obj as GridRecordContextMenuInfo).Record as ViewUvWorkorder;
                var lot = record.Lotid;
                var mw = new MainWindow();

                if (record != null)
                {
                    //장부이력 반납처리
                    using (var db = new Db_Uv_InventoryContext())
                    {
                        var result = db.TbUvWorkorder.SingleOrDefault(x => x.Id == record.Id);

                        if (result != null)
                        {
                            result.WaitTrackout = true;
                            db.SaveChanges();
                            mw.UpdateFiltered_WorkorderList();
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
        private static void CancelWaitTrackout(object obj)
        {
            if (obj is GridRecordContextMenuInfo)
            {
                var record = (obj as GridRecordContextMenuInfo).Record as ViewUvWorkorder;
                var lot = record.Lotid;
                var mw = new MainWindow();
                if (record != null)
                {
                    using (var db = new Db_Uv_InventoryContext())
                    {
                        var result = db.TbUvWorkorder.SingleOrDefault(x => x.Id == record.Id);
                        if (result != null)
                        {
                            result.WaitTrackout = false;
                            db.SaveChanges();
                            mw.UpdateFiltered_WorkorderList();
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
                var insulinfo = MainWindow._mainwindowViewModel.ToolInfos.Where(x=>x.ProductId == record.ProductId).Select(x=>x.InsulInfo).First().Split(',');
                
                
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
                var lot = record.Lotid;
                var mw = new MainWindow();

                if (record != null)
                {
                    //장부이력 반납처리
                    using (var db = new Db_Uv_InventoryContext())
                    {
                        var result = db.TbUvToolinfo.SingleOrDefault(x => x.ProductId == record.ProductId);

                        if (result != null)
                        {
                            result.CamFinished = true;
                            db.SaveChanges();
                            mw.UpdateFiltered_WorkorderList();
                        }
                    }
                }
            }
        }
        #endregion
    }

}

