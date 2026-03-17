using ClashDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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

namespace ClashDemo.Views
{
    /// <summary>
    /// ConnectionPage.xaml 的交互逻辑
    /// </summary>
    public partial class ConnectionPage : Page
    {
        public ConnectionPage(ConnectionPageViewModel vm)
        {
            InitializeComponent();
            this.DataContext=vm;

            var str = new FormattedText
                ("ab",
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                new Typeface(
                    new FontFamily("Times New Roma"),
                FontStyles.Normal,
                FontWeights.Medium,
                FontStretches.Normal),
                14,
                Brushes.Black
                );
            var geometry=str.BuildGeometry(new Point(0,0));
            var strGeometry=PathGeometry.CreateFromGeometry(geometry).ToString();
        }
    }
}
