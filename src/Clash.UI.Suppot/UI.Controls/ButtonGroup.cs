using Clash.UI.Suppot.UI.CommonResources.DefaultDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Clash.UI.Suppot.UI.Controls
{
    public class ButtonGroup:ItemsControl
    {
        protected override bool IsItemItsOwnContainerOverride(object item) => item is Button or RadioButton or ToggleButton;



        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(ButtonGroup), new PropertyMetadata(Orientation.Horizontal));



        public LinearLayout Layout
        {
            get { return (LinearLayout)GetValue(LayoutProperty); }
            set { SetValue(LayoutProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Layout.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LayoutProperty =
            DependencyProperty.Register(nameof(Layout), typeof(LinearLayout), typeof(ButtonGroup), new PropertyMetadata(LinearLayout.Uniform));


        protected override void OnRender(DrawingContext drawingContext)
        {
            var count = Items.Count;
            for (var i = 0; i < count; i++)
            {
                var item = (ButtonBase)Items[i];
                item.Style = ItemContainerStyleSelector?.SelectStyle(item, this);
            }
        }
    }
}
