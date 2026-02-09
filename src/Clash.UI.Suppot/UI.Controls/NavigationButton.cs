using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Clash.UI.Suppot.UI.Controls
{
    public class NavigationButton : ListBoxItem
    {



        public Geometry HeaderIcon
        {
            get { return (Geometry)GetValue(HeaderIconProperty); }
            set { SetValue(HeaderIconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderIcon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderIconProperty =
            DependencyProperty.Register(nameof(HeaderIcon), typeof(Geometry), typeof(NavigationButton), new PropertyMetadata(null));



    }
}
