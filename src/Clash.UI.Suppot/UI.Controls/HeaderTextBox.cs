using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shell;

namespace Clash.UI.Suppot.UI.Controls
{
    public class HeaderTextBox : TextBox
    {
        //  字体变化率 1.5247  d/x
        //  padding 0.8519  z/b
        //35 0
        //23 27  
        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register(nameof(Header), typeof(string), typeof(HeaderTextBox), new PropertyMetadata(""));


        private TextBlock txt;
        private Border border;
        public HeaderTextBox()
        {
            this.Loaded += (s, e) =>
            {
                txt = this.Template.FindName("moveText", this) as TextBlock;
                border = this.Template.FindName("Header", this) as Border;
                IniTextBox(s);
            };
            this.GotFocus += (sender, e) =>
            {
                //28.3132
                if (!string.IsNullOrWhiteSpace(this.Text)) return;
                var hei = txt.ActualHeight;
                var wid = txt.ActualWidth;
                var minWidth = wid * 0.67;
                var padding = minWidth / 0.8209 ;
                var margin = new Thickness(0, 0, padding, 0);
                //CreateAnimation(margin, 12, -20, padding/2 * 0.112).Begin();
                CreateAnimation(margin, 12, -20, (padding-minWidth)/2-2/0.8209).Begin();
               

            };
            this.LostFocus += (sender, e) =>
            {
                if (!string.IsNullOrWhiteSpace(this.Text)) return;
                var margin = new Thickness(0, 0, 0, 0);
                CreateAnimation(margin, 18, 0, 0).Begin();
            };

        }

        public void IniTextBox(object sender) 
        {
            if (!string.IsNullOrWhiteSpace(this.Text))
            {
                var hei = txt.ActualHeight;
                var wid = txt.ActualWidth;
                var minWidth = wid * 0.67;
                var padding = minWidth / 0.8209;
                var margin = new Thickness(0, 0, padding, 0);
                //CreateAnimation(margin, 12, -20, padding/2 * 0.112).Begin();
                CreateAnimation(margin, 12, -20, (padding - minWidth) / 2 - 2 / 0.8209).Begin();
            }
        }

        private Storyboard CreateAnimation(Thickness padding, double fontsize, double translateY, double translateX)
        {
            var animationBoard = new Storyboard();
            var thicknessAnimation = new ThicknessAnimation()
            {
                Duration = TimeSpan.FromSeconds(0.2),
                To = padding
            };
            Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath("Padding"));
            Storyboard.SetTarget(thicknessAnimation, border);


            var yAnimation = new DoubleAnimation()
            {
                Duration = TimeSpan.FromSeconds(0.2),
                To = translateY
            };
            Storyboard.SetTargetProperty(yAnimation, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.Y)"));
            Storyboard.SetTarget(yAnimation, txt);

            var xAnimation = new DoubleAnimation()
            {
                Duration = TimeSpan.FromSeconds(0.2),
                To = translateX
            };
            Storyboard.SetTargetProperty(xAnimation, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));
            Storyboard.SetTarget(xAnimation, txt);

            var fontSizeAnimation = new DoubleAnimation()
            {
                Duration = TimeSpan.FromSeconds(0.2),
                To = fontsize
            };
            Storyboard.SetTargetProperty(fontSizeAnimation, new PropertyPath("FontSize"));
            Storyboard.SetTarget(fontSizeAnimation, txt);

            animationBoard.Children.Add(yAnimation);
            animationBoard.Children.Add(xAnimation);
            animationBoard.Children.Add(fontSizeAnimation);
            animationBoard.Children.Add(thicknessAnimation);

            return animationBoard;
        }
    }
}
