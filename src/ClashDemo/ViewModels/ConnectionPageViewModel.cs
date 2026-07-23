using Bogus;
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
    public partial class ConnectionPageViewModel
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

            Randomizer.Seed = new Random(7758);
            int useridstart = 1;
            var fake = new Faker("zh_CN");
            var randomDatas = new Faker<ConnectionPageTestModel>()
                .StrictMode(true)
                .RuleFor(id => id.ID, f => useridstart++)
                .RuleFor(name => name.Name, f => f.Name.FullName())
                .RuleFor(u => u.Age, f => GetAge(f.Random.Number()) + 18)
                .RuleFor(u => u.Part, f => f.Part())
                .RuleFor(u => u.Salary, f =>f.Random.Double(12000,24000))
                ;
            Datas=new ObservableCollection<ConnectionPageTestModel>( randomDatas.Generate(50));
            //Datas = new ObservableCollection<ConnectionPageTestModel>();
            //Enumerable.Range(1, 50).ToList().ForEach(i => Datas.Add(new ConnectionPageTestModel
            //{
            //    Name = $"Name{i}",
            //    Age = GetAge(i) + 18,
            //    Salary = i * GetAge(i) * 1000,
            //    Part = $"Part{i}",
            //    ID = i
            //}));
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
            return faker.PickRandom(new[] {"市场部", "人事部","总经办","宣传部","研发部"});
        }
    }
}
