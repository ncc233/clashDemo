using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Clash.UI.Suppot.UI.Helpers
{
    public class RotationBehaviorHelper
    {
        // 附加属性：是否启用旋转行为
        public static readonly DependencyProperty IsRotationEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsRotationEnabled",
                typeof(bool),
                typeof(RotationBehaviorHelper),
                new PropertyMetadata(false, OnIsRotationEnabledChanged));

        public static void SetIsRotationEnabled(DependencyObject element, bool value) =>
            element.SetValue(IsRotationEnabledProperty, value);

        public static bool GetIsRotationEnabled(DependencyObject element) =>
            (bool)element.GetValue(IsRotationEnabledProperty);

        // 属性变更回调：订阅/取消订阅控件的生命周期事件
        private static void OnIsRotationEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not FrameworkElement element)
                return;

            bool oldValue = (bool)e.OldValue;
            bool newValue = (bool)e.NewValue;

            if (newValue)
            {
                element.Loaded += OnElementLoaded;
                element.Unloaded += OnElementUnloaded;
                if (element.IsLoaded)
                    SubscribeToIsEnabledChanged(element);
            }
            else
            {
                element.Loaded -= OnElementLoaded;
                element.Unloaded -= OnElementUnloaded;
                UnsubscribeFromIsEnabledChanged(element);
                StopRotationAnimation(element);
            }
        }

        // 控件加载时订阅 IsEnabledChanged 事件
        private static void OnElementLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element)
                SubscribeToIsEnabledChanged(element);
        }

        // 控件卸载时取消订阅并停止动画
        private static void OnElementUnloaded(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                UnsubscribeFromIsEnabledChanged(element);
                StopRotationAnimation(element);
            }
        }

        private static void SubscribeToIsEnabledChanged(FrameworkElement element)
        {
            element.IsEnabledChanged += OnIsEnabledChanged;
            // 如果当前已禁用，立即启动动画
            if (!element.IsEnabled)
                StartRotationAnimation(element);
        }

        private static void UnsubscribeFromIsEnabledChanged(FrameworkElement element)
        {
            element.IsEnabledChanged -= OnIsEnabledChanged;
            StopRotationAnimation(element);
        }

        // IsEnabled 变化时启动/停止动画
        private static void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is not FrameworkElement element)
                return;

            if ((bool)e.NewValue)
                StopRotationAnimation(element);
            else
                StartRotationAnimation(element);
        }

        // 启动旋转动画
        private static void StartRotationAnimation(FrameworkElement element)
        {
            RotateTransform rotate = GetOrCreateRotateTransform(element);
            if (rotate == null) return;

            var animation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = TimeSpan.FromSeconds(1),   // 可调整或通过附加属性配置
                RepeatBehavior = RepeatBehavior.Forever
            };

            rotate.BeginAnimation(RotateTransform.AngleProperty, animation);
        }

        // 停止旋转动画
        private static void StopRotationAnimation(FrameworkElement element)
        {
            RotateTransform rotate = GetExistingRotateTransform(element);
            if (rotate != null)
            {
                rotate.BeginAnimation(RotateTransform.AngleProperty, null);
                rotate.Angle = 0;   // 复位角度
            }
        }

        // 获取现有的 RotateTransform（不创建）
        private static RotateTransform GetExistingRotateTransform(FrameworkElement element)
        {
            Transform transform = element.RenderTransform;
            return transform switch
            {
                RotateTransform rotate => rotate,
                TransformGroup group => GetRotateFromGroup(group),
                _ => null
            };
        }

        // 从 TransformGroup 中查找 RotateTransform
        private static RotateTransform GetRotateFromGroup(TransformGroup group)
        {
            foreach (Transform child in group.Children)
                if (child is RotateTransform rt)
                    return rt;
            return null;
        }

        // 获取或创建 RotateTransform，保留原有的变换结构
        private static RotateTransform GetOrCreateRotateTransform(FrameworkElement element)
        {
            Transform transform = element.RenderTransform;
            if (transform == null)
            {
                var rotateRender = new RotateTransform();
                element.RenderTransform = rotateRender;
                return rotateRender;
            }

            if (transform is RotateTransform rotate)
                return rotate;

            if (transform is TransformGroup group)
            {
                var existing = GetRotateFromGroup(group);
                if (existing != null)
                    return existing;

                var newRotate = new RotateTransform();
                group.Children.Add(newRotate);
                return newRotate;
            }

            // 其他变换类型：包装到 TransformGroup 中
            var group2 = new TransformGroup();
            group2.Children.Add(transform);
            var newRotate2 = new RotateTransform();
            group2.Children.Add(newRotate2);
            element.RenderTransform = group2;
            return newRotate2;
        }
    }
}
