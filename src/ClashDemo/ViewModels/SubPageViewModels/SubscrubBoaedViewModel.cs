using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashDemo.ViewModels.SubPageViewModels
{
    [INotifyPropertyChanged]
    public partial class SubscrubBoaedViewModel
    {
        public string OriginAddress { get; set; }
        public string UpdateTime { get; set; }
        public string CurrentCapacity { get; set; }

        public string FinishTime { get; set; }

        public double MaxCapacity { get; set; }
        public double MinCapacity { get; set; }

        public double UsedCapacity { get; set; }

        public SubscrubBoaedViewModel() 
        {
            OriginAddress = "来自:www.bilibili.com";
            UpdateTime = "更新时间:"+DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            CurrentCapacity = @"已使用 / 总量: 18.8GB / 768GB";
            FinishTime = DateTime.Now.AddMonths(6).ToString("yyyy-MM-dd");
            MaxCapacity = 768;
            MinCapacity = 0;
            UsedCapacity=18.8;
        }



    }
}
