using Autofac;
using Autofac.Core;
using Clash.UI.Suppot.UI.Componentes;
using Clash.UI.Suppot.UI.Helpers;
using ClashDemo.ViewModels;
using ClashDemo.Views.Dialogs;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
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

namespace ClashDemo.Views
{
    /// <summary>
    /// HomPage.xaml 的交互逻辑
    /// </summary>
    public partial class HomPage : Page
    {
        public HomPage()
        {
            InitializeComponent();
        }
        public HomPage(HomePageViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var window=App.Current.Container.Resolve<MainWindow>();
            UserControl common = App.Current.Container.Resolve<CommonSettingDialog>();
            common.DataContext = this.DataContext;
            ShadowdialogHelper.RunDialog(window.gridMain.Children,new ShadowDialog(),common);
        }

    }
}
