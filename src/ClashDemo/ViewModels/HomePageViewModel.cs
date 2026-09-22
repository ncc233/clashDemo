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
using System.Windows;

namespace ClashDemo.ViewModels
{
    [INotifyPropertyChanged]
    public partial class HomePageViewModel
    {
        private CancellationTokenSource _automaticDowngradeFlag;
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
            ShadowdialogHelper.RunDialog(window, common);

        }
        [RelayCommand]
        private void ShowOperatingInstructions()
        {
            //Hyperlink link = new Hyperlink();
            //link.NavigateUri = new Uri("https://www.bilibili.com/");
            //Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri) 
            //{
            //    UseShellExecute = true
            //});

            AutomaticDowngrade(DateTimeOffset.Now + TimeSpan.FromSeconds(10), async (cts) =>
            {
                MessageBox.Show("10");
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
            WeakReferenceMessenger.Default.Send(new NavigationInfo() { PageName = name });
        }
        [RelayCommand]
        private async Task ShowInfoWindow()
        {

            //NotificationHelper.ShowDesktopNotification("可视化元素（包括图片、线等，继承自UIElement）可以实现2D变换，包括平移、旋转、缩放等，通过设置其两个属性来实现，可以设置的属性包括变换类型及变换的原点坐标设置。");
            //NotificationHelper.ShowDesktopNotification("你好2",NotificationLevel.Urgent);
            //NotificationHelper.ShowDesktopNotification("你好3", NotificationLevel.Warning);
            NotificationHelper.ShowDesktopNotification("你好4");
            //NotificationHelper.ShowDesktopNotification("你好5", NotificationLevel.Urgent);
            _automaticDowngradeFlag?.Cancel();

        }
        /// <summary>
        /// 自动降级任务，返回 CancellationTokenSource 以便外部取消任务
        /// </summary>
        public async Task AutomaticDowngrade(
            DateTimeOffset executeAt,
            Func<CancellationToken, Task> callback)
        {
            if (callback is null) throw new ArgumentNullException(nameof(callback));
            if (_automaticDowngradeFlag != null)
            {
                _automaticDowngradeFlag.Cancel();
                await Task.Delay(200);
                _automaticDowngradeFlag.Dispose();
                _automaticDowngradeFlag = null;
            }
            _automaticDowngradeFlag = new CancellationTokenSource();
            // 使用 Task.Run 确保 Schedule 立即返回，不阻塞调用线程
            _ = Task.Run(async () =>
            {
                try
                {
                    await DelayUntilAsync(executeAt, _automaticDowngradeFlag.Token).ConfigureAwait(false);

                    // 到点后执行回调，传入 token，便于回调内部协作取消
                    await callback(_automaticDowngradeFlag.Token).ConfigureAwait(false);
                }
                catch (OperationCanceledException) when (_automaticDowngradeFlag.IsCancellationRequested)
                {
                    // 任务被取消，属于正常流程
                    MessageBox.Show("任务取消");
                }
                catch (Exception ex)
                {
                    // 实际项目中建议使用 ILogger 记录
                    //ShowMsg($"权限自动降级任务执行异常: {ex}");
                }
                finally
                {
                    _automaticDowngradeFlag.Dispose();
                    _automaticDowngradeFlag = null;
                }
            });
        }
        /// <summary>
        /// 延迟点
        /// </summary>
        private static async Task DelayUntilAsync(DateTimeOffset executeAt, CancellationToken token)
        {
            var delay = executeAt - DateTimeOffset.Now;

            if (delay <= TimeSpan.Zero)
                return;
            await Task.Delay(delay, token).ConfigureAwait(false);
        }
    }
}
