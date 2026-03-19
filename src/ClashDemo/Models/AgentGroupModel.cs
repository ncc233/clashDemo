using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
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


        public AgentGroupItemModel SelectedItem { set; get; }

        public ObservableCollection<AgentGroupItemModel> GroupItems { get; set; }

        [RelayCommand]
        private async Task FlashAllPoint() 
        {
            foreach (var item in GroupItems)
            {
                item.IsTesting=true;
            }
            await Task.Delay(1000 * 4);
            foreach (var item in GroupItems)
            {
                var random = new Random();
                item.IsTesting = false;
                item.Delay = random.Next(0,1000).ToString();
            }
        }
    }
}
