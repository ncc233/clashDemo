using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashDemo.Models
{
    [INotifyPropertyChanged]
    public partial class AgentGroupModel
    {
        public string GroupName { get; set; }

        public int ItemCount { get; set; }

        public string AgentSelectMode { get; set; }

        public string SelectModeDetial { get; set; }

        public ObservableCollection<AgentGroupItemModel> GroupItems { get; set; }
    }
}
