using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Clash.UI.Suppot.UI.Controls
{
    public class DelayTestButton : Button
    {




        public bool IsTesting
        {
            get { return (bool)GetValue(IsTestingProperty); }
            set { SetValue(IsTestingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsTesting.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsTestingProperty =
            DependencyProperty.Register(nameof(IsTesting), typeof(bool), typeof(DelayTestButton), new PropertyMetadata(false));


        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            if (int.TryParse(newContent.ToString(), out int res))
            {
                Brush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(res > 500 ? "#ff9529" : "#007aff"));
                this.Foreground = brush;
            }
        }

        protected override void OnClick()
        {
            base.OnClick();
            IsTesting = true;
            Task.Run(async () =>
            {
                await Task.Delay(2000);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    IsTesting = false;
                });

            });
        }
    }
}
