using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Clash.UI.Suppot.UI.Helpers
{
    /// <summary>
    /// 涟漪的生成位置
    /// </summary>
    public enum RippleOrigin
    {
        /// <summary>控件正中心</summary>
        Center,
        /// <summary>鼠标点击处</summary>
        Pointer
    }

    /// <summary>
    /// 控件点击涟漪效果（附加属性）
    /// </summary>
    public static class Ripple
    {
        #region 内部状态

        private sealed class RippleState
        {
            /// <summary>当前控件对应的装饰层</summary>
            public RippleAdorner Adorner;
            /// <summary>按下时创建、抬起时需要淡出的那个涟漪</summary>
            public Ellipse Active;
        }

        private static readonly ConditionalWeakTable<UIElement, RippleState> States
            = new ConditionalWeakTable<UIElement, RippleState>();

        private static RippleState GetState(UIElement element)
        {
            if (!States.TryGetValue(element, out var state))
            {
                state = new RippleState();
                States.Add(element, state);
            }
            return state;
        }

        #endregion

        #region 附加属性

        // ---------------- IsEnabled ----------------
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsEnabled", typeof(bool), typeof(Ripple),
                new PropertyMetadata(false, OnIsEnabledChanged));

        public static bool GetIsEnabled(DependencyObject d) => (bool)d.GetValue(IsEnabledProperty);
        public static void SetIsEnabled(DependencyObject d, bool v) => d.SetValue(IsEnabledProperty, v);

        // ---------------- Color ----------------
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.RegisterAttached(
                "Color", typeof(Color), typeof(Ripple),
                new PropertyMetadata(Colors.Black));

        public static Color GetColor(DependencyObject d) => (Color)d.GetValue(ColorProperty);
        public static void SetColor(DependencyObject d, Color v) => d.SetValue(ColorProperty, v);

        // ---------------- Origin ----------------
        public static readonly DependencyProperty OriginProperty =
            DependencyProperty.RegisterAttached(
                "Origin", typeof(RippleOrigin), typeof(Ripple),
                new PropertyMetadata(RippleOrigin.Pointer));

        public static RippleOrigin GetOrigin(DependencyObject d) => (RippleOrigin)d.GetValue(OriginProperty);
        public static void SetOrigin(DependencyObject d, RippleOrigin v) => d.SetValue(OriginProperty, v);

        // ---------------- MaxOpacity ----------------
        public static readonly DependencyProperty MaxOpacityProperty =
            DependencyProperty.RegisterAttached(
                "MaxOpacity", typeof(double), typeof(Ripple),
                new PropertyMetadata(0.2));

        public static double GetMaxOpacity(DependencyObject d) => (double)d.GetValue(MaxOpacityProperty);
        public static void SetMaxOpacity(DependencyObject d, double v) => d.SetValue(MaxOpacityProperty, v);

        // ---------------- ExpandDuration ----------------
        public static readonly DependencyProperty ExpandDurationProperty =
            DependencyProperty.RegisterAttached(
                "ExpandDuration", typeof(TimeSpan), typeof(Ripple),
                new PropertyMetadata(TimeSpan.FromMilliseconds(600)));

        public static TimeSpan GetExpandDuration(DependencyObject d) => (TimeSpan)d.GetValue(ExpandDurationProperty);
        public static void SetExpandDuration(DependencyObject d, TimeSpan v) => d.SetValue(ExpandDurationProperty, v);

        // ---------------- FadeDuration ----------------
        public static readonly DependencyProperty FadeDurationProperty =
            DependencyProperty.RegisterAttached(
                "FadeDuration", typeof(TimeSpan), typeof(Ripple),
                new PropertyMetadata(TimeSpan.FromMilliseconds(300)));




        public static Thickness GetCornerRadius(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(DependencyObject obj, Thickness value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached("CornerRadius", typeof(Thickness), typeof(Ripple), new PropertyMetadata(new Thickness(0)));





        public static TimeSpan GetFadeDuration(DependencyObject d) => (TimeSpan)d.GetValue(FadeDurationProperty);
        public static void SetFadeDuration(DependencyObject d, TimeSpan v) => d.SetValue(FadeDurationProperty, v);

        #endregion

        #region 事件挂接 / 卸载

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not FrameworkElement element) return;

            if ((bool)e.NewValue)
            {
                element.PreviewMouseLeftButtonDown += OnMouseDown;
                element.PreviewMouseLeftButtonUp += OnMouseUp;
                element.MouseLeave += OnMouseLeave;
                element.Unloaded += OnUnloaded;
            }
            else
            {
                element.PreviewMouseLeftButtonDown -= OnMouseDown;
                element.PreviewMouseLeftButtonUp -= OnMouseUp;
                element.MouseLeave -= OnMouseLeave;
                element.Unloaded -= OnUnloaded;
                Detach(element);
            }
        }

        #endregion

        #region 鼠标事件

        private static void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is not UIElement element) return;

            var adorner = EnsureAdorner(element);
            if (adorner == null) return;

            var state = GetState(element);

            // 极少数情况下（如多指/异常序列）上一次涟漪还没释放，先让它淡出，避免"粘住"
            if (state.Active != null)
            {
                adorner.FadeOut(state.Active, new Duration(GetFadeDuration(element)));
                state.Active = null;
            }

            // 计算生成点
            Point origin = GetOrigin(element) == RippleOrigin.Center
                ? new Point(element.RenderSize.Width / 2.0, element.RenderSize.Height / 2.0)
                : e.GetPosition(element);
            var targetSize = new TargetControlSize
            {
                Width = element.RenderSize.Width,
                Height = element.RenderSize.Height,
                Radius = GetCornerRadius(element)
            };
            double opacity = GetMaxOpacity(element);
            if (opacity < 0) opacity = 0;
            if (opacity > 1) opacity = 1;
            // 按下 → 新建一个涟漪并扩散到最大（不自动消失）
            state.Active = adorner.CreateRipple(
                origin,
                targetSize,
                GetColor(element),
                opacity,
                new Duration(GetExpandDuration(element)));
        }

        private static void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is UIElement element) FadeActive(element);
        }

        private static void OnMouseLeave(object sender, MouseEventArgs e)
        {
            // 鼠标在控件外抬起时，控件会先丢捕获，这里兜底
            if (sender is UIElement element) FadeActive(element);
        }

        private static void OnUnloaded(object sender, RoutedEventArgs e)
        {
            if (sender is UIElement element) Detach(element);
        }

        #endregion

        #region 辅助

        /// <summary>抬起鼠标：让当前按下的涟漪慢慢消失</summary>
        private static void FadeActive(UIElement element)
        {
            if (!States.TryGetValue(element, out var state)) return;
            if (state.Active == null) return;

            state.Adorner?.FadeOut(state.Active, new Duration(GetFadeDuration(element)));
            state.Active = null;
        }

        /// <summary>取得（必要时创建）该控件上的装饰层</summary>
        private static RippleAdorner EnsureAdorner(UIElement element)
        {
            var state = GetState(element);
            if (state.Adorner != null) return state.Adorner;

            var layer = AdornerLayer.GetAdornerLayer(element);
            if (layer == null) return null;   // 例如放在没有 AdornerDecorator 的宿主里

            var adorner = new RippleAdorner(element);
            layer.Add(adorner);
            state.Adorner = adorner;
            return adorner;
        }

        /// <summary>移除装饰层，释放资源</summary>
        private static void Detach(UIElement element)
        {
            if (!States.TryGetValue(element, out var state)) return;

            if (state.Adorner != null)
            {
                AdornerLayer.GetAdornerLayer(element)?.Remove(state.Adorner);
                state.Adorner.Clear();
                state.Adorner = null;
            }
            state.Active = null;
        }

        #endregion
    }

    public struct TargetControlSize 
    {
        public double Height { get; set; }

        public double Width { get; set; }

        public Thickness Radius { get; set; }
    }
    public sealed class RippleAdorner : Adorner
    {
        private readonly VisualCollection _visuals;
        private readonly Canvas _canvas;

        public RippleAdorner(UIElement adornedElement) : base(adornedElement)
        {
            var frameElement= adornedElement as FrameworkElement;
            _visuals = new VisualCollection(this);
            _canvas = new Canvas
            {
                IsHitTestVisible = false,
                ClipToBounds = true
            };
            _visuals.Add(_canvas);
            IsHitTestVisible = false;
        }

        protected override int VisualChildrenCount => _visuals.Count;

        protected override Visual GetVisualChild(int index) => _visuals[index];

        protected override Size MeasureOverride(Size constraint)
        {
            _canvas.Measure(constraint);
            return constraint;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _canvas.Arrange(new Rect(finalSize));
            return finalSize;
        }

        public Ellipse CreateRipple(Point center,TargetControlSize targetControlSize,Color color, double maxOpacity, Duration duration)
        {
            Size size = AdornedElement.RenderSize;
            double radius = MaxDistance(center, size);
            double diameter = radius * 2;
            var retGeometry = new RectangleGeometry();
            retGeometry.Rect= new Rect(0, 0, targetControlSize.Width, targetControlSize.Height);
            retGeometry.RadiusX = targetControlSize.Radius.Top;
            retGeometry.RadiusY = targetControlSize.Radius.Right;
            _canvas.Clip = retGeometry;
            var brush = new SolidColorBrush(color);
            brush.Freeze();
            var ellipse = new Ellipse
            {
                Width = diameter,
                Height = diameter,
                Fill = brush,
                Opacity = maxOpacity,
                IsHitTestVisible = false,
                RenderTransformOrigin = new Point(0.5, 0.5),
                RenderTransform = new ScaleTransform(0, 0)
            };

            Canvas.SetLeft(ellipse, center.X - radius);
            Canvas.SetTop(ellipse, center.Y - radius);

            _canvas.Children.Add(ellipse);

            var scale = (ScaleTransform)ellipse.RenderTransform;
            var ease = new CubicEase { EasingMode = EasingMode.EaseOut };

            var ax = new DoubleAnimation(0, 1, duration) { EasingFunction = ease };
            var ay = new DoubleAnimation(0, 1, duration) { EasingFunction = ease };
            scale.BeginAnimation(ScaleTransform.ScaleXProperty, ax);
            scale.BeginAnimation(ScaleTransform.ScaleYProperty, ay);

            return ellipse;
        }

        public void FadeOut(Ellipse ellipse, Duration duration)
        {
            if (ellipse == null || !_canvas.Children.Contains(ellipse)) return;

            var fade = new DoubleAnimation
            {
                To = 0.0,
                Duration = duration,
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn },
                FillBehavior = FillBehavior.Stop
            };
            fade.Completed += (s, e) => _canvas.Children.Remove(ellipse);
            ellipse.BeginAnimation(UIElement.OpacityProperty, fade);
        }

        public void Clear()
        {
            _canvas.Children.Clear();
        }

        private static double MaxDistance(Point p, Size s)
        {
            double max = 0;
            double[] xs = { 0, s.Width };
            double[] ys = { 0, s.Height };
            foreach (var x in xs)
                foreach (var y in ys)
                {
                    double dx = p.X - x, dy = p.Y - y;
                    double d = Math.Sqrt(dx * dx + dy * dy);
                    if (d > max) max = d;
                }
            return max;
        }
    }
}
