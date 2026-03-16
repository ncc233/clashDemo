using Clash.UI.Suppot.UI.CommonResources.DefaultDefinition;
using Clash.UI.Suppot.UI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Clash.UI.Suppot.UI.Componentes
{

    /// <summary>
    /// NotificationWindow.xaml 的交互逻辑
    public partial class NotificationWindow : Window
    {
        private DispatcherTimer _autoCloseTimer;
        private bool _isMouseOver;
        private NotificationLevel _level;
        private string _message;
        private NotificationAlignment _alignment;
        private bool _isDesktop;

        // 供 XAML 绑定的背景色
        public SolidColorBrush BackgroundColor { get; private set; }

        public NotificationWindow(string message, NotificationLevel level, NotificationAlignment alignment, bool isDesktop)
        {
            InitializeComponent();

            _message = message;
            _level = level;
            _alignment = alignment;
            _isDesktop = isDesktop;

            // 根据等级设置背景色
            switch (level)
            {
                case NotificationLevel.Info:
                    MainBorder.Background = new SolidColorBrush(Color.FromRgb(0, 120, 215));
                    break;
                case NotificationLevel.Warning:
                    MainBorder.Background = new SolidColorBrush(Color.FromRgb(255, 140, 0));
                    break;
                case NotificationLevel.Urgent:
                    MainBorder.Background = new SolidColorBrush(Color.FromRgb(232, 17, 35));
                    break;
            }

            // 设置图标
            IconText.Text = level switch
            {
                NotificationLevel.Info => "\uE946",
                NotificationLevel.Warning => "\uE7BA",
                NotificationLevel.Urgent => "\uEA39",
                _ => "\uE946"
            };

            MessageText.Text = message;

            // 自动关闭计时器
            _autoCloseTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };
            _autoCloseTimer.Tick += AutoCloseTimer_Tick;

            this.Topmost = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 初始缩放为0（窗口刚显示时不可见）
            BorderScale.ScaleX = 0.5;
            BorderScale.ScaleY = 0.5;

            // 动画到正常大小（150ms）
            var scaleXAnim = new DoubleAnimation(1, TimeSpan.FromSeconds(0.1));
            var scaleYAnim = new DoubleAnimation(1, TimeSpan.FromSeconds(0.1));
            BorderScale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnim);
            BorderScale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnim);


            ////平移动画
            //MainBorder.Margin = new(120, 0, 0, 0);
            //var thicknessAnimation = new ThicknessAnimation(new Thickness(0),TimeSpan.FromSeconds(0.2));
            //MainBorder.BeginAnimation(Window.MarginProperty,thicknessAnimation);

            //透明度动画
            this.Opacity = 0.2;
            var opacityAnimation = new DoubleAnimation(1, TimeSpan.FromSeconds(0.05));
            this.BeginAnimation(Window.OpacityProperty, opacityAnimation);

            _autoCloseTimer.Start();
        }

        private void AutoCloseTimer_Tick(object sender, EventArgs e)
        {
            if (!_isMouseOver)
            {
                _autoCloseTimer.Stop();
                BeginAutoCloseAnimation();
            }
        }

        private void BeginAutoCloseAnimation()
        {
            // 通知管理器移除自己（堆叠重新布局）
            NotificationManager.BeginRemove(this);

            // 窗口向上移动50像素
            var topAnim = new DoubleAnimation(this.Top - 50, TimeSpan.FromMilliseconds(300));
            var opacityAnim = new DoubleAnimation(0, TimeSpan.FromMilliseconds(300));
            opacityAnim.Completed += (s, _) => this.Close();

            this.BeginAnimation(Window.TopProperty, topAnim);
            this.BeginAnimation(Window.OpacityProperty, opacityAnim);
        }
        private void Window_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _isMouseOver = true;
            _autoCloseTimer.Stop();
        }

        private void Window_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _isMouseOver = false;
            if (_autoCloseTimer != null && !_autoCloseTimer.IsEnabled)
            {
                _autoCloseTimer.Start();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            _autoCloseTimer?.Stop();
            BeginManualCloseAnimation();
        }

        private void BeginManualCloseAnimation()
        {
            // 通知管理器移除自己（让堆叠重新布局）
            NotificationManager.BeginRemove(this);

            //var scaleXAnim = new DoubleAnimation(0, TimeSpan.FromMilliseconds(150));
            //var scaleYAnim = new DoubleAnimation(0, TimeSpan.FromMilliseconds(150));
            //scaleXAnim.Completed += (s, _) => this.Close();

            //BorderScale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnim);
            //BorderScale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnim);
            this.Close();
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            _autoCloseTimer?.Stop();
            base.OnClosing(e);
        }

        // 公开属性供管理器访问
        public NotificationAlignment Alignment => _alignment;
        public bool IsDesktop => _isDesktop;
    }
}
