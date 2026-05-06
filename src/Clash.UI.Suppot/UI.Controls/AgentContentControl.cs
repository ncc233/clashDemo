using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Clash.UI.Suppot.UI.Controls
{
    public class AgentContentControl : Control
    {

        /// <summary>
        /// 是否处于测试状态
        /// </summary>
        public bool IsTesting
        {
            get { return (bool)GetValue(IsTestingProperty); }
            set { SetValue(IsTestingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsTesting.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsTestingProperty =
            DependencyProperty.Register(nameof(IsTesting), typeof(bool), typeof(AgentContentControl), new PropertyMetadata(false));







        /// <summary>
        /// 延迟参数
        /// </summary>
        public object Delay
        {
            get { return (object)GetValue(DelayProperty); }
            set { SetValue(DelayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Delay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DelayProperty =
            DependencyProperty.Register(nameof(Delay), typeof(object), typeof(AgentContentControl), new PropertyMetadata("Check", OnDelayChanged));


        /// <summary>
        /// 标题
        /// </summary>
        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register(nameof(Header), typeof(string), typeof(AgentContentControl), new PropertyMetadata(""));





        /// <summary>
        /// 副标题
        /// </summary>
        public string SubHeader
        {
            get { return (string)GetValue(SubHeaderProperty); }
            set { SetValue(SubHeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SubHeader.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SubHeaderProperty =
            DependencyProperty.Register(nameof(SubHeader), typeof(string), typeof(AgentContentControl), new PropertyMetadata("Selector"));




        /// <summary>
        /// 代理类型集合
        /// </summary>
        public IEnumerable<string> AgentSource
        {
            get { return (IEnumerable<string>)GetValue(AgentSourceProperty); }
            set { SetValue(AgentSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AgentSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AgentSourceProperty =
            DependencyProperty.Register(nameof(AgentSource), typeof(IEnumerable<string>), typeof(AgentContentControl), new PropertyMetadata(null));





        /// <summary>
        /// 延迟测试
        /// </summary>
        public ICommand SingleDelayTest
        {
            get { return (ICommand)GetValue(SingleDelayTestProperty); }
            set { SetValue(SingleDelayTestProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SingleDelayTest.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SingleDelayTestProperty =
            DependencyProperty.Register(nameof(SingleDelayTest), typeof(ICommand), typeof(AgentContentControl), new PropertyMetadata(null));


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (Delay.ToString() != "Check")
                OnDelayChanged(this, new DependencyPropertyChangedEventArgs(AgentContentControl.DelayProperty, "", Delay));
        }

        private static void OnDelayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is AgentContentControl agentListBoxItem)
            {
                if (e.NewValue is string str)
                {

                    if (agentListBoxItem.Template == null) return;
                    Button btn = agentListBoxItem.Template.FindName("delayButton", agentListBoxItem) as Button;
                    //var btn = agentListBoxItem.delayButton;
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
    }
}
