using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Clash.UI.Suppot.UI.Controls
{
    /// <summary>
    /// 自定义ListBoxItem，扩展了头部、副头部、延迟和细节徽章集合属性。
    /// </summary>
    public class AgentListBoxItem : ListBoxItem
    {
        static AgentListBoxItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AgentListBoxItem),
                new FrameworkPropertyMetadata(typeof(AgentListBoxItem)));
        }

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(AgentListBoxItem),
                new PropertyMetadata(string.Empty));

        public string Subheader
        {
            get { return (string)GetValue(SubheaderProperty); }
            set { SetValue(SubheaderProperty, value); }
        }
        public static readonly DependencyProperty SubheaderProperty =
            DependencyProperty.Register("Subheader", typeof(string), typeof(AgentListBoxItem),
                new PropertyMetadata(string.Empty));

        public string Delay
        {
            get { return (string)GetValue(DelayProperty); }
            set { SetValue(DelayProperty, value); }
        }
        public static readonly DependencyProperty DelayProperty =
            DependencyProperty.Register("Delay", typeof(string), typeof(AgentListBoxItem),
                new PropertyMetadata(string.Empty));

        public IEnumerable<string> Detail
        {
            get { return (List<string>)GetValue(DetailProperty); }
            set { SetValue(DetailProperty, value); }
        }
        public static readonly DependencyProperty DetailProperty =
            DependencyProperty.Register("Detail", typeof(IEnumerable<string>), typeof(AgentListBoxItem),
                new PropertyMetadata(null));



        public bool IsTesting
        {
            get { return (bool)GetValue(IsTestingProperty); }
            set { SetValue(IsTestingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsTesting.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsTestingProperty =
            DependencyProperty.Register(nameof(IsTesting), typeof(bool), typeof(AgentListBoxItem), new PropertyMetadata(false));



    }
}