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
    public class CustomRadioButton:RadioButton
    {


        public bool IsRun
        {
            get { return (bool)GetValue(IsRunProperty); }
            set { SetValue(IsRunProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsRun.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsRunProperty =
            DependencyProperty.Register(nameof(IsRun), typeof(bool), typeof(CustomRadioButton), new PropertyMetadata(false));



        public string DetailMessage
        {
            get { return (string)GetValue(DetailMessageProperty); }
            set { SetValue(DetailMessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DetailMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DetailMessageProperty =
            DependencyProperty.Register(nameof(DetailMessage), typeof(string), typeof(CustomRadioButton), new PropertyMetadata(""));





        public Geometry HeaderIcon
        {
            get { return (Geometry)GetValue(HeaderIconProperty); }
            set { SetValue(HeaderIconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderIcon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderIconProperty =
            DependencyProperty.Register(nameof(HeaderIcon), typeof(Geometry), typeof(CustomRadioButton), new PropertyMetadata(null));



    }
}
