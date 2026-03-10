using Clash.UI.Suppot.UI.CommonResources.DefaultDefinition;
using Clash.UI.Suppot.UI.Componentes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Clash.UI.Suppot.UI.Helpers
{
    public static class NotificationHelper
    {
        /// <summary>
        /// 显示程序内弹窗（相对于主窗口）
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="level">信息等级</param>
        /// <param name="rightTop">true=右上角，false=左上角</param>
        public static void ShowInAppNotification(string message, NotificationLevel level=NotificationLevel.Info, bool rightTop = true)
        {
            EnsureUIThread(() =>
            {
                var alignment = rightTop ? NotificationAlignment.RightTop : NotificationAlignment.LeftTop;
                var win = new NotificationWindow(message, level, alignment, isDesktop: false);
                win.Owner = Application.Current.MainWindow; // 确保在主窗口之上
                win.Show();
                NotificationManager.AddWindow(win);
            });
        }

        /// <summary>
        /// 显示桌面弹窗（相对于屏幕）
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="level">信息等级</param>
        /// <param name="rightTop">true=屏幕右上角，false=屏幕左上角</param>
        public static void ShowDesktopNotification(string message, NotificationLevel level=NotificationLevel.Info, bool rightTop = true)
        {
            EnsureUIThread(() =>
            {
                var alignment = rightTop ? NotificationAlignment.RightTop : NotificationAlignment.LeftTop;
                var win = new NotificationWindow(message, level, alignment, isDesktop: true);
                win.Show();
                NotificationManager.AddWindow(win);
            });
        }

        private static void EnsureUIThread(Action action)
        {
            if (Application.Current == null)
            {
                throw new InvalidOperationException("Application 未初始化");
            }

            if (Application.Current.Dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                Application.Current.Dispatcher.Invoke(action);
            }
        }
    }
}
