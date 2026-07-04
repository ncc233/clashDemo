using Autofac;
using Autofac.Configuration;
using Clash.UI.Suppot.UI.Componentes;
using Clash.UI.Suppot.UI.Helpers;
using ClashDemo.Views.Dialogs;
using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace ClashDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static Mutex? _mutex;

        public readonly static new App Current = (App)Application.Current;

        public readonly IContainer Container;
        public App()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            var config = new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsetting.json", optional: false, reloadOnChange: true)
            .Build();
            var cfgModel = new ConfigurationModule(config);
            var serviceCollection = new ContainerBuilder();
            serviceCollection.RegisterModule(cfgModel);
            serviceCollection.RegisterType<MainWindow>().SingleInstance();
            serviceCollection.RegisterType<CommonSettingDialog>();
            serviceCollection.RegisterType<AddTestingDialog>();
            var assembly = Assembly.GetExecutingAssembly();
            serviceCollection.RegisterAssemblyTypes(assembly)
                .Where(x => x.Name.EndsWith("Page"))
                .PublicOnly()
                .Where(xx => xx.IsClass).As<Page>();
            //serviceCollection.RegisterAssemblyTypes(assembly)
            //    .Where(x => x.Name.EndsWith("Page") && !x.Name.Contains("AgentPage"))
            //    .PublicOnly()
            //    .Where(xx => xx.IsClass).As<Page>();
            //serviceCollection.RegisterAssemblyTypes(assembly)
            //    .Where(x => x.Name.EndsWith("Page") && x.Name.Contains("AgentPage"))
            //    .PublicOnly()
            //    .Where(xx => xx.IsClass).As<Page>().SingleInstance();
            serviceCollection.RegisterAssemblyTypes(assembly)
                .Where(x => x.Name.EndsWith("ViewModel")&&!x.Name.Contains("SubPageViewModels"))
                .PublicOnly()
                .Where(xx => xx.IsClass).SingleInstance();
            serviceCollection.RegisterAssemblyTypes(assembly)
                .Where(x => x.Name.EndsWith("ViewModel")&&x.Name.Contains("SubPageViewModels"))
                .PublicOnly()
                .Where(xx => xx.IsClass);

            Container = serviceCollection.Build();
        }

        private TaskbarIcon? _notifyIcon;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            if (IsAppAleardyRun())
            {
                Current.Shutdown();
            }
            // 从资源获取托盘图标
            _notifyIcon = (TaskbarIcon)FindResource("TrayIcon");

            // 显示气泡通知
            _notifyIcon?.ShowBalloonTip("提示", "应用已启动，最小化到托盘", BalloonIcon.Info);
            var mainWindow = Container.Resolve<MainWindow>();
            MainWindow = mainWindow;
            mainWindow.Show();

        }

        private bool IsAppAleardyRun()
        {
            bool isRun = false;
            _mutex = new Mutex(true, @"FullDemoApp", out isRun);
            return !isRun;
        }
        protected override void OnExit(ExitEventArgs e)
        {
            NotificationManager.CloseAllNotifications();
            _notifyIcon?.Dispose();
            base.OnExit(e);
        }

        private void ShowWindow_Click(object sender, RoutedEventArgs e)
        {
            ShowMainWindow();

        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void About_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            _notifyIcon?.Dispose();
            Shutdown();
        }
        private void TrayIcon_DoubleClick(object sender, RoutedEventArgs e)
        {
            ToggleMainWindow();
        }

        private void ShowMainWindow()
        {
            var window = Current.MainWindow;
            if (window == null) return;

            window.Show();
            window.WindowState = WindowState.Normal;
            window.Activate();
        }

        private void ToggleMainWindow()
        {
            var window = Current.MainWindow;
            if (window == null) return;

            if (window.IsVisible)
            {
                window.Hide();
            }
            else
            {
                window.Show();
                window.WindowState = WindowState.Normal;
                window.Activate();
            }
        }
    }

}
