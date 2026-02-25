using Clash.UI.Suppot.UI.Adorners;
using Clash.UI.Suppot.UI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Clash.UI.Suppot.UI.Componentes
{
    /// <summary>
    /// NetConfigButtonGroup.xaml 的交互逻辑
    /// </summary>
    public partial class NetConfigButtonGroup : UserControl
    {
        private ResourceDictionary _resource;
        /// <summary>
        /// 网络模式 0-未运行 1-系统代理 2-虚拟网卡模式 3-系统代理+虚拟网卡模式
        /// </summary>
        public short NetSettingMode
        {
            get { return (short)GetValue(NetSettingModeProperty); }
            set { SetValue(NetSettingModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NetSettingMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NetSettingModeProperty =
            DependencyProperty.Register(nameof(NetSettingMode), typeof(short), typeof(NetConfigButtonGroup), new PropertyMetadata((short)0));


        public NetConfigButtonGroup()
        {
            InitializeComponent();
            _resource= new ResourceDictionary()
            {
                Source = new Uri("pack://Application:,,,/Clash.UI.Suppot;component/UI.CommonResources/CommonGeometry.xaml")
            };
            this.Loaded += NetConfigButtonGroup_Loaded;
            systemRadioButton.IsChecked = true;
            CardRadioButton_Unchecked(null, null);
            CardAgentCheckBox_Unchecked(systemAgentCheckBox, null);
            CardAgentCheckBox_Unchecked(cardAgentCheckBox, null);
            detailText.Text = "系统代理已关闭, 建议大多数用户打开此选项";
        }

        private void NetConfigButtonGroup_Loaded(object sender, RoutedEventArgs e)
        {
            cardRadioButton.Checked += CardRadioButton_Checked;
            cardRadioButton.Unchecked += CardRadioButton_Unchecked;
            cardAgentCheckBox.Checked += CardAgentCheckBox_Checked;
            systemAgentCheckBox.Checked += CardAgentCheckBox_Checked;
            cardAgentCheckBox.Unchecked += CardAgentCheckBox_Unchecked;
            systemAgentCheckBox.Unchecked += CardAgentCheckBox_Unchecked;
            NetSettingMode = 1;

            //DescriptionTextChanged();
        }
        private void CardAgentCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var sen = sender as CheckBox;
            if (sen.Name == "cardAgentCheckBox")
            {
                cardAgentBorder.BeginAnimation(Border.OpacityProperty, CreateBorderBackgroundAnimation(false));
            }
            else
            {
                systemAgentBorder.BeginAnimation(Border.OpacityProperty, CreateBorderBackgroundAnimation(false));
            }
            DescriptionTextChanged();
        }



        private void CardAgentCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var sen = sender as CheckBox;
            if (sen.Name == "cardAgentCheckBox")
            {
                cardAgentBorder.BeginAnimation(Border.OpacityProperty, CreateBorderBackgroundAnimation(true));
            }
            else
            {
                systemAgentBorder.BeginAnimation(Border.OpacityProperty, CreateBorderBackgroundAnimation(true));
            }
            DescriptionTextChanged();
        }
        private DoubleAnimationBase CreateBorderBackgroundAnimation(bool isCheck)
        {
            var colorAnimation = new DoubleAnimation
            {
                To = isCheck ? 0.1 : 0,
                Duration = TimeSpan.FromMilliseconds(200)
            };
            return colorAnimation;
        }
        private void CardRadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            cardAgentCheckBox.Visibility = Visibility.Hidden;
            systemAgentCheckBox.Visibility = Visibility.Visible;
            cardAgentBorder.Visibility = Visibility.Hidden;
            systemAgentBorder.Visibility = Visibility.Visible;
            modeDescriptionText.Text = "系统代理";
            DescriptionTextChanged();
        }

        private void CardRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            cardAgentCheckBox.Visibility = Visibility.Visible;
            systemAgentCheckBox.Visibility = Visibility.Hidden;
            cardAgentBorder.Visibility = Visibility.Visible;
            systemAgentBorder.Visibility = Visibility.Hidden;
            modeDescriptionText.Text = "虚拟网卡模式";
            DescriptionTextChanged();
        }

        private void DescriptionTextChanged()
        {
            string res = "";
            string resMore = "";
            var brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#989b9e"));
            bool isSystemAgent = systemRadioButton.IsChecked == true;
            if (isSystemAgent)
            {
                detailText.Text = systemAgentCheckBox.IsChecked==true ? "系统代理已开启, 您的应用将通过代理的方式访问网络" : "系统代理已关闭, 建议大多数用户打开此选项";
                statuPath.Fill = systemAgentCheckBox.IsChecked == true ? Brushes.Green : brush;
                statuPath.Data = systemAgentCheckBox.IsChecked == true ?(Geometry) _resource["restartGeometry"] :(Geometry) _resource["suspendedGeometry"];
                NetSettingMode= systemAgentCheckBox.IsChecked== true ? (short)1 : (short)0;
                NetSettingMode = cardAgentCheckBox.IsChecked == true && NetSettingMode == 1 ? (short)3 : NetSettingMode;
            }
            else
            {
                detailText.Text = cardAgentCheckBox.IsChecked==true ? "TUN 模式已开启, 应用将通过虚拟网卡访问网络" : "TUN 模式已关闭, 适用于特殊应用";
                statuPath.Fill = cardAgentCheckBox.IsChecked == true ? Brushes.Green : brush;
                statuPath.Data = cardAgentCheckBox.IsChecked == true ? (Geometry)_resource["restartGeometry"] : (Geometry)_resource["suspendedGeometry"];
                NetSettingMode = cardAgentCheckBox.IsChecked == true ? (short)2 : (short)0;
                NetSettingMode = systemAgentCheckBox.IsChecked == true && NetSettingMode == 2 ? (short)3 : NetSettingMode;
            }


        }
    }
}
