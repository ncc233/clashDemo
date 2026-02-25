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
    /// AgentModeGroup.xaml 的交互逻辑
    /// </summary>
    public partial class AgentModeGroup : UserControl
    {


        public int AgentMode
        {
            get { return (int)GetValue(AgentModeProperty); }
            set { SetValue(AgentModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AgentMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AgentModeProperty =
            DependencyProperty.Register(nameof(AgentMode), typeof(int), typeof(AgentModeGroup), new PropertyMetadata(0));


        public AgentModeGroup()
        {
            InitializeComponent();
            this.Loaded += AgentModeGroup_Loaded;
        }

        private void AgentModeGroup_Loaded(object sender, RoutedEventArgs e)
        {
            ruleRadio.Checked += Radio_Checked;
            directlyRadio.Checked += Radio_Checked;
            globalRadio.Checked += Radio_Checked;
            ruleRadio.IsChecked = true;
        }

        private void Radio_Checked(object sender, RoutedEventArgs e)
        {
            var radio=sender as FrameworkElement;
            if (radio == ruleRadio)
            {
                AgentMode = 0;

            }
            else if (radio == directlyRadio)
            {
                AgentMode = 1;
            }
            else if (radio == globalRadio)
            {
                AgentMode = 2;
            }
            detailText.Text = AgentMode switch
            {
                0 => "基于预设规则智绊判断流量走向，提供灵活的代理策略",
                1 => "所有流量不经过代理节点，但经过Clash内核转发连接目标服务器，适用于需要通过内核进行分流的特定场景",
                2 => "所有流量均通过代理服务器，适用于需要全局科学上网的场景",
                _ => detailText.Text
            };
        }
    }
}
