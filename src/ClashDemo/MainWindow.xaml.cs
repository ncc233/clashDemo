using Clash.UI.Suppot.UI.Controls;
using Clash.UI.Suppot.UI.Helpers;
using ClashDemo.ViewModels;
using System.Diagnostics;
using System.IO;
using System.Reflection;
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
            this.Closed += (s, e) => NotificationManager.CloseAllNotifications();
            var vm = new MainWindowViewModel();

            this.DataContext = vm;
            navigationBar.SelectedIndex = 0;
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            var frame = sender as Frame;
            var backStack = frame.BackStack?.Cast<object>() ?? new List<object>();
            if (backStack.Count() >= 1)
                while (frame.RemoveBackEntry() != null) { }
        }
    }
}