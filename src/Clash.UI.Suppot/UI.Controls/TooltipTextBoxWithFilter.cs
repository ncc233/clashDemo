using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Clash.UI.Suppot.UI.Controls
{
    public class TooltipTextBoxWithFilter:TextBox
    {



        public string BottomTipText
        {
            get { return (string)GetValue(BottomTipTextProperty); }
            set { SetValue(BottomTipTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BottomTipText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomTipTextProperty =
            DependencyProperty.Register(nameof(BottomTipText), typeof(string), typeof(TooltipTextBoxWithFilter), new PropertyMetadata(""));

        private string _textTooltip = string.Empty;

        public TooltipTextBoxWithFilter()
        {
            this.Loaded += TooltipTextBox_Loaded;
            this.Unloaded += (s, e) =>
            {
                Loaded -= TooltipTextBox_Loaded;
                TextChanged -= TooltipTextBox_TextChanged;
            };
        }

        private void TooltipTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            _textTooltip = BottomTipText;
            this.TextChanged += TooltipTextBox_TextChanged;
        }



        private void TooltipTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Text))
            {
                BottomTipText = _textTooltip;
            }
            else
            {
                BottomTipText = "";
            }
        }
    }
}
