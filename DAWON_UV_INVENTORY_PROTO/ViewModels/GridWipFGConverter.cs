﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using DAWON_UV_INVENTORY_PROTO.Models;

namespace DAWON_UV_INVENTORY_PROTO.ViewModels
{
    
    public class GridWipFGConverter : IValueConverter
    {
        public SolidColorBrush ToBrush(string HexColorString)
        {
            return (SolidColorBrush)(new BrushConverter().ConvertFrom(HexColorString));
        }
        public SolidColorBrush BrushFromHex(string hexColorString)
        {
            return (SolidColorBrush)(new BrushConverter().ConvertFrom(hexColorString));
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = value as ViewUvWorkorder;
            object result = new object();
            if (data.FormatFg != null)
            {
                if (data.FormatFg.Length>6) result = (SolidColorBrush)(new BrushConverter().ConvertFromString(data.FormatFg));

                else
                    result = DependencyProperty.UnsetValue;

            }
            else
                result = DependencyProperty.UnsetValue;

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
