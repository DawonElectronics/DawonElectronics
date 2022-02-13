using System;
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
    public class GridWipColorConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = value as ViewUvWorkorder;
            object result = new object();
            if (data != null)
            {
                if (data.WaitTrackout == true)
                    result = new SolidColorBrush(Colors.YellowGreen);
                else
                    result = DependencyProperty.UnsetValue;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
