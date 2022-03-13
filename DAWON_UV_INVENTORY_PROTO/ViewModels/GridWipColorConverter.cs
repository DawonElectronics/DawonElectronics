using DAWON_UV_INVENTORY_PROTO.Models;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace DAWON_UV_INVENTORY_PROTO.ViewModels
{

    public class GridWipColorConverter : IValueConverter
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
                if (data.CamFinished == false)
                { result = BrushFromHex("#FFD966"); }
                else if (data.WaitTrackout == true)
                { result = new SolidColorBrush(Colors.YellowGreen); }
                else if (data.CamFinished == false && data.FormatBg != null)
                { result = BrushFromHex("#FFD966"); }
                else if (data.WaitTrackout == false && data.FormatBg != null)
                {
                    if (data.FormatBg.Length > 6)
                    {
                        result = data.FormatBg;
                        Debug.WriteLine("배경컨버팅");
                    }
                }
                else
                { result = DependencyProperty.UnsetValue; }
            }


            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
