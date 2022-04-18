using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using ConnectorYPE.Models;

namespace DAWON_UV_INVENTORY_PROTO.ViewModels
{
   
    public class GridTrackinYpeColorConverter : IValueConverter
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
            var data = value as WipModelAfterToolValidation;
            object result = new object();
            result = DependencyProperty.UnsetValue;
            if (data != null)
            {
                if (System.Convert.ToBoolean(data.IsRegist == false))
                { result = BrushFromHex("#FFD966"); }

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
