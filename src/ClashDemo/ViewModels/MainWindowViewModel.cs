using Clash.UI.Suppot.UI.Controls;
using ClashDemo.Args;
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
        public List<NavigationButton> NavigationItems { get; set; }

        public MainWindowViewModel()
        {

            IniNavigationBar();
            WeakReferenceMessenger.Default.Register<NavigationInfo>(this,ExternNavigationTask);
        }


        private void IniNavigationBar()
        {
            var rsdic = new ResourceDictionary()
            {
                Source = new Uri("pack://Application:,,,/Clash.UI.Suppot;component/UI.CommonResources/NavigationGeometry.xaml")
            };
            Dictionary<string, string> keyValuePairs = [];
            keyValuePairs.Add("首 页", "homeGeometry");
            keyValuePairs.Add("代 理", "netAgentGeometry");
            keyValuePairs.Add("订 阅", "subscribeGeometry");
            keyValuePairs.Add("连 接", "connectionGeometry");
            keyValuePairs.Add("规 则", "ruleGeometry");
            keyValuePairs.Add("日 志", "logGeometry");
            keyValuePairs.Add("测 试", "testGeometry");
            keyValuePairs.Add("设 置", "settingGeometry");

            NavigationItems = [];
            foreach (var item in keyValuePairs)
            {
                var geometry = rsdic[item.Value];
                if (geometry is null) continue;
                NavigationItems.Add(new NavigationButton()
                {
                    Content = item.Key,
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
