using Autofac;
using Autofac.Configuration;
using Clash.UI.Suppot.UI.Componentes;
using Clash.UI.Suppot.UI.Helpers;
using ClashDemo.Views.Dialogs;
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
            //    .Where(x => x.Name.EndsWith("Page") &&! x.Name.Contains("AgentPage"))
            //    .PublicOnly()
            //    .Where(xx => xx.IsClass).As<Page>();
            //serviceCollection.RegisterAssemblyTypes(assembly)
            //    .Where(x => x.Name.EndsWith("Page")&&x.Name.Contains("AgentPage"))
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

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            if (IsAppAleardyRun())
            {
                Current.Shutdown();
            }
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
            base.OnExit(e);
        }
    }

}
