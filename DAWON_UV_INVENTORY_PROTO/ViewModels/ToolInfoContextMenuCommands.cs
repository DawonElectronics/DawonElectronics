﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DAWON_UV_INVENTORY_PROTO.Models;
using DAWON_UV_INVENTORY_PROTO.Views;
using Syncfusion.UI.Xaml.Grid;

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

        
    }
}