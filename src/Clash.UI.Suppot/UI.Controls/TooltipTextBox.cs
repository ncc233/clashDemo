using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Clash.UI.Suppot.UI.Controls
{
    public class TooltipTextBox:TextBox
    {



        public string BottomTipText
        {
            get { return (string)GetValue(BottomTipTextProperty); }
            set { SetValue(BottomTipTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BottomTipText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomTipTextProperty =
            DependencyProperty.Register(nameof(BottomTipText), typeof(string), typeof(TooltipTextBox), new PropertyMetadata(""));

        private string _textTooltip=string.Empty;

        private Button _clipButton;
        private Button _clearButton;
        public TooltipTextBox() 
        {
            this.Loaded += TooltipTextBox_Loaded;
            this.Unloaded += (s, e) =>
            {
                Loaded-=TooltipTextBox_Loaded;
                TextChanged-= TooltipTextBox_TextChanged;
                _clipButton.Click -= _clipButton_Click;
                _clearButton.Click -= _clearButton_Click;
            };
        }

        private void TooltipTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            _textTooltip = BottomTipText;
            this.TextChanged += TooltipTextBox_TextChanged;
            if (sender is TooltipTextBox box) 
            {
                _clipButton = (Button)box.Template.FindName("PART_clipButton",box);
                _clearButton = (Button)box.Template.FindName("PART_clearButton", box);

                _clipButton.Click += _clipButton_Click;
                _clearButton.Click += _clearButton_Click;
            }
        }

        private void _clearButton_Click(object sender, RoutedEventArgs e)
        {
            Text = string.Empty;
        }

        private void _clipButton_Click(object sender, RoutedEventArgs e)
        {
            Text=Clipboard.GetText();
        }

        private void TooltipTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Text))
            {
                BottomTipText = _textTooltip;
                _clearButton.Visibility = Visibility.Collapsed;
                _clipButton.Visibility = Visibility.Visible;
            }
            else 
            {
                BottomTipText = "";
                _clearButton.Visibility = Visibility.Visible;
                _clipButton.Visibility = Visibility.Collapsed;
            }
        }
    }
}
