using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Clash.UI.Suppot.UI.Controls
{
    public class ScrollButton:Button
    {
        public ScrollButton() 
        {
            this.Loaded += ScrollButton_Loaded;
        }

        private void ScrollButton_Loaded(object sender, RoutedEventArgs e)
        {

            var str=this.Content.ToString();
            this.Content = str[0];
        }
    }
}
