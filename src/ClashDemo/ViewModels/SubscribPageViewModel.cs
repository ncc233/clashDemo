using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashDemo.ViewModels
{
    [INotifyPropertyChanged]
    public partial class SubscribPageViewModel
    {
        public string LinkUrl { get; set; }

        public SubscribPageViewModel() 
        {

        }
    }
}
