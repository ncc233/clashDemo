using ClashDemo.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashDemo.ViewModels
{
    [INotifyPropertyChanged]
    public partial class SubscribPageViewModel:INavigationViewModel
    {
        public string LinkUrl { get; set; }

        public SubscribPageViewModel() 
        {

        }

        public async Task<bool> NavigaedTo()
        {
            await Task.Delay(200);
            return true;
        }
    }
}
