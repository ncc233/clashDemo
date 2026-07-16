using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Clash.UI.Suppot.UI.Validation
{
    public class ValidationParams:DependencyObject
    {


        public DateTime Param1
        {
            get { return (DateTime)GetValue(Param1Property); }
            set { SetValue(Param1Property, value); }
        }

        // Using a DependencyProperty as the backing store for Param1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Param1Property =
            DependencyProperty.Register(nameof(Param1), typeof(DateTime), typeof(ValidationParams), new PropertyMetadata(DateTime.Today));




        public DateTime Param2
        {
            get { return (DateTime)GetValue(Param2Property); }
            set { SetValue(Param2Property, value); }
        }

        // Using a DependencyProperty as the backing store for Param2.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Param2Property =
            DependencyProperty.Register(nameof(Param2), typeof(DateTime), typeof(ValidationParams), new PropertyMetadata(DateTime.Today.AddHours(23).AddMinutes(59)));


    }
}
