using Clash.UI.Suppot.UI.Componentes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Clash.UI.Suppot.UI.Helpers
{
    public static class NotificationManager
    {
        private static readonly object _lock = new object();
        private static readonly Dictionary<NotificationAlignment, List<NotificationWindow>> _activeWindows = new();

        // 窗口尺寸常量（应与 XAML 中一致）
        private const double WindowWidth = 350;
        private const double WindowHeight = 70;
        private const double Margin = 10;

        public static void AddWindow(NotificationWindow window)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                lock (_lock)
                {
                    var alignment = window.Alignment;
                    if (!_activeWindows.ContainsKey(alignment))
                        _activeWindows[alignment] = new List<NotificationWindow>();

                    _activeWindows[alignment].Add(window);
                    LayoutWindows(alignment);
                }
            });
        }

        public static void BeginRemove(NotificationWindow window)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                lock (_lock)
                {
                    var alignment = window.Alignment;
                    if (_activeWindows.ContainsKey(alignment))
                    {
                        _activeWindows[alignment].Remove(window);
                        LayoutWindows(alignment);
                    }
                }
            });
        }

        private static void LayoutWindows(NotificationAlignment alignment)
        {
            if (!_activeWindows.ContainsKey(alignment))
                return;

            var windows = _activeWindows[alignment];
            if (windows.Count == 0) return;

            // 确定参考坐标系
            double screenLeft = 0, screenTop = 0, screenWidth = 0, screenHeight = 0;
            double refLeft, refTop, refRight, refBottom;

            // 获取主窗口或屏幕工作区
            var mainWindow = Application.Current.MainWindow;
            bool isDesktop = windows.First().IsDesktop; // 假设同一 alignment 下类型一致（可改进）

            if (isDesktop)
            {
                var workArea = SystemParameters.WorkArea;
                screenLeft = workArea.Left;
                screenTop = workArea.Top;
                screenWidth = workArea.Width;
                screenHeight = workArea.Height;
                refLeft = screenLeft;
                refTop = screenTop;
                refRight = screenLeft + screenWidth;
                refBottom = screenTop + screenHeight;
            }
            else
            {
                if (mainWindow == null) return;
                refLeft = mainWindow.Left;
                refTop = mainWindow.Top;
                refRight = mainWindow.Left + mainWindow.Width;
                refBottom = mainWindow.Top + mainWindow.Height;
            }

            for (int i = 0; i < windows.Count; i++)
            {
                var win = windows[i];
                double left, top;

                switch (alignment)
                {
                    case NotificationAlignment.RightTop:
                        left = refRight - WindowWidth - Margin;
                        top = refTop + i * (WindowHeight + Margin) + Margin;
                        break;
                    case NotificationAlignment.LeftTop:
                        left = refLeft + Margin;
                        top = refTop + i * (WindowHeight + Margin) + Margin;
                        break;
                    // 可扩展其他对齐方式
                    default:
                        left = refRight - WindowWidth - Margin;
                        top = refTop + i * (WindowHeight + Margin) + Margin;
                        break;
                }

                // 确保不超出屏幕（可选）
                if (isDesktop)
                {
                    left = Math.Max(screenLeft, Math.Min(left, screenLeft + screenWidth - WindowWidth));
                    top = Math.Max(screenTop, Math.Min(top, screenTop + screenHeight - WindowHeight));
                }

                win.Left = left;
                win.Top = top;
            }
        }
        public static void CloseAllNotifications()
        {
            if (Application.Current == null) return;

            Application.Current.Dispatcher.Invoke(() =>
            {
                lock (_lock)
                {
                    // 收集所有窗口
                    var allWindows = _activeWindows.Values.SelectMany(list => list).ToList();
                    foreach (var win in allWindows)
                    {
                        if (win != null && win.IsVisible)
                        {
                            win.Close();
                        }
                    }
                    _activeWindows.Clear();
                }
            });
        }
    }
    public enum NotificationAlignment
    {
        LeftTop,
        RightTop
        // 可添加更多
    }
}
