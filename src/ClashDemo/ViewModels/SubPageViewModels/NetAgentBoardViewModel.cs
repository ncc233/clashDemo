using Clash.UI.Suppot.UI.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashDemo.ViewModels.SubPageViewModels
{
    [INotifyPropertyChanged]
    public partial class NetAgentBoardViewModel
    {
        public ObservableCollection<NetAgentComboBoxItemModel> Items { get; } =
        new()
        {
            new() { Name = "香港02  (1x)", Count = 47 },
            new() { Name = "香港09  (1x)", Count = 48 },
            new() { Name = "香港05  (1x)", Count = 54 },
            new() { Name = "香港03  (1x)", Count = 55 },
            new() { Name = "香港07  (1x)", Count = 65 },
        };

        public NetAgentComboBoxItemModel SelectedItem { get; set; }
    }
}
