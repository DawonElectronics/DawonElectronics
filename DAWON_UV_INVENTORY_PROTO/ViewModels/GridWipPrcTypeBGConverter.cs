using DAWON_UV_INVENTORY_PROTO.Models;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace DAWON_UV_INVENTORY_PROTO.ViewModels
{

    public class GridWipPrcTypeBGConverter : IValueConverter
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
            result = DependencyProperty.UnsetValue;
            if (data != null)
            {
                if (data.PrcName == "BASE")
                {
                    result = new SolidColorBrush(Colors.SkyBlue);
                }
                else
                {
                    result = DependencyProperty.UnsetValue;
                }
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
