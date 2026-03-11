using Clash.UI.Suppot.UI.Adorners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Clash.UI.Suppot.UI.Helpers
{
    public class ControlSuspensionHelper
    {
        private static Dictionary<int, Thickness> _originMargin = new Dictionary<int, Thickness>();
        public static double GetMoveValue(DependencyObject obj)
        {
            return (double)obj.GetValue(MoveValueProperty);
        }

        public static void SetMoveValue(DependencyObject obj, double value)
        {
            obj.SetValue(MoveValueProperty, value);
        }

        // Using a DependencyProperty as the backing store for MoveValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MoveValueProperty =
            DependencyProperty.RegisterAttached("MoveValue", typeof(double), typeof(ControlSuspensionHelper), new PropertyMetadata(2d));



        public static bool GetIEnable(DependencyObject obj)
        {
            return (bool)obj.GetValue(IEnableProperty);
        }

        public static void SetIEnable(DependencyObject obj, bool value)
        {
            obj.SetValue(IEnableProperty, value);
        }

        // Using a DependencyProperty as the backing store for IEnable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IEnableProperty =
            DependencyProperty.RegisterAttached("IEnable", typeof(bool), typeof(ControlSuspensionHelper), new PropertyMetadata(false, OnEnableChanged));

        private static void OnEnableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as FrameworkElement;
            if (control != null)
            {
                control.PreviewMouseDown += Control_MouseDown;
                control.PreviewMouseUp += Control_MouseUp;
                control.MouseEnter += Control_MouseEnter;
                control.MouseLeave += Control_MouseLeave;
                control.Loaded += Control_Loaded;
                control.Unloaded += Control_Unloaded;
            }
        }

        private static void Control_Unloaded(object sender, RoutedEventArgs e)
        {
            var control = sender as FrameworkElement;
            if (control != null)
            {
                control.PreviewMouseDown -= Control_MouseDown;
                control.PreviewMouseUp -= Control_MouseUp;
                control.MouseEnter -= Control_MouseEnter;
                control.MouseLeave -= Control_MouseLeave;
                control.Loaded -= Control_Loaded;
                control.Unloaded -= Control_Unloaded;
            }
        }

        private static void Control_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                MoveAnimation(sender, element);
            }
        }


        private static void Control_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                _originMargin.TryAdd(sender.GetHashCode(), element.Margin);
            }
        }

        private static void Control_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                if (_originMargin.TryGetValue(sender.GetHashCode(), out var margin))
                {
                    var story = CreateTrasnformAnimation(element, margin);
                    story.Begin();
                }
            }
        }

        private static void Control_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                MoveAnimation(sender, element);
            }
        }

        private static void Control_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                if (_originMargin.TryGetValue(sender.GetHashCode(), out var margin))
                {
                    var story = CreateTrasnformAnimation(element, margin);
                    story.Begin();
                }
            }
        }

        private static void MoveAnimation(object sender, FrameworkElement element)
        {
            if (_originMargin.TryGetValue(sender.GetHashCode(), out var margin))
            {
                var move = new Thickness(margin.Left, margin.Top - GetMoveValue(element), margin.Right, margin.Bottom + GetMoveValue(element));
                var story = CreateTrasnformAnimation(element, move);
                story.Begin();
            }
        }
        private static Storyboard CreateTrasnformAnimation(FrameworkElement frameworkElement, Thickness margin)
        {
            var story = new Storyboard();
            ThicknessAnimation animation = new ThicknessAnimation();
            animation.Duration = TimeSpan.FromSeconds(0.2);
            animation.To = margin;
            Storyboard.SetTargetProperty(animation, new PropertyPath(FrameworkElement.MarginProperty));
            Storyboard.SetTarget(animation, frameworkElement);
            story.Children.Add(animation);
            return story;
        }
    }
}
