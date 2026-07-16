using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using PropertyChanged;
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

        public string TestValue { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


        public Func<double, string> FormatterCount { get; set; }
        public Func<double, string> FormatterDuring { get; set; }

        public SeriesCollection SeriesCollection { get; set; }

        public ColumnSeries AlarmCountSeries { get; set; } = new ColumnSeries();
        public ColumnSeries AlarmDuringSeries { get; set; } = new ColumnSeries();
        public string[] XAxis { get; set; }
        public ChartValues<AlarmCountData> AlarmCount { get; set; }

        public ChartValues<AlarmDuringData> AlarmDuring { get; set; }

        public RulePageViewModel()
        {
            Time = DateTime.Now;
            var mapperCount = Mappers.Xy<AlarmCountData>()
                .X(model => model.Top)
                .Y(model => model.Value);

            var mapperDuring = Mappers.Xy<AlarmDuringData>()
                .X(model => model.Top)
                .Y(model => model.Value);

            Charting.For<AlarmDuringData>(mapperDuring);
            Charting.For<AlarmCountData>(mapperCount);

            XAxis = new string[] { "TOP1", "TOP2", "TOP3", "TOP4", "TOP5", "TOP6", "TOP7" };
            FormatterCount = value => value.ToString();
            FormatterDuring = value => value.ToString() + "min";
            AlarmCountSeries.Title = "报警次数";
            AlarmCountSeries.ScalesYAt = 0;
            AlarmDuringSeries.Title = "报警时长(min)";
            AlarmDuringSeries.ScalesYAt = 1;

            SeriesCollection = new SeriesCollection
            {
                AlarmCountSeries,
                AlarmDuringSeries
            };
            AlarmCount =
                [new AlarmCountData { Top = 1, Value = 8 ,AlarmMsg="Alarm0"},
                new AlarmCountData { Top = 2, Value = 20 ,AlarmMsg="Alarm1eeeeeeeeeeeeeeeeeeeeeeeeeeeeeee" },
                new AlarmCountData { Top = 3, Value = 22 ,AlarmMsg="Alarm2" },
                new AlarmCountData { Top = 4, Value = 32 ,AlarmMsg="Alarm3" },
                new AlarmCountData { Top = 5, Value = 43 ,AlarmMsg="Alarm4" },
                new AlarmCountData { Top = 6, Value = 54 ,AlarmMsg="Alarm5" },
                new AlarmCountData { Top = 7, Value = 68 ,AlarmMsg="Alarm6" }];
            AlarmDuring = [new AlarmDuringData { Top = 7, Value = 100 ,AlarmMsg="Alarm0"},
                new AlarmDuringData { Top = 6, Value = 200 , AlarmMsg="Alarm1"},
                new AlarmDuringData { Top = 5, Value = 300 , AlarmMsg="Alarm2"},
                new AlarmDuringData { Top = 4, Value = 400 , AlarmMsg="Alarm3"},
                new AlarmDuringData { Top = 3, Value = 500 , AlarmMsg="Alarm4"},
                new AlarmDuringData { Top = 2, Value = 600 , AlarmMsg="Alarm5"},
                new AlarmDuringData { Top = 1, Value = 700 , AlarmMsg="Alarm6"}];
            //AlarmDuring.Reverse();
            AlarmCountSeries.Values = AlarmCount;
            AlarmDuringSeries.Values =AlarmDuring;

        }
        [RelayCommand]
        private void ClearData()
        {
            AlarmCount.Clear();
            AlarmDuring.Clear();
            AlarmCountSeries.Values.Clear();
            AlarmDuringSeries.Values.Clear();
        }
        [RelayCommand]
        private void AddData()
        {
            foreach (var item in AlarmCount)
            {
                item.Value += 8;
            }
        }

    }
    [AddINotifyPropertyChangedInterface]
    public class AlarmCountData
    {
        public int Top { get; set; }
        public int Value { get; set; }

        public string AlarmMsg { get; set; }
    }
    public class AlarmDuringData()
    {
        public int Top { get; set; }
        public double Value { get; set; }

        public string AlarmMsg { get; set; }
    }
}
