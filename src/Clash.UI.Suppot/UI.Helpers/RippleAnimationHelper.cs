using Clash.UI.Suppot.UI.Adorners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;

namespace Clash.UI.Suppot.UI.Helpers
{
    public class RippleAnimationHelper
    {




        public static bool GetIsEnable(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnableProperty);
        }

        public static void SetIsEnable(DependencyObject obj, bool value)
        {
            obj.SetValue(IsEnableProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsEnable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEnableProperty =
            DependencyProperty.RegisterAttached("IsEnable", typeof(bool), typeof(RippleAnimationHelper), new PropertyMetadata(false,OnIsEnable));


        public static double GetRippleParentRadius(DependencyObject obj)
        {
            return (double)obj.GetValue(RippleParentRadiusProperty);
        }

        public static void SetRippleParentRadius(DependencyObject obj, double value)
        {
            obj.SetValue(RippleParentRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for RippleParentRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RippleParentRadiusProperty =
            DependencyProperty.RegisterAttached("RippleParentRadius", typeof(double), typeof(RippleAnimationHelper), new PropertyMetadata(0d));





        public static double GetRippleTime(DependencyObject obj)
        {
            return (double)obj.GetValue(RippleTimeProperty);
        }

        public static void SetRippleTime(DependencyObject obj, double value)
        {
            obj.SetValue(RippleTimeProperty, value);
        }

        // Using a DependencyProperty as the backing store for RippleTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RippleTimeProperty =
            DependencyProperty.RegisterAttached("RippleTime", typeof(double), typeof(RippleAnimationHelper), new PropertyMetadata(0.5));



        public static SolidColorBrush GetRippleBrush(DependencyObject obj)
        {
            return (SolidColorBrush)obj.GetValue(RippleBrushProperty);
        }

        public static void SetRippleBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(RippleBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for RippleBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RippleBrushProperty =
            DependencyProperty.RegisterAttached("RippleBrush", typeof(SolidColorBrush), typeof(RippleAnimationHelper), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"))));

        private static void OnIsEnable(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as FrameworkElement;
            if (control != null)
            {
                control.PreviewMouseDown += Control_MouseDown;
                control.PreviewMouseUp += Control_MouseUp;
                control.MouseLeave += Control_MouseLeave;
                control.Loaded += Control_Loaded;
            }
        }

        private static void Control_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(element);
                if (adornerLayer == null) return;
                RippleAnimationAdorner adorner = null;
                adorner = new RippleAnimationAdorner(element);
                adornerLayer.Add(adorner);
                SetRippleAnimationAdorner(element, adorner);
            }
        }

        private static readonly DependencyProperty PulseAdornerProperty =
    DependencyProperty.RegisterAttached(
        "RippleAnimationAdorner",
        typeof(RippleAnimationAdorner),
        typeof(RippleAnimationHelper));

        private static RippleAnimationAdorner GetRippleAnimationAdorner(DependencyObject obj)
        {
            return (RippleAnimationAdorner)obj.GetValue(PulseAdornerProperty);
        }

        private static void SetRippleAnimationAdorner(DependencyObject obj, RippleAnimationAdorner value)
        {
            obj.SetValue(PulseAdornerProperty, value);
        }
        private static void AddRippleAnimationAdorner(FrameworkElement element)
        {
            RippleAnimationAdorner adorner = null;
            adorner = GetRippleAnimationAdorner(element);
            SetRippleAnimationAdorner(element, adorner);
            adorner.AddAnimation(element, GetRippleBrush(element),GetRippleParentRadius(element), GetRippleTime(element));
        }
        private static void RemovePulseAdorner(UIElement element)
        {
            var adorner = GetRippleAnimationAdorner(element);
            if (adorner == null) return;

            var adornerLayer = AdornerLayer.GetAdornerLayer(element);
            adornerLayer?.Remove(adorner);
            SetRippleAnimationAdorner(element, null);
        }
        private static void Control_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is FrameworkElement fe)
            {
                var adorner = GetRippleAnimationAdorner(fe);
                if (adorner == null) return;
                adorner.OpacitiesAnimation(GetRippleTime(fe)*0.7);
            }
        }

        private static void Control_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement fe)
            {
                var adorner = GetRippleAnimationAdorner(fe);
                adorner.OpacitiesAnimation(GetRippleTime(fe) * 0.7);
            }
        }

        private static void Control_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement fe)
            {
                var position = e.GetPosition(fe);

                AddRippleAnimationAdorner(fe);
            }

        }
    }
}
