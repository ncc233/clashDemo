using Autofac;
using Clash.UI.Suppot.UI.Componentes;
using Clash.UI.Suppot.UI.Helpers;
using ClashDemo.Args;
using ClashDemo.Models;
using ClashDemo.ViewModels.SubPageViewModels;
using ClashDemo.Views.Dialogs;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Clash.UI.Suppot.UI.CommonResources.DefaultDefinition;

namespace ClashDemo.ViewModels
{
    [INotifyPropertyChanged]
    public partial class HomePageViewModel
    {
        private ShadowDialog _shadowDialog;
        public List<HomePageDaskBoardModelBase> DaskBoardItems { get; set; }

        public SubscrubBoardViewModel SubscrubBoardViewModel { get; set; }

        public NetAgentBoardViewModel NetAgentBoardViewModel { get; set; }

        public IPMessagBoardViewModel IPMessagBoardViewModel { get; set; }
        public NetTestBoardViewModel NetTestBoardViewModel { get; set; }
        public HomePageViewModel(SubscrubBoardViewModel subscrubBoardViewModel,
            NetAgentBoardViewModel netAgentBoardViewModel,
            IPMessagBoardViewModel iPMessagBoard,
            NetTestBoardViewModel netTestBoardViewModel) 
        {
            DaskBoardItems = 
                [
                new(){Name="订阅卡"},
                new(){Name="当前代理卡" },
                new(){Name="网络设置卡" },
                new(){Name="代理模式卡" },
                new(){Name="网站测试卡" },
                new(){Name="IP 信息卡" },
                new(){Name="Clash 信息卡" },
                new(){Name="系统信息卡" },
                ];
            SubscrubBoardViewModel = subscrubBoardViewModel;
            NetAgentBoardViewModel = netAgentBoardViewModel;
            IPMessagBoardViewModel = iPMessagBoard;
            NetTestBoardViewModel = netTestBoardViewModel;

        }

        [RelayCommand]
        private void ShowSettingDialog() 
        {
            var window = App.Current.Container.Resolve<MainWindow>();
            UserControl common = App.Current.Container.Resolve<CommonSettingDialog>();
            common.DataContext = this;
            _shadowDialog = new ShadowDialog();
            ShadowdialogHelper.RunDialog(window.gridMain.Children, _shadowDialog, common);
            
        }
        [RelayCommand]
        private void ShowOperatingInstructions() 
        {
            Hyperlink link = new Hyperlink();
            link.NavigateUri = new Uri("https://www.bilibili.com/");
            Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri) 
            {
                UseShellExecute = true
            });

        }

        [RelayCommand]
        private void CancelDashBoardSetting() 
        {
            ShadowdialogHelper.CloseDialog();
        }
        [RelayCommand]
        private void SaveDaskBoardSetting() 
        {
            ShadowdialogHelper.CloseDialog();
        }
        [RelayCommand]
        private void ToNavigateSubscribPage(string name) 
        {
            WeakReferenceMessenger.Default.Send(new NavigationInfo() { PageName= name });
        }
        [RelayCommand]
        private async Task ShowInfoWindow() 
        {

            //NotificationHelper.ShowDesktopNotification("可视化元素（包括图片、线等，继承自UIElement）可以实现2D变换，包括平移、旋转、缩放等，通过设置其两个属性来实现，可以设置的属性包括变换类型及变换的原点坐标设置。");
            //NotificationHelper.ShowDesktopNotification("你好2",NotificationLevel.Urgent);
            //NotificationHelper.ShowDesktopNotification("你好3", NotificationLevel.Warning);
            NotificationHelper.ShowDesktopNotification("你好4");
            //NotificationHelper.ShowDesktopNotification("你好5", NotificationLevel.Urgent);
        }
    }
}
