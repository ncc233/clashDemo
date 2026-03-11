using Clash.UI.Suppot.UI.Componentes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
        /// <param name="uIElementCollection">父类容器子集</param>
        /// <param name="userControl">展示对话框</param>
        /// <param name="gridSpan"></param>
        public static void RunDialog(UIElementCollection uIElementCollection,ShadowDialog shadowDialog,UserControl userControl,int gridSpan=3) 
        {
            _curentDialog = shadowDialog;
            userControl.MouseLeftButtonUp += (s, e) =>e.Handled = true;
            _curentDialog.ContentDialog.Content = userControl;
            _elementCollection = uIElementCollection;
            Grid.SetColumnSpan(_curentDialog, gridSpan);
            Grid.SetRowSpan(_curentDialog, gridSpan);
            _curentDialog.MouseLeftButtonUp += UserControl_MouseLeftButtonUp;
            _elementCollection.Add(_curentDialog);
        }

        private static void UserControl_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _elementCollection?.Remove(_curentDialog);
        }

        public static void CloseDialog() 
        {
            _curentDialog.MouseLeftButtonUp -= UserControl_MouseLeftButtonUp;
            UserControl_MouseLeftButtonUp(null,null);
        }
    }
}
