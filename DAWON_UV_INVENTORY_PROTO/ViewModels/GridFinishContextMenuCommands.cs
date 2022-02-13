using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DAWON_UV_INVENTORY_PROTO.Models;
using Syncfusion.UI.Xaml.Grid;

namespace DAWON_UV_INVENTORY_PROTO.ViewModels
{
    public static class GridFinishContextMenuCommands
    {
        #region 재공 이력 컨텍스트 메뉴(출고취소처리-db만처리)

        static BaseCommand _cancelTrackoutRecordCommand;
        public static BaseCommand CancelTrackoutRecordCommand
        {
            get
            {
                _cancelTrackoutRecordCommand = new BaseCommand(CancelTrackoutRecord);
                return _cancelTrackoutRecordCommand;
            }
        }

        private static void CancelTrackoutRecord(object obj)
        {

            

            if (obj is GridRecordContextMenuInfo)
            {
                var mw = new MainWindow();
                var record = (obj as GridRecordContextMenuInfo).Record as ViewUvWorkorderDone;
                var lot = record.Lotid;
                var cancelTrackinMsg = MessageBox.Show(" 출고취소할까요?", "출고취소", MessageBoxButton.YesNo,
                    MessageBoxImage.Information);

                if (record != null)
                {
                    if (cancelTrackinMsg == MessageBoxResult.Yes)
                    {
                        using (var db = new Db_Uv_InventoryContext())
                        {
                            var result = db.TbUvWorkorder.SingleOrDefault(x => x.Id == record.Id);

                            if (result != null)
                            {
                                result.TrackoutTime = null;
                                result.TrackoutUser = null;
                                result.IsDone = false;

                                db.SaveChanges();

                                mw.UpdateFiltered_WorkorderSearchList();
                                mw.UpdateFiltered_WorkorderList();

                                MessageBox.Show("처리되었습니다");
                            }
                        }
                    }

                    else if (cancelTrackinMsg == MessageBoxResult.No)
                    {
                        return;
                    }
                }
            }
        }
        #endregion

        
    }
}
