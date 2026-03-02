using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashDemo.Models
{
    [INotifyPropertyChanged]
    public partial class NetTestBlockModel
    {
        public string BlockName { get; set; }

        public string BlockURL { get; set; }

        public int NetDelay { get; set; }

        public bool IsTesting { get; set; } = false;
    }
}
