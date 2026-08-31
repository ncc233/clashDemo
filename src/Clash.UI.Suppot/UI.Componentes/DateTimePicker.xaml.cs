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
    /// DateTimePicker.xaml 的交互逻辑
    /// </summary>
    public partial class DateTimePicker : UserControl
    {
        public DateTimePicker()
        {
            InitializeComponent();
        }



        public DateTime HoverStart
        {
            get { return (DateTime)GetValue(HoverStartProperty); }
            set { SetValue(HoverStartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HoverStart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HoverStartProperty =
            DependencyProperty.Register(nameof(HoverStart), typeof(DateTime), typeof(DateTimePicker), new PropertyMetadata(null));



        public DateTime HoverEnd
        {
            get { return (DateTime)GetValue(HoverEndProperty); }
            set { SetValue(HoverEndProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HoverEnd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HoverEndProperty =
            DependencyProperty.Register(nameof(HoverEnd), typeof(DateTime), typeof(DateTimePicker), new PropertyMetadata(null));



        public DateTime DateTimeRangeStart
        {
            get { return (DateTime)GetValue(DateTimeRangeStartProperty); }
            set { SetValue(DateTimeRangeStartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DateTimeRangeStart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateTimeRangeStartProperty =
            DependencyProperty.Register(nameof(DateTimeRangeStart), typeof(DateTime), typeof(DateTimePicker), new PropertyMetadata(null));




        public DateTime DateTimeRangeEnd
        {
            get { return (DateTime)GetValue(DateTimeRangeEndProperty); }
            set { SetValue(DateTimeRangeEndProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DateTimeRangeEnd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateTimeRangeEndProperty =
            DependencyProperty.Register(nameof(DateTimeRangeEnd), typeof(DateTime), typeof(DateTimePicker), new PropertyMetadata(null));

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void startHours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void yesBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
