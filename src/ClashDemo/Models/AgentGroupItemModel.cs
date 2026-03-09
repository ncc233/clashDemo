using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClashDemo.Models
{
    [INotifyPropertyChanged]
    public partial class AgentGroupItemModel
    {
        public bool IsFirst { get; set; }
        public string ItemName { get; set; }
        public List<string> ItemMessages { get; set; }

        public bool IsTesting { get; set; }

        public string Delay { get; set; }

        public AgentGroupItemModel() 
        {
        }

        [RelayCommand]
        private async Task FlashDelay() 
        {
            IsTesting = true;
            await Task.Delay(2000);
            Delay = "333";
            IsTesting = false;

            
        }
    }
}
