using Clash.UI.Suppot.UI.Componentes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Clash.UI.Suppot.UI.Helpers
{
    public class ShadowdialogHelper
    {
        private static UIElementCollection _elementCollection;
        private static ShadowDialog _curentDialog;

        private ShadowdialogHelper() { }
        /// <summary>
        /// 运行隐形对话框
        /// </summary>
        /// <param name="parent">父类窗口</param>
        /// <param name="shadowContent">对话框内容</param>
        public static void RunDialog(Window parent, UserControl shadowContent)
        {
            int rowCount = 0, colCount = 0;
            int childCount = VisualTreeHelper.GetChildrenCount(parent);
            var child = FindVisualChildren<Grid>(parent).FirstOrDefault();
            if (child is Grid grid)
            {
                rowCount = grid.RowDefinitions.Count;
                colCount = grid.ColumnDefinitions.Count;
                _elementCollection = grid.Children;
            }
            else
            {
                throw new InvalidOperationException($"{parent.GetType().FullName}中不存在Grid控件!");
            }

            _curentDialog = new ShadowDialog();
            shadowContent.MouseLeftButtonUp += (s, e) => e.Handled = true;
            _curentDialog.ContentDialog.Content = shadowContent;
            if (colCount != 0)
                Grid.SetColumnSpan(_curentDialog, colCount);
            if (rowCount != 0)
                Grid.SetRowSpan(_curentDialog, rowCount);
            _curentDialog.MouseLeftButtonUp += UserControl_MouseLeftButtonUp;
            _elementCollection.Add(_curentDialog);
        }

        private static void UserControl_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _elementCollection?.Remove(_curentDialog);
        }

        public static void CloseDialog()
        {
            _elementCollection?.Remove(_curentDialog);
            _curentDialog.MouseLeftButtonUp -= UserControl_MouseLeftButtonUp;
        }
        /// <summary>
        /// 在可视化树中查找指定类型的所有子元素。
        /// </summary>
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null) yield break;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T typedChild)
                {
                    yield return typedChild;
                }
                // 递归查找更深层的子元素
                foreach (T childOfChild in FindVisualChildren<T>(child))
                {
                    yield return childOfChild;
                }
            }
        }
    }
}
