using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Clash.UI.Suppot.UI.Controls
{
    public class HeaderComboBox:ComboBox
    {


        public object Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register(nameof(Header), typeof(object), typeof(HeaderComboBox), new PropertyMetadata(null));

        public HeaderComboBox()
        {

            Loaded += HeaderComboBox_Loaded;
        }

        private void HeaderComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var control=sender as HeaderComboBox;
            var border=control.Template.FindName("dropDownBorder", control) as Border;
            var hei=border.Height;
            var wid=border.Width;
            var radius=border.CornerRadius;
            var rect=new RectangleGeometry(new Rect(0, 0, wid, hei), radius.TopLeft, radius.TopLeft);
            border.Clip = rect;

        }
    }
}
