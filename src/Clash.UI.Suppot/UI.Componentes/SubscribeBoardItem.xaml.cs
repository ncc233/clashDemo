using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Clash.UI.Suppot.UI.Componentes
{
    /// <summary>
    /// SubscribeBoardItem.xaml 的交互逻辑
    /// </summary>
    public partial class SubscribeBoardItem : UserControl
    {
        public SubscribeBoardItem()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button ben) 
            {
                ben.IsEnabled = false;
                await Task.Delay(1000 * 2);
                ben.IsEnabled = true;
            }
        }
    }
}
