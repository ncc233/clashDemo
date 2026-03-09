using Clash.UI.Suppot.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Clash.UI.Suppot.UI.Helpers.TemplateSelecters
{
    public class ButtonGroupItemStyleSelector:StyleSelector
    {
        private static readonly Dictionary<string, Style> StyleDict = new()
        {
            ["RadioGroupItemSingle"] = GetResourceDictionary()["RadioGroupItemSingle"] as Style,
            ["RadioGroupItemHorizontalFirst"] = GetResourceDictionary()["RadioGroupItemHorizontalFirst"] as Style,
            ["RadioGroupItemHorizontalLast"] = GetResourceDictionary()["RadioGroupItemHorizontalLast"] as Style,
            ["toggleButtonGroupItemSingle"] = GetResourceDictionary()["toggleButtonGroupItemSingle"] as Style,
        };


        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (container is ButtonGroup buttonGroup && item is ButtonBase buttonBase)
            {
                var count = GetVisibleButtonsCount(buttonGroup);

                switch (buttonBase)
                {
                    case RadioButton: return GetRadioButtonStyle(count, buttonGroup, buttonBase);
                    //case Button: return GetButtonStyle(count, buttonGroup, buttonBase);
                    case ToggleButton: return GetToggleButtonStyle(count, buttonGroup, buttonBase);
                }
            }

            return null;
        }

        //private static Style GetButtonStyle(int count, ButtonGroup buttonGroup, ButtonBase button)
        //{
        //    if (count == 1)
        //    {
        //        return StyleDict[ResourceToken.ButtonGroupItemSingle];
        //    }

        //    var index = buttonGroup.Items.IndexOf(button);
        //    return buttonGroup.Orientation == Orientation.Horizontal
        //        ? index == 0
        //            ? StyleDict[ResourceToken.ButtonGroupItemHorizontalFirst]
        //            : StyleDict[index == count - 1
        //                ? ResourceToken.ButtonGroupItemHorizontalLast
        //                : ResourceToken.ButtonGroupItemDefault]
        //        : index == 0
        //            ? StyleDict[ResourceToken.ButtonGroupItemVerticalFirst]
        //            : StyleDict[index == count - 1
        //                ? ResourceToken.ButtonGroupItemVerticalLast
        //                : ResourceToken.ButtonGroupItemDefault];
        //}

        private static int GetVisibleButtonsCount(ButtonGroup buttonGroup)
        {
            return buttonGroup.Items.OfType<ButtonBase>().Count(button => button.IsVisible);
        }

        private static ResourceDictionary GetResourceDictionary() => new ResourceDictionary() 
        {
            Source=new Uri("/Clash.UI.Suppot;component/UI.Styles/ButtonGroupItemStyle.xaml", UriKind.Relative)
        };
        private static Style GetRadioButtonStyle(int count, ButtonGroup buttonGroup, ButtonBase button)
        {
            if (count == 1)
            {
                return StyleDict["RadioGroupItemSingle"];
            }

            var index = buttonGroup.Items.IndexOf(button);
            return index == 0
                    ? StyleDict["RadioGroupItemHorizontalFirst"]
                    : StyleDict[index == count - 1
                        ? "RadioGroupItemHorizontalLast"
                        : "RadioGroupItemSingle"];
        }
        private static Style GetToggleButtonStyle(int count, ButtonGroup buttonGroup, ButtonBase button)
        {
            if (count == 1)
            {
                return StyleDict["toggleButtonGroupItemSingle"];
            }

            var index = buttonGroup.Items.IndexOf(button);
            return index == 0
                    ? StyleDict["toggleButtonGroupItemSingle"]
                    : StyleDict[index == count - 1
                        ? "toggleButtonGroupItemSingle"
                        : "toggleButtonGroupItemSingle"];
        }
    }
}
