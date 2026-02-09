
using Autofac;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace ClashDemo.Converters
{
    public class PageNaviagtionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string pageName = string.Empty;
            if (value is ListBoxItem item)
                pageName = item.Content.ToString().Replace(" ","");
            Page page = null;
            if (string.IsNullOrEmpty(pageName))
            {
                page = new Page();
                page.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ececec"));
                return page;
            }
            page = App.Current.Container.Resolve<IEnumerable<Page>>().FirstOrDefault(x =>
            {

               return x.Name.Contains(pageName);
            });
            if (page == null)
            {
                page = new Page();
                page.Background = Brushes.Red;
            }
            return page;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
