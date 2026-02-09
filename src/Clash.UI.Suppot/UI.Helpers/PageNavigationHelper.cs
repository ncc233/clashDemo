using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Clash.UI.Suppot.UI.Helpers
{
    public class PageNavigationHelper
    {


        public static Page GetNavigateTarget(DependencyObject obj)
        {
            return (Page)obj.GetValue(NavigateTargetProperty);
        }

        public static void SetNavigateTarget(DependencyObject obj, Page value)
        {
            obj.SetValue(NavigateTargetProperty, value);
        }

        // Using a DependencyProperty as the backing store for NavigateTarget.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NavigateTargetProperty =
            DependencyProperty.RegisterAttached("NavigateTarget", typeof(Page), typeof(PageNavigationHelper), new PropertyMetadata(null,OnTargetChanded));

        private static void OnTargetChanded(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var page=e.NewValue as Page;
            Frame? frame=d as Frame;
            if (frame == null) throw new InvalidOperationException("Frame控件不存在,不可将PageNavigationHelper放置在Frame以外的控件");

            //var story = new Storyboard();
            //var thickAnimation = new ThicknessAnimation();
            //thickAnimation.Duration = TimeSpan.FromSeconds(0.1);
            //thickAnimation.From = new System.Windows.Thickness(0, 10, 0, -10);
            //thickAnimation.To = new System.Windows.Thickness(0);
            //Storyboard.SetTargetProperty(thickAnimation, new PropertyPath(UserControl.MarginProperty));
            //Storyboard.SetTarget(thickAnimation, page);
            //story.Children.Add(thickAnimation);
            frame.Navigate(page);
            //story.Begin();

        }
    }
}
