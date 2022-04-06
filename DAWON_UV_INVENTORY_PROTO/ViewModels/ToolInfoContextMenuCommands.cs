using DAWON_UV_INVENTORY_PROTO.Models;
using DAWON_UV_INVENTORY_PROTO.Views;
using Syncfusion.UI.Xaml.Grid;
using System.Linq;
using System.Windows;

namespace DAWON_UV_INVENTORY_PROTO.ViewModels
{
    public static class ToolInfoContextMenuCommands
    {
        #region 모델정보 수정 컨텍스트 메뉴

        static BaseCommand _editToolinfoCommand;
        public static BaseCommand EditToolinfoCommand
        {
            get
            {
                _editToolinfoCommand = new BaseCommand(EditToolinfo);
                return _editToolinfoCommand;
            }
        }

        private static void EditToolinfo(object obj)
        {
            if (obj is GridRecordContextMenuInfo)
            {
                var record = (obj as GridRecordContextMenuInfo).Record as TbUvToolinfo;

                if (!Application.Current.Windows.OfType<ToolInfoEditorWindow>().Any())
                {
                    var mcWindow = new ToolInfoEditorWindow(record);
                    mcWindow.Topmost = true;
                    mcWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                    mcWindow.Show();
                }
            }
        }
        #endregion

        #region 모델 정보 새로고침 컨텍스트 메뉴

        static BaseCommand _refreshToolinfoCommand;
        public static BaseCommand RefreshToolinfoCommand
        {
            get
            {
                _refreshToolinfoCommand = new BaseCommand(RefreshToolinfo);
                return _refreshToolinfoCommand;
            }
        }

        private static void RefreshToolinfo(object obj)
        {
            using (var mw = new MainWindow())
            { mw.UpdateToolInfoList(); }
        }
        #endregion


    }
}
