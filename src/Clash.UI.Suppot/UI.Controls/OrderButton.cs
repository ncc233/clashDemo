using Clash.UI.Suppot.UI.CommonResources.DefaultDefinition;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Clash.UI.Suppot.UI.Controls
{
    public class OrderButton : Button
    {
        [Description]
        private Dictionary<EnumOrderUnit, (Geometry geometry,string description)> geometries = new Dictionary<EnumOrderUnit, (Geometry,string)>();

        public  EnumOrderUnit CurrentStatu
        {
            get { return (EnumOrderUnit)GetValue(CurrentStatuProperty); }
            private set { SetValue(CurrentStatuProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentStatu.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentStatuProperty =
            DependencyProperty.Register(nameof(CurrentStatu), typeof(EnumOrderUnit), typeof(OrderButton), new PropertyMetadata(EnumOrderUnit.ASC));
        public OrderButton()
        {
            ResourceDictionary rsd=new ResourceDictionary();
            ResourceDictionary rsd2=new ResourceDictionary();
            
            rsd.Source = new Uri(@"/Clash.UI.Suppot;component/UI.CommonResources/CommonGeometry.xaml",UriKind.RelativeOrAbsolute);
            rsd2.Source = new Uri(@"/Clash.UI.Suppot;component/UI.Styles/TooltipStyle.xaml", UriKind.RelativeOrAbsolute);
            var data=rsd["descendingAlphabetOrder"];
            var data1=rsd["alphabeticalOrder"];
            var data2=rsd["delayOrder"];


            geometries[EnumOrderUnit.ASC] = ((GeometryGroup)data1, "按名称升序");
            geometries[EnumOrderUnit.DESC] = ((GeometryGroup)data, "按名称降序");
            geometries[EnumOrderUnit.Delay] = ((Geometry)data2,"按延迟排序");
            var style=(Style)rsd2["RectToolTip"];
            var tooltip = new ToolTip();
            tooltip.Style = style;
            tooltip.Content = geometries[EnumOrderUnit.ASC].description;
            this.ToolTip = tooltip;
            Content = data1;
        }

        protected override void OnClick()
        {
            base.OnClick();
            CurrentStatu = CurrentStatu == 0 ? (EnumOrderUnit)1 : CurrentStatu == (EnumOrderUnit)1 ? (EnumOrderUnit)2 : 0 ;
            Content=geometries[CurrentStatu].geometry;
            var tooltip = (ToolTip)this.ToolTip;
            tooltip.Content= geometries[CurrentStatu].description;


        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
        }

    }

}
