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
    /// NetTestBlock.xaml 的交互逻辑
    /// </summary>
    public partial class NetTestBlock : UserControl
    {



        public Geometry Icon
        {
            get { return (Geometry)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(nameof(Icon), typeof(Geometry), typeof(NetTestBlock), new PropertyMetadata(null));


        public string TestName
        {
            get { return (string)GetValue(TestNameProperty); }
            set { SetValue(TestNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TestName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TestNameProperty =
            DependencyProperty.Register(nameof(TestName), typeof(string), typeof(NetTestBlock), new PropertyMetadata("",OnTestNameChanged));

        private static void OnTestNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as NetTestBlock;
            string res = e.NewValue?.ToString();
            if (string.IsNullOrWhiteSpace(res)) return;
            control.testName.Text= res;
        }

        public bool IsCurrentTest
        {
            get { return (bool)GetValue(IsCurrentTestProperty); }
            set { SetValue(IsCurrentTestProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsCurrentTest.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCurrentTestProperty =
            DependencyProperty.Register(nameof(IsCurrentTest), typeof(bool), typeof(NetTestBlock), new PropertyMetadata(false,OnIsCurrentTestChanged));

        private static void OnIsCurrentTestChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control=d as NetTestBlock;
            control.buttonTest.IsTesting = (bool)e.NewValue;
        }




        public string DelayMessage
        {
            get { return (string)GetValue(DelayMessageProperty); }
            set { SetValue(DelayMessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DelayMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DelayMessageProperty =
            DependencyProperty.Register(nameof(DelayMessage), typeof(string), typeof(NetTestBlock), new PropertyMetadata("",OnDelayMessageChanged));

        private static void OnDelayMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as NetTestBlock;
            control.buttonTest.Content = e.NewValue;
        }

        public NetTestBlock()
        {
            InitializeComponent();
            icon.Data = Icon;
        }
    }
}
