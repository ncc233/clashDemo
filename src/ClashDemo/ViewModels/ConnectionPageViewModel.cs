using Bogus;
using ClashDemo.Interfaces;
using ClashDemo.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClashDemo.ViewModels
{
    [INotifyPropertyChanged]
    public partial class ConnectionPageViewModel : INavigationViewModel
    {
        public List<string> TestItems { get; set; }

        public ObservableCollection<ConnectionPageTestModel> Datas { get; set; }

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
            Datas = new ObservableCollection<ConnectionPageTestModel>();
            Randomizer.Seed = new Random(7758);

            var fake = new Faker("zh_CN");

        }
        public async Task<bool> NavigaedTo()
        {
            int useridstart = 1;
            var randomDatas = new Faker<ConnectionPageTestModel>()
               .StrictMode(true)
               .RuleFor(id => id.ID, f => useridstart++)
               .RuleFor(name => name.Name, f => f.Name.FullName())
               .RuleFor(u => u.Age, f => GetAge(f.Random.Number()) + 18)
               .RuleFor(u => u.Part, f => f.Part())
               .RuleFor(u => u.Salary, f => f.Random.Double(12000, 24000));
            foreach (var item in randomDatas.Generate(50))
            {
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    Datas.Add(item);
                });
                await Task.Delay(50);
            }
            return true;

        }


        private int GetAge(int value)
        {
            return (int)value switch
            {
                < 20 => 1,
                >= 20 and < 30 => 1,
                >= 30 and < 50 => 4,
                >= 50 and < 60 => 8,
                >= 60 and < 70 => 12,
                _ => 6
            };
        }


    }


    public static class bogusExtensions
    {
        public static string Part(this Faker faker)
        {
            return faker.PickRandom(new[] { "市场部", "人事部", "总经办", "宣传部", "研发部" });
        }
    }
}
