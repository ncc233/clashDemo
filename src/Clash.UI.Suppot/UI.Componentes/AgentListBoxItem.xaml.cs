using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Clash.UI.Suppot.UI.Componentes
{
    /// <summary>
    /// AgentListBoxItem.xaml 的交互逻辑
    /// </summary>
    public partial class AgentListBoxItem : UserControl
    {


        public bool IsTesting
        {
            get { return (bool)GetValue(IsTestingProperty); }
            set { SetValue(IsTestingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsTesting.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsTestingProperty =
            DependencyProperty.Register(nameof(IsTesting), typeof(bool), typeof(AgentListBoxItem), new PropertyMetadata(false, OnIsTestingChanged));

        private static void OnIsTestingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                if (d is AgentListBoxItem agentListBoxItem)
                {
                    agentListBoxItem.delayButton.Visibility = Visibility.Hidden;
                    agentListBoxItem.delayBorder.Visibility = Visibility.Hidden;
                    agentListBoxItem.delayAnimationControl.IsEnabled = true;


                }
            }
            else
            {
                if (d is AgentListBoxItem agentListBoxItem)
                {
                    agentListBoxItem.delayButton.Visibility = Visibility.Visible;
                    agentListBoxItem.delayBorder.IsEnabled = true;
                    agentListBoxItem.delayAnimationControl.IsEnabled = false;
                }
            }

        }

        public IEnumerable<string> AgentSource
        {
            get { return (IEnumerable<string>)GetValue(AgentSourceProperty); }
            set { SetValue(AgentSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AgentSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AgentSourceProperty =
            DependencyProperty.Register(nameof(AgentSource), typeof(IEnumerable<string>), typeof(AgentListBoxItem), new PropertyMetadata(null));



        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register(nameof(Header), typeof(string), typeof(AgentListBoxItem), new PropertyMetadata("自动选择"));



        public string SubHeader
        {
            get { return (string)GetValue(SubHeaderProperty); }
            set { SetValue(SubHeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SubHeader.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SubHeaderProperty =
            DependencyProperty.Register(nameof(SubHeader), typeof(string), typeof(AgentListBoxItem), new PropertyMetadata("Selector"));



        public object Delay
        {
            get { return (object)GetValue(DelayProperty); }
            set { SetValue(DelayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Delay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DelayProperty =
            DependencyProperty.Register(nameof(Delay), typeof(object), typeof(AgentListBoxItem), new PropertyMetadata("Check", OnDelayChanged));




        public ICommand SingleDelayTest
        {
            get { return (ICommand)GetValue(SingleDelayTestProperty); }
            set { SetValue(SingleDelayTestProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SingleDelayTest.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SingleDelayTestProperty =
            DependencyProperty.Register(nameof(SingleDelayTest), typeof(ICommand), typeof(AgentListBoxItem), new PropertyMetadata(null));




        private static void OnDelayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is AgentListBoxItem agentListBoxItem)
            {
                if (e.NewValue is string str)
                {
                    var btn = agentListBoxItem.delayButton;
                    btn.Content = str;
                    switch (str.ToUpper())
                    {
                        case "ERROR":
                            btn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff3b30"));//红
                            break;
                        case "CHECK":
                            btn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#007aff"));//蓝
                            break;
                        case "":
                            break;
                        default:
                            if (int.TryParse(str, out int res))
                            {
                                if (res < 200)
                                    btn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#06943d"));//绿
                                else if (200 <= res && res < 800)
                                    btn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#007aff"));//蓝
                                else
                                    btn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff990a"));//橙
                            }
                            break;
                    }
                }
            }
        }

        public AgentListBoxItem()
        {
            InitializeComponent();
            this.Loaded += AgentListBoxItem_Loaded;
        }

        private void AgentListBoxItem_Loaded(object sender, RoutedEventArgs e)
        {
            badgeSource.ItemsSource = AgentSource;
            header.Text = Header;
            subHeader.Text = SubHeader;
            delayButton.Content = Delay;
            delayButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#007aff"));//蓝
            delayButton.MouseEnter += DelayButton_MouseEnter;
            delayButton.MouseLeave += DelayButton_MouseLeave;
            Loaded += AgentListBoxItem_Loaded;
            delayButton.Command = SingleDelayTest;
        }

        private void DelayButton_MouseLeave(object sender, MouseEventArgs e)
        {
            this.delayBorder.Visibility = Visibility.Hidden;
        }

        private void DelayButton_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!IsTesting )
                this.delayBorder.Visibility = Visibility.Visible;
        }




    }
}
