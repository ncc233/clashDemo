using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashDemo.ViewModels
{
    [INotifyPropertyChanged]
    public partial class ConnectionPageViewModel
    {
        public List<string> TestItems { get; set; }

        public ConnectionPageViewModel() 
        {
            TestItems = 
                [
                "123",
                "456",
                "789",
                "147",
                "258",
                "369",
                "741",
                "852",
                "963",
                "753",
                "159"

                ];
        }
    }
}
