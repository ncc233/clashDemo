using Autofac;
using ClashDemo.Args;
using ClashDemo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ClashDemo.Helpers
{
    public class PageNavigationHelper
    {


        public static NavigationInfo GetNavigateTarget(DependencyObject obj)
        {
            return (NavigationInfo)obj.GetValue(NavigateTargetProperty);
        }

        public static void SetNavigateTarget(DependencyObject obj, NavigationInfo value)
        {
            obj.SetValue(NavigateTargetProperty, value);
        }

        // Using a DependencyProperty as the backing store for NavigateTarget.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NavigateTargetProperty =
            DependencyProperty.RegisterAttached("NavigateTarget", typeof(NavigationInfo), typeof(PageNavigationHelper), new PropertyMetadata(null,OnTargetChanded));

        private static void OnTargetChanded(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var navigationInfo = e.NewValue as NavigationInfo;
            Frame? frame=d as Frame;
            if (frame == null) throw new InvalidOperationException("Frame控件不存在,不可将PageNavigationHelper放置在Frame以外的控件");
            string pageName = navigationInfo?.PageName;
            Page page = null;
            if (string.IsNullOrEmpty(pageName))
            {
                page = new Page();
                page.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ececec"));
            }
            page = App.Current.Container.ResolveKeyed<Page>(pageName) as Page;
            Func<Task<bool>> func = null;
            if (page == null)
            {
                page = new Page();
                page.Background = Brushes.Red;
            }
            else
            {
                var navigatViewModel = page.DataContext as INavigationViewModel;
                if (navigatViewModel != null)
                {
                    func = navigatViewModel.NavigaedTo;
                }
            }
            //var story = new Storyboard();
            //var thickAnimation = new ThicknessAnimation();
            //thickAnimation.Duration = TimeSpan.FromSeconds(0.1);
            //thickAnimation.From = new System.Windows.Thickness(0, 10, 0, -10);
            //thickAnimation.To = new System.Windows.Thickness(0);
            //Storyboard.SetTargetProperty(thickAnimation, new PropertyPath(UserControl.MarginProperty));
            //Storyboard.SetTarget(thickAnimation, page);
            //story.Children.Add(thickAnimation);
            frame.Navigate(page);
            _=func?.Invoke().ConfigureAwait(false);
            //story.Begin();

        }
    }
}
