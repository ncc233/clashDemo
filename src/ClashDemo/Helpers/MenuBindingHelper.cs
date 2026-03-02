using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClashDemo.Helpers
{
    internal class MenuBindingHelper : Freezable
    {


        protected override Freezable CreateInstanceCore()
        {
            return new MenuBindingHelper();
        }
        public object Data
        {
            get { return GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(MenuBindingHelper), new UIPropertyMetadata(null));
    }
}
