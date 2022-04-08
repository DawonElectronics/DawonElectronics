using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace DAWON_UV_INVENTORY_PROTO.ViewModels
{
    public class YpNextResourceConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = value as string;
            var result = new ObservableCollection<object>();

            if (data != null)
            {
                
                foreach (var item in data.Split(',').ToList())
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            throw new NotImplementedException();
        }
    }
}
