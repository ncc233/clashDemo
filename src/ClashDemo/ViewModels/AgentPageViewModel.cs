using ClashDemo.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashDemo.ViewModels
{
    [INotifyPropertyChanged]
    public partial class AgentPageViewModel
    {
        public List<AgentGroupModel> AgentGroups { get; set; }

        public AgentPageViewModel() 
        {
            AgentGroups = new List<AgentGroupModel>()
            {
                new AgentGroupModel()
                {
                    GroupName="魔法喵",
                    ItemCount = 4,
                    AgentSelectMode="Selector",
                    GroupItems =
                [
                    new()
                    {
                        ItemName="自动选择",
                        ItemMessages=
                        [
                            "Vless",
                            "UDP"
                            ],
                        Delay="Check"
                     },
                    new()
                    {
                        ItemName="自动选择",
                        ItemMessages=
                        [
                            "Vless",
                            "UDP"
                            ],
                        Delay="Check"
                     },
                    new()
                    {
                        ItemName="自动选择",
                        ItemMessages=
                        [
                            "Vless",
                            "UDP"
                            ],
                        Delay="Check"
                     },
                    new()
                    {
                        ItemName="自动选择",
                        ItemMessages=
                        [
                            "Vless",
                            "UDP"
                            ],
                        Delay="Check"
                     },
                    new()
                    {
                        ItemName="自动选择",
                        ItemMessages=
                        [
                            "Vless",
                            "UDP"
                            ],
                        Delay="Check"
                     },] 
                },
                    new AgentGroupModel()
                {
                    GroupName="自动选择",
                    AgentSelectMode="URLTest",
                    ItemCount = 4,
                    GroupItems =
                [
                    new()
                    {
                        ItemName="自动选择",
                        ItemMessages=
                        [
                            "Vless",
                            "UDP"
                            ],
                        Delay="Check"
                     },
                    new()
                    {
                        ItemName="自动选择",
                        ItemMessages=
                        [
                            "Vless",
                            "UDP"
                            ],
                        Delay="Check"
                     },
                    new()
                    {
                        ItemName="自动选择",
                        ItemMessages=
                        [
                            "Vless",
                            "UDP"
                            ],
                        Delay="Check"
                     },
                    new()
                    {
                        ItemName="自动选择",
                        ItemMessages=
                        [
                            "Vless",
                            "UDP"
                            ],
                        Delay="Check"
                     },
                    new()
                    {
                        ItemName="自动选择",
                        ItemMessages=
                        [
                            "Vless",
                            "UDP"
                            ],
                        Delay="Check"
                     },]
                }
                    ,
                    new AgentGroupModel()
                {
                    GroupName="故障转移",
                    AgentSelectMode="Fallback",
                    ItemCount = 4,
                    GroupItems =
                [
                    new()
                    {
                        ItemName="自动选择",
                        ItemMessages=
                        [
                            "Vless",
                            "UDP"
                            ],
                        Delay="Check"
                     },
                    new()
                    {
                        ItemName="自动选择",
                        ItemMessages=
                        [
                            "Vless",
                            "UDP"
                            ],
                        Delay="Check"
                     },
                    new()
                    {
                        ItemName="自动选择",
                        ItemMessages=
                        [
                            "Vless",
                            "UDP"
                            ],
                        Delay="Check"
                     },
                    new()
                    {
                        ItemName="自动选择",
                        ItemMessages=
                        [
                            "Vless",
                            "UDP"
                            ],
                        Delay="Check"
                     },
                    new()
                    {
                        ItemName="自动选择",
                        ItemMessages=
                        [
                            "Vless",
                            "UDP"
                            ],
                        Delay="Check"
                     },]
                }
            };
        }
    }
}
