using Autofac;
using Clash.UI.Suppot.UI.Componentes;
using Clash.UI.Suppot.UI.Helpers;
using ClashDemo.Models;
using ClashDemo.Views.Dialogs;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ClashDemo.ViewModels.SubPageViewModels
{
    [INotifyPropertyChanged]
    public partial class NetTestBoardViewModel
    {
        private ShadowDialog _shadowDialog;
        public ObservableCollection<NetTestBlockModel> TestItems { get; set; }

        public NetTestBoardViewModel() 
        {
            TestItems = new ObservableCollection<NetTestBlockModel>();

            for (int i = 0; i < 10; i++) 
            {
                TestItems.Add(new NetTestBlockModel() 
                {
                    BlockName=$"Bili Bili{i}",
                    NetDelay=333,

                });
            }
        }
        [RelayCommand]
        private void ShowAddDialog()
        {
            var window = App.Current.Container.Resolve<MainWindow>();
            UserControl common = App.Current.Container.Resolve<AddTestingDialog>();
            common.DataContext = this;
            _shadowDialog = new ShadowDialog();
            ShadowdialogHelper.RunDialog(window.gridMain.Children, _shadowDialog, common);

        }

        [RelayCommand]
        private async Task TestNets() 
        {
            //foreach (var item in TestItems)
            //{
            //    item.IsTesting=true;
            //}
            TestItems.Select(x => x.IsTesting = true).ToList();
            await Task.Delay(2000);
            TestItems.Select(x => x.IsTesting = false).ToList();
        }

        [RelayCommand]
        private void CancelDashBoardSetting()
        {
            ShadowdialogHelper.CloseDialog();
        }
        [RelayCommand]
        private void SaveDaskBoardSetting()
        {
            ShadowdialogHelper.CloseDialog();
        }
    }
}
