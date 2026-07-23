using ClashDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Reflection;
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

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            bool isShow = true;
            // 获取当前列的属性名和类型
            string propertyName = e.PropertyName;
            Type propertyType = ((PropertyDescriptor)e.PropertyDescriptor).PropertyType;

            // 创建 DataTemplate
            DataTemplate cellTemplate = new DataTemplate();
            FrameworkElementFactory textBlockFactory = new FrameworkElementFactory(typeof(TextBlock));

            // 设置绑定路径为当前属性名
            Binding binding = new Binding(propertyName);
            Thickness thickness = new Thickness();
            thickness.Right = 10;
            thickness.Left = 10;
            // 如果是 DateTime 类型，设置格式
            if (propertyType == typeof(DateTime))
            {
                binding.StringFormat = "yyyy-MM-dd HH:mm:ss";
                textBlockFactory.SetValue(TextBlock.ForegroundProperty, Brushes.Black);
                textBlockFactory.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                textBlockFactory.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
                textBlockFactory.SetValue(TextBlock.MarginProperty, thickness);
            }
            else
            {
                textBlockFactory.SetValue(TextBlock.ForegroundProperty, Brushes.Black);
                textBlockFactory.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                textBlockFactory.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
                textBlockFactory.SetValue(TextBlock.MarginProperty, thickness);
            }

            // 应用绑定到 TextBlock
            textBlockFactory.SetBinding(TextBlock.TextProperty, binding);

            // 将 TextBlock 添加到 DataTemplate 的视觉树
            cellTemplate.VisualTree = textBlockFactory;




            var propertyDescriptor = e.PropertyDescriptor as PropertyDescriptor;
            if (propertyDescriptor == null)
            {
                return; // Ensure the property descriptor is valid
            }
            var componentType = propertyDescriptor.ComponentType;

            // 获取当前属性的DisplayName特性
            System.Reflection.PropertyInfo? propertyInfo = componentType.GetProperty(e.PropertyName);
            var displayName = propertyInfo?.GetCustomAttribute(typeof(ColumnAttribute), true) as ColumnAttribute;


            // 创建并配置 DataGridTemplateColumn
            DataGridTemplateColumn templateColumn = new DataGridTemplateColumn
            {
                Header = displayName?.Name ?? e.Column.Header,
                CellTemplate = cellTemplate,
                SortMemberPath = propertyName, // 支持排序
                Visibility = isShow ? Visibility.Visible : Visibility.Collapsed,
            };

            // 替换自动生成的列
            e.Column = templateColumn;


            /*// 获取属性所属的组件类型
            var propertyDescriptor = e.PropertyDescriptor as PropertyDescriptor;
            if (propertyDescriptor == null)
            {
                return; // Ensure the property descriptor is valid
            }
            var componentType = propertyDescriptor.ComponentType;

            // 获取当前属性的DisplayName特性
            System.Reflection.PropertyInfo? propertyInfo = componentType.GetProperty(e.PropertyName);
            var displayName = propertyInfo?.GetCustomAttribute(typeof(TableViewMetaAttribute), true) as TableViewMetaAttribute;
            // 动态设置Header
            if (displayName != null)
            {
                e.Column.Header = displayName.Description;
            }*/
        }
    }
}
