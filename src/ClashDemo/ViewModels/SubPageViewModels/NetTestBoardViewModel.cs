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

            for (int i = 0; i < 4; i++) 
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
            var dataContext= App.Current.Container.Resolve<AddTestingDialogViewModel>();
            dataContext.Ini("新建测试",TestItems);
            common.DataContext = dataContext;
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
        private void EditNetTestBoard(NetTestBlockModel netTestBlock)
        {
            var window = App.Current.Container.Resolve<MainWindow>();
            UserControl common = App.Current.Container.Resolve<AddTestingDialog>();
            var dataContext = App.Current.Container.Resolve<AddTestingDialogViewModel>();
            dataContext.Ini("编辑测试", TestItems,netTestBlock);
            common.DataContext= dataContext;
            _shadowDialog = new ShadowDialog();
            ShadowdialogHelper.RunDialog(window.gridMain.Children, _shadowDialog, common);
        }
        [RelayCommand]
        private void DeleteNetTestBoard(NetTestBlockModel netTestBlock)
        {
            TestItems.Remove(netTestBlock);
        }
    }
}
