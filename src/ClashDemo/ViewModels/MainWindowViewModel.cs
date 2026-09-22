using Clash.UI.Suppot.UI.Controls;
using ClashDemo.Args;
using ClashDemo.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ClashDemo.ViewModels
{
    [INotifyPropertyChanged]
    public partial class MainWindowViewModel
    {

        public CancellationTokenSource TestCancel { get; set; }

        public List<NavigationButton> NavigationItems { get; set; }

        public MainWindowViewModel()
        {

            IniNavigationBar();
            WeakReferenceMessenger.Default.Register<NavigationInfo>(this,ExternNavigationTask);
        }


        private void IniNavigationBar()
        {
            var data=TestCancel;
            var rsdic = new ResourceDictionary()
            {
                Source = new Uri("pack://Application:,,,/Clash.UI.Suppot;component/UI.CommonResources/NavigationGeometry.xaml")
            };
            Dictionary<string,(string tag,string icon)> keyValuePairs = [];
            keyValuePairs.Add("首 页",(nameof(HomPage), "homeGeometry"));
            keyValuePairs.Add("代 理", (nameof(AgentPage), "netAgentGeometry"));
            keyValuePairs.Add("订 阅", (nameof(SubscribePage), "subscribeGeometry"));
            keyValuePairs.Add("连 接", (nameof(ConnectionPage), "connectionGeometry"));
            keyValuePairs.Add("规 则", (nameof(RulePage), "ruleGeometry"));
            keyValuePairs.Add("日 志", (nameof(LoggingPage), "logGeometry"));
            keyValuePairs.Add("测 试", (nameof(TestingPage), "testGeometry"));
            keyValuePairs.Add("设 置", (nameof(SettingPage), "settingGeometry"));

            NavigationItems = [];
            foreach (var item in keyValuePairs)
            {
                var geometry = rsdic[item.Value.icon];
                if (geometry is null) continue;
                NavigationItems.Add(new NavigationButton()
                {
                    Content = item.Key,
                    Tag=item.Value.tag,
                    HeaderIcon = geometry as Geometry,
                });
            }

        }

        private void ExternNavigationTask(object recipient,NavigationInfo info) 
        {
            var navItem=NavigationItems.FirstOrDefault(x=>x.Content.ToString().Contains(info.PageName));
            if (navItem is not null)
            {
                navItem.IsSelected = true;
            }
        }
    }
}
