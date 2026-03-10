using ClashDemo.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace ClashDemo.ViewModels
{
    [INotifyPropertyChanged]
    public partial class AgentPageViewModel
    {
        public List<AgentGroupModel> AgentGroups { get; set; }

        public AgentPageViewModel() 
        {
            List<string> countries = ["香港", "美国", "日本", "英国", "新加坡", "韩国", "马来西亚", "乌克兰"];
            var listCountries = new Dictionary<string, int>();
            countries.ForEach(item => 
            {
                listCountries[item] = 0;
            });

            var collecton = new ObservableCollection<AgentGroupItemModel>();
            var countryRandom = new Random();
            Enumerable.Range(0, 33).ToList().ForEach(item => 
            {
                var country=countries[countryRandom.Next(0, countries.Count)];
                listCountries[country] = ++listCountries[country];
                string res = country + listCountries[country];
                collecton.Add(new AgentGroupItemModel 
                {
                    ItemName=res,
                    Delay="Check",
                    ItemMessages = ["Vless","UDP"]
                });
            });
            AgentGroups = new List<AgentGroupModel>()
            {
                new AgentGroupModel()
                {
                    GroupName="魔法喵",
                    ItemCount = collecton.Count,
                    AgentSelectMode="Selector",
                    GroupItems=collecton
                },
                new AgentGroupModel()
                {
                    GroupName="自动选择",
                    ItemCount = collecton.Count,
                    AgentSelectMode="Selector",
                    GroupItems=collecton
                },
                new AgentGroupModel()
                {
                    GroupName="故障转移",
                    ItemCount = collecton.Count,
                    AgentSelectMode="Selector",
                    GroupItems=collecton
                },

            };
        }
    }
}
