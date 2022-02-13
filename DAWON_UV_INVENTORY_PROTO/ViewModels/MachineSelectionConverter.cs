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
    public class MachineSelectionConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string data=string.Empty;
            if ((string)parameter == "MachineCs")
            { data = ((ViewUvWorkorder)value).MachineCs; }
            else if ((string)parameter == "MachineSs")
            { data = ((ViewUvWorkorder)value).MachineCs; }


            //custom condition is checked based on data.
            if (data != null )
                return data.Split(',').ToList();
            else
                return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = value as List<string>;
            
            string tempdata = string.Empty;
            if ((string)parameter == "MachineCs")
            { tempdata = ((ViewUvWorkorder)value).MachineCs; }
            else if ((string)parameter == "MachineSs")
            { tempdata = ((ViewUvWorkorder)value).MachineCs; }

            if (data != null)
               
                return String.Join(", ", data.ToArray());
            else
                return DependencyProperty.UnsetValue;
        }
    }
}
