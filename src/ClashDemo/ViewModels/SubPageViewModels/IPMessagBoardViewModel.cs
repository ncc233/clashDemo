using ClashDemo.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashDemo.ViewModels.SubPageViewModels
{
    [INotifyPropertyChanged]
    public partial class IPMessagBoardViewModel
    {
        public string BoardStatu { get; set; } = "Flashed";

        public IPMessagBoardViewModel() 
        { 
        }
        [RelayCommand]
        private async Task FlashBoard()
        {
            BoardStatu = "Flash";
            await Task.Delay(1000*5);
            BoardStatu = "Flashed";
        }
    }
}
