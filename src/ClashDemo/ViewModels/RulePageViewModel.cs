using CommunityToolkit.Mvvm.ComponentModel;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


        public Func<double,string> FormatterCount { get; set; }
        public Func<double,string> FormatterDuring { get; set; }

        public SeriesCollection SeriesCollection { get; set; }

        public ColumnSeries AlarmCountSeries { get; set; }=new ColumnSeries();
        public ColumnSeries AlarmDuringSeries { get; set; }=new ColumnSeries();
        public string[] XAxis { get; set; }

        public List<AlarmCountData> AlarmCount { get; set; }

        public List<AlarmDuringData> AlarmDuring { get; set; }

        public RulePageViewModel()
        {
            Time = DateTime.Now;
            var mapperCount=Mappers.Xy<AlarmCountData>()
                .X(model=>model.Top)
                .Y(model=>model.Value);

            var mapperDuring= Mappers.Xy<AlarmDuringData>()
                .X(model => model.Top)
                .Y(model => model.Value);

            Charting.For<AlarmDuringData>(mapperDuring);
            Charting.For<AlarmCountData>(mapperCount);

            XAxis = new string[] { "TOP1", "TOP2", "TOP3", "TOP4", "TOP5", "TOP6", "TOP7" };
            FormatterCount = value => value.ToString("R");
            FormatterDuring = value => value.ToString("R");
            AlarmCountSeries.Title = "报警次数";
            AlarmCountSeries.ScalesYAt = 0;
            AlarmDuringSeries.Title = "报警时长";
            AlarmDuringSeries.ScalesYAt = 1;

            SeriesCollection = new SeriesCollection
            {
                AlarmCountSeries,
                AlarmDuringSeries
            };
            AlarmCount = [new AlarmCountData { Top = 1, Value = 8 }, new AlarmCountData { Top = 2, Value = 20 }, new AlarmCountData { Top = 3, Value = 22 }, new AlarmCountData { Top = 4, Value = 32 }, new AlarmCountData { Top = 5, Value = 43 }, new AlarmCountData { Top = 6, Value = 54 }, new AlarmCountData { Top = 7, Value = 68 }];
            AlarmDuring = [new AlarmDuringData { Top = 7, Value = 100 }, new AlarmDuringData { Top = 6, Value = 200 }, new AlarmDuringData { Top = 5, Value = 300 }, new AlarmDuringData { Top = 4, Value = 400 }, new AlarmDuringData { Top = 3, Value = 500 }, new AlarmDuringData { Top = 2, Value = 600 }, new AlarmDuringData { Top = 1, Value = 700 }];
            //AlarmDuring.Reverse();
            AlarmCountSeries.Values=new ChartValues<AlarmCountData>(AlarmCount);
            AlarmDuringSeries.Values=new ChartValues<AlarmDuringData>(AlarmDuring);

        }

        public class AlarmCountData() 
        {
            public int Top { get; set; }
            public int Value{ get; set; }

            public string AlarmMsg { get; set; }
        }
        public class AlarmDuringData()
        {
            public int Top { get; set; }
            public double Value { get; set; }

            public string AlarmMsg { get; set; }
        }
    }
}
