using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Clash.UI.Suppot.UI.Helpers
{
    public static class ScrollViewerAutoScrollHelper
    {
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsEnabled",
                typeof(bool),
                typeof(ScrollViewerAutoScrollHelper),
                new PropertyMetadata(false, OnIsEnabledChanged));

        public static void SetIsEnabled(DependencyObject element, bool value)
            => element.SetValue(IsEnabledProperty, value);

        public static bool GetIsEnabled(DependencyObject element)
            => (bool)element.GetValue(IsEnabledProperty);

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not FrameworkElement element)
                return;

            if ((bool)e.NewValue)
            {
                element.Loaded += OnElementLoaded;
                element.Unloaded += OnElementUnloaded;
            }
            else
            {
                element.Loaded -= OnElementLoaded;
                element.Unloaded -= OnElementUnloaded;
                RemovePreviewMouseWheelHandler(element);
            }
        }

        private static void OnElementLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is not FrameworkElement element)
                return;

            var scrollViewer = FindVisualChild<ScrollViewer>(element);
            if (scrollViewer != null)
            {
                element.PreviewMouseWheel += OnPreviewMouseWheel;
                SetCachedScrollViewer(element, scrollViewer);
            }
        }

        private static void OnElementUnloaded(object sender, RoutedEventArgs e)
        {
            if (sender is not FrameworkElement element)
                return;

            element.PreviewMouseWheel -= OnPreviewMouseWheel;
            SetCachedScrollViewer(element, null);
        }

        private static void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender is not FrameworkElement element)
                return;

            // 1. 尝试获取内部 ScrollViewer（即 ListBox 自身的 ScrollViewer）
            var innerScrollViewer = GetCachedScrollViewer(element) ?? FindVisualChild<ScrollViewer>(element);
            if (innerScrollViewer != null)
            {
                bool canScrollUp = innerScrollViewer.VerticalOffset > 0;
                bool canScrollDown = innerScrollViewer.VerticalOffset < innerScrollViewer.ExtentHeight - innerScrollViewer.ViewportHeight;

                // 如果内部可以滚动，则滚动内部并阻止事件
                if ((e.Delta > 0 && canScrollUp) || (e.Delta < 0 && canScrollDown))
                {
                    ScrollScrollViewer(innerScrollViewer, e.Delta);
                    e.Handled = true;
                    return;
                }
            }

            // 2. 内部不能滚动，则查找外层 ScrollViewer（例如包裹 ItemsControl 的 ScrollViewer）
            var outerScrollViewer = FindAncestorScrollViewer(element);
            if (outerScrollViewer != null)
            {
                bool canScrollUp = outerScrollViewer.VerticalOffset > 0;
                bool canScrollDown = outerScrollViewer.VerticalOffset < outerScrollViewer.ExtentHeight - outerScrollViewer.ViewportHeight;

                // 如果外层可以滚动，则滚动外层并阻止事件
                if ((e.Delta > 0 && canScrollUp) || (e.Delta < 0 && canScrollDown))
                {
                    ScrollScrollViewer(outerScrollViewer, e.Delta);
                    e.Handled = true;
                    return;
                }
            }

            // 3. 都没有可滚动的 ScrollViewer，则不处理，让事件继续传递
        }

        private static void ScrollScrollViewer(ScrollViewer scrollViewer, int delta)
        {
            int lines = SystemParameters.WheelScrollLines;
            if (delta > 0)
            {
                for (int i = 0; i < lines; i++)
                    scrollViewer.LineUp();
            }
            else
            {
                for (int i = 0; i < lines; i++)
                    scrollViewer.LineDown();
            }
        }

        private static ScrollViewer FindAncestorScrollViewer(DependencyObject child)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);
            while (parent != null)
            {
                if (parent is ScrollViewer sv)
                    return sv;
                parent = VisualTreeHelper.GetParent(parent);
            }
            return null;
        }

        private static readonly DependencyProperty CachedScrollViewerProperty =
            DependencyProperty.RegisterAttached(
                "CachedScrollViewer",
                typeof(ScrollViewer),
                typeof(ScrollViewerAutoScrollHelper),
                new PropertyMetadata(null));

        private static void SetCachedScrollViewer(DependencyObject element, ScrollViewer value)
            => element.SetValue(CachedScrollViewerProperty, value);

        private static ScrollViewer GetCachedScrollViewer(DependencyObject element)
            => (ScrollViewer)element.GetValue(CachedScrollViewerProperty);

        private static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T t)
                    return t;

                var result = FindVisualChild<T>(child);
                if (result != null)
                    return result;
            }
            return null;
        }

        private static void RemovePreviewMouseWheelHandler(FrameworkElement element)
        {
            element.PreviewMouseWheel -= OnPreviewMouseWheel;
        }
    }
}