using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace ClashDemo.Views
{
    /// <summary>
    /// TestingPage.xaml 的交互逻辑
    /// </summary>
    public partial class TestingPage : Page
    {
        private BitmapImage _bmp;
        public TestingPage()
        {
            InitializeComponent();
        }
        private void btnGray_Click(object sender, RoutedEventArgs e)
        {
            //_bmp = new BitmapImage(new Uri(@"E:\vsCode\Learning\wpf\ClashvergeUI\src\ClashDemo\Icons\Icons\128x128@2x.png", UriKind.RelativeOrAbsolute));
            _bmp = new BitmapImage(new Uri(@"Icons\128x128@2x.png", UriKind.RelativeOrAbsolute));
            img.Source = new FormatConvertedBitmap(_bmp, PixelFormats.Gray8, null, 0);
            
        }
        private void btnPixel_Click(object sender, RoutedEventArgs e)
        {
            var src = new FormatConvertedBitmap(_bmp, PixelFormats.Gray8, null, 0);
            var h = src.PixelHeight;    // 图像高度
            var w = src.PixelWidth;     // 图像宽度

            var rect=new Int32Rect(h/2, w/2, w/2, h/2);
            var stride=_bmp.Format.BitsPerPixel*rect.Width / 8;
            var pixels = new byte[h * stride];
            src.CopyPixels(rect,pixels, stride, 0);
            //pixels = pixels.Select(x => x != 255 ? x : (byte)0).ToArray();
            img.Source = BitmapSource.Create(w, h, 0, 0,
                PixelFormats.Indexed8, BitmapPalettes.Gray256, pixels, stride);
        }
    }
}
