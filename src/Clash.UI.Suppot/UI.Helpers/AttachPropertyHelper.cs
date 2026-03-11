using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Clash.UI.Suppot.UI.Helpers
{
    public class AttachPropertyHelper
    {



        public static CornerRadius GetCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(AttachPropertyHelper), new PropertyMetadata(new CornerRadius(0),OnCornerRadiusChanged));

        private static void OnCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }




        public static double GetVerticalScrollTo(DependencyObject obj)
        {
            return (double)obj.GetValue(VerticalScrollToProperty);
        }

        public static void SetVerticalScrollTo(DependencyObject obj, double value)
        {
            obj.SetValue(VerticalScrollToProperty, value);
        }

        // Using a DependencyProperty as the backing store for VerticalScrollTo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalScrollToProperty =
            DependencyProperty.RegisterAttached("VerticalScrollTo", typeof(double), typeof(AttachPropertyHelper), new PropertyMetadata(0d, (s, e) => 
            {
                if (s is ScrollViewer scrollViewer && e.NewValue is double newValue)
                {
                    scrollViewer.ScrollToVerticalOffset(newValue);
                }
            }));




        public static double GetHorizontalScrollTo(DependencyObject obj)
        {
            return (double)obj.GetValue(HorizontalScrollToProperty);
        }

        public static void SetHorizontalScrollTo(DependencyObject obj, double value)
        {
            obj.SetValue(HorizontalScrollToProperty, value);
        }

        // Using a DependencyProperty as the backing store for HorizontalScrollTo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalScrollToProperty =
            DependencyProperty.RegisterAttached("HorizontalScrollTo", typeof(double), typeof(AttachPropertyHelper), new PropertyMetadata(0d, (s, e) => 
            {
                if (s is ScrollViewer scrollViewer && e.NewValue is double newValue)
                {
                    scrollViewer.ScrollToHorizontalOffset(newValue);
                }
            }));




    }
}
