using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Clash.UI.Suppot.UI.Helpers
{
    public class ScrollViewerToHelper
    {





        public static bool GetIsToTop(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsToTopProperty);
        }

        public static void SetIsToTop(DependencyObject obj, bool value)
        {
            obj.SetValue(IsToTopProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsToTop.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsToTopProperty =
            DependencyProperty.RegisterAttached("IsToTop", typeof(bool), typeof(ScrollViewerToHelper), new PropertyMetadata(false));



        public static double GetAnimationTime(DependencyObject obj)
        {
            return (double)obj.GetValue(AnimationTimeProperty);
        }

        public static void SetAnimationTime(DependencyObject obj, double value)
        {
            obj.SetValue(AnimationTimeProperty, value);
        }

        // Using a DependencyProperty as the backing store for AnimationTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimationTimeProperty =
            DependencyProperty.RegisterAttached("AnimationTime", typeof(double), typeof(ScrollViewerToHelper), new PropertyMetadata(1d));


        public static Orientation GetOrientation(DependencyObject obj)
        {
            return (Orientation)obj.GetValue(OrientationProperty);
        }

        public static void SetOrientation(DependencyObject obj, Orientation value)
        {
            obj.SetValue(OrientationProperty, value);
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.RegisterAttached("Orientation", typeof(Orientation), typeof(ScrollViewerToHelper), new PropertyMetadata(Orientation.Vertical));



        public static ScrollViewer GetOriginScrollViewer(DependencyObject obj)
        {
            return (ScrollViewer)obj.GetValue(OriginScrollViewerProperty);
        }

        public static void SetOriginScrollViewer(DependencyObject obj, ScrollViewer value)
        {
            obj.SetValue(OriginScrollViewerProperty, value);
        }

        // Using a DependencyProperty as the backing store for OriginScrollViewer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OriginScrollViewerProperty =
            DependencyProperty.RegisterAttached("OriginScrollViewer", typeof(ScrollViewer), typeof(ScrollViewerToHelper), new PropertyMetadata(null, OnOriginScrollViewer));

        private static void OnOriginScrollViewer(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Button btn)
            {
                if (GetIsToTop(d))
                {
                    btn.Click += Btn_Click;
                    var scrollViewer=e.NewValue as ScrollViewer;
                    scrollViewer.ScrollChanged += (s, e) =>
                    {
                        var scr=s as ScrollViewer;
                        btn.IsEnabled = scr.VerticalOffset > 90;
                    };

                }
                else
                {
                    btn.Loaded += (s, e) =>
                    {
                        btn.MouseEnter += Btn_MouseEnter;

                    };
                    btn.Unloaded += (s, e) =>
                    {
                        btn.MouseEnter -= Btn_MouseEnter;
                    };
                }
            }
        }

        private static void Btn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
                BeginAnimation(GetOriginScrollViewer(btn), new Point(0, 0), Orientation.Vertical, GetAnimationTime(btn));
        }

        private static void Btn_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is Button btn)
            {
                var point = DistanceCalculation(GetOriginScrollViewer(btn), GetTargetItems(btn).ItemContainerGenerator.ContainerFromItem(btn.DataContext), btn.DataContext);
                BeginAnimation(GetOriginScrollViewer(btn), point, GetOrientation(btn), GetAnimationTime(btn));
            }
        }


        public static ItemsControl GetTargetItems(DependencyObject obj)
        {
            return (ItemsControl)obj.GetValue(TargetItemsProperty);
        }

        public static void SetTargetItems(DependencyObject obj, ItemsControl value)
        {
            obj.SetValue(TargetItemsProperty, value);
        }

        // Using a DependencyProperty as the backing store for TargetItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetItemsProperty =
            DependencyProperty.RegisterAttached("TargetItems", typeof(ItemsControl), typeof(ScrollViewerToHelper), new PropertyMetadata(null));


        private static Point DistanceCalculation(ScrollViewer scrollViewer, DependencyObject targetLocationControl, object item)
        {
            Point point = new Point();
            if (targetLocationControl is Visual visual)
            {
                Point originPoint = new Point(scrollViewer.HorizontalOffset, scrollViewer.VerticalOffset);
                point = visual.TransformToAncestor(scrollViewer).Transform(originPoint);
            }
            return point;
        }

        private static void BeginAnimation(ScrollViewer scrollViewer, Point point, Orientation orientation, double time)
        {
            double targetLoaction = orientation is Orientation.Vertical ? point.Y : point.X;
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.Duration = TimeSpan.FromSeconds(time);
            doubleAnimation.EasingFunction = new QuarticEase
            {
                EasingMode = EasingMode.EaseInOut,
            };
            doubleAnimation.From = orientation is Orientation.Vertical ? scrollViewer.VerticalOffset : scrollViewer.VerticalOffset;
            doubleAnimation.To = targetLoaction;
            scrollViewer.BeginAnimation(orientation is Orientation.Vertical ? AttachPropertyHelper.VerticalScrollToProperty : AttachPropertyHelper.HorizontalScrollToProperty, doubleAnimation);
        }
    }
}
