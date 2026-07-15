using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Clash.UI.Suppot.UI.Converters
{
    public class SelectedDatesConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType.Name == "CornerRadius")
            {
                if (values.Length < 3) return new CornerRadius(0);
                if (values[0].Equals(values[1])) return new CornerRadius(13, 0, 0, 13);
                else if (values[0].Equals(values[2])) return new CornerRadius(0, 13, 13, 0);
                else return new CornerRadius(0);
            }
            else
            {
                if (values.Length < 3) return Visibility.Collapsed;
                if ((values[0].Equals(values[1]) || values[0].Equals(values[2])) && System.Convert.ToBoolean(values[3]) == false) return Visibility.Visible;
                else return Visibility.Collapsed;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
