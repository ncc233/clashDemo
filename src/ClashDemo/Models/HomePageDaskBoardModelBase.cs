using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashDemo.Models
{
    public class HomePageDaskBoardModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public string Name { get; set; }
        public bool IsEnable { get; set; }=true;    
    }
}
