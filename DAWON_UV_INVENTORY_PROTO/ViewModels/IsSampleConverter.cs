using System;
using System.Globalization;
using System.Windows.Data;

namespace DAWON_UV_INVENTORY_PROTO.ViewModels
{
    public class IsSampleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = (string)value;
            if (str == "샘플")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool check = (bool)value;
            if (check == true)
            {
                return "샘플";
            }
            else
            {
                return "양산";
            }
        }
    }
}
