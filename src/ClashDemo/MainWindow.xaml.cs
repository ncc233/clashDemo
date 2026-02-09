using Clash.UI.Suppot.UI.Controls;
using ClashDemo.ViewModels;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClashDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var vm = new MainWindowViewModel();
            this.DataContext = vm;
            navigationBar.SelectedIndex = 0;
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            var page=e.Content as Page;
            if (page != null) 
            {
                var datas=navigationBar.ItemsSource.Cast<NavigationButton>();
                var item=datas.First(x=>x.Content.ToString().Replace(" ","")==page.Name);
                item.IsSelected = true;
            }
        }
    }
}