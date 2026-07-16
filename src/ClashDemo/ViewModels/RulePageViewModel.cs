using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashDemo.ViewModels
{
    [INotifyPropertyChanged]
    public partial class RulePageViewModel
    {
        public DateTime Time { get; set; }

        public string TestValue { get; set; }=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
