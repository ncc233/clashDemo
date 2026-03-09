using System.Windows;
using System.Windows.Controls;

namespace Clash.UI.Suppot.UI.Controls
{
    /// <summary>
    /// 自定义Expander控件，扩展了头部文本、细节文本和计数显示。
    /// </summary>
    public class CustomExpander : Expander
    {
        static CustomExpander()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomExpander),
                new FrameworkPropertyMetadata(typeof(CustomExpander)));
        }

        /// <summary>
        /// 头部主标题
        /// </summary>
        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register(nameof(Header), typeof(string), typeof(CustomExpander),
                new PropertyMetadata(string.Empty));

        /// <summary>
        /// 成员计数（显示在最右侧）
        /// </summary>
        public int Count
        {
            get { return (int)GetValue(CountProperty); }
            set { SetValue(CountProperty, value); }
        }

        public static readonly DependencyProperty CountProperty =
            DependencyProperty.Register(nameof(Count), typeof(int), typeof(CustomExpander),
                new PropertyMetadata(0));

        /// <summary>
        /// 头部下方的细节文本
        /// </summary>
        public string Detail
        {
            get { return (string)GetValue(DetailProperty); }
            set { SetValue(DetailProperty, value); }
        }

        public static readonly DependencyProperty DetailProperty =
            DependencyProperty.Register(nameof(Detail), typeof(string), typeof(CustomExpander),
                new PropertyMetadata(string.Empty));



        public string ModeDescraption
        {
            get { return (string)GetValue(ModeDescraptionProperty); }   
            set { SetValue(ModeDescraptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ModeDescraption.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModeDescraptionProperty =
            DependencyProperty.Register(nameof(ModeDescraption), typeof(string), typeof(CustomExpander), new PropertyMetadata(""));


    }
}
