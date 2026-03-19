using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;


namespace Clash.UI.Suppot.UI.Adorners
{
    public class RippleAnimationAdorner : Adorner
    {
        private readonly Grid _container;

        public RippleAnimationAdorner(UIElement adornedElement) : base(adornedElement)
        {
            _container = new Grid
            {
                ClipToBounds = true,
                IsHitTestVisible = false,
            };

            // 添加到视觉树
            AddVisualChild(_container);
        }


        /// <summary>
        /// 添加动画
        /// </summary>
        /// <param name="element"></param>
        /// <param name="brush"></param>
        /// <param name="radius"></param>
        /// <param name="time"></param>
        /// <param name="isCenter"></param>
        public void AddAnimation(FrameworkElement element, Brush brush, double radius = 4, double time = 0.4, bool isCenter = false)
        {
            var rect = new Rect(0, 0, element.ActualWidth, ActualHeight);
            _container.Clip = new RectangleGeometry()
            {
                Rect = rect,
                RadiusX = radius,
                RadiusY = radius
            };
            var center = isCenter ? new Point(_container.ActualWidth / 2, _container.ActualHeight / 2) : Mouse.GetPosition(element);
            var storyboard = new Storyboard();
            var animationSize = Math.Max(element.ActualWidth, element.ActualHeight);
            var ellipse = new Path();
            ellipse.Data = new EllipseGeometry
            {
                Center = center,
                RadiusX = 0,
                RadiusY = 0,

            };
            ellipse.Fill = brush;
            _container.Children.Add(ellipse);


            var func = new QuarticEase 
            {
                EasingMode = EasingMode.EaseInOut,
            };
            // x动画
            DoubleAnimation aniX = new DoubleAnimation();
            aniX.Duration = TimeSpan.FromSeconds(time);
            aniX.From = 0;
            aniX.To = animationSize;
            aniX.EasingFunction = func;
            //var kfRadiusX = new DoubleAnimationUsingKeyFrames();
            //kfRadiusX.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.Zero)));
            //kfRadiusX.KeyFrames.Add(new LinearDoubleKeyFrame(animationSize * 0.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(time * 0.5))));
            //kfRadiusX.KeyFrames.Add(new LinearDoubleKeyFrame(animationSize * 0.8, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(time * 0.8))));
            //kfRadiusX.KeyFrames.Add(new LinearDoubleKeyFrame(animationSize, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(time))));


            // y动画
            DoubleAnimation aniY = new DoubleAnimation();
            aniY.Duration = TimeSpan.FromSeconds(time);
            aniY.From = 0;
            aniY.To = animationSize;
            aniY.EasingFunction = func;
            //var radiusY = new DoubleAnimation();
            //var kfRadiusY = new DoubleAnimationUsingKeyFrames();
            //kfRadiusY.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.Zero)));
            //kfRadiusY.KeyFrames.Add(new LinearDoubleKeyFrame(animationSize * 0.5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(time * 0.5))));
            //kfRadiusY.KeyFrames.Add(new LinearDoubleKeyFrame(animationSize * 0.8, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(time * 0.8))));
            //kfRadiusY.KeyFrames.Add(new LinearDoubleKeyFrame(animationSize, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(time))));



            // 透明度动画
            var opacityAni = new DoubleAnimation();
            var kfOpacity = new DoubleAnimationUsingKeyFrames();
            kfOpacity.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.Zero)));
            kfOpacity.KeyFrames.Add(new LinearDoubleKeyFrame(0.4 * 0.7, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(time * 0.5))));
            kfOpacity.KeyFrames.Add(new LinearDoubleKeyFrame(0.4 * 0.9, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(time * 0.8))));
            kfOpacity.KeyFrames.Add(new LinearDoubleKeyFrame(0.4, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(time))));



            ellipse.Data.BeginAnimation(EllipseGeometry.RadiusXProperty, aniX);
            ellipse.Data.BeginAnimation(EllipseGeometry.RadiusYProperty, aniY);
            ellipse.BeginAnimation(Path.OpacityProperty, kfOpacity);


        }

        private void KeepAnimationCount()
        {
            var paths = _container.Children.Cast<UIElement>().ToList();
            paths.RemoveAll(x => x.Opacity == 0);
        }

        public void OpacitiesAnimation(double time = 0.4)
        {
            var grid = _container;
            foreach (var chiled in grid.Children)
            {
                if (chiled is System.Windows.Shapes.Path path)
                {
                    path.BeginAnimation(System.Windows.Shapes.Path.OpacityProperty, new DoubleAnimation
                    {
                        To = 0,
                        Duration = TimeSpan.FromSeconds(time),
                    });
                }
            }
            KeepAnimationCount();
        }


        protected override Size MeasureOverride(Size constraint)
        {
            _container.Measure(constraint);
            return base.MeasureOverride(constraint);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _container.Arrange(new Rect(finalSize));
            return base.ArrangeOverride(finalSize);
        }

        protected override int VisualChildrenCount => 1;

        protected override Visual GetVisualChild(int index)
        {
            if (index != 0) throw new ArgumentOutOfRangeException(nameof(index));
            return _container;
        }
    }
}
