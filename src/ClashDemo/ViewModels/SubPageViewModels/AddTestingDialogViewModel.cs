using Clash.UI.Suppot.UI.Helpers;
using ClashDemo.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace ClashDemo.ViewModels.SubPageViewModels
{
    [INotifyPropertyChanged]
    public partial class AddTestingDialogViewModel
    {
        public string BoardName { get; set; }

        public NetTestBlockModel CurrentModel { get; set; }

        private NetTestBlockModel _model;
        private ObservableCollection<NetTestBlockModel> _models;

        public void Ini(string name, ObservableCollection<NetTestBlockModel> items, NetTestBlockModel model = null)
        {
            _models = items;
            if (name.Contains("新建"))
            {
                BoardName = name;
                CurrentModel = new NetTestBlockModel();
            }
            else
            {
                BoardName = name;
                CurrentModel = JsonConvert.DeserializeObject<NetTestBlockModel>(JsonConvert.SerializeObject(model));
            }
            _model = model;
        }
        [RelayCommand]
        private void CancelDashBoardSetting()
        {
            ShadowdialogHelper.CloseDialog();
        }
        [RelayCommand]
        private void SaveDaskBoardSetting()
        {
            if (BoardName.Contains("新建"))
            {
                _models.Add(CurrentModel);
            }
            else
            {

                if (_model != null)
                {
                    var index=_models.IndexOf(_model);
                    _models.Remove(_model);
                    _models.Insert(index, CurrentModel);
                }
            }
            ShadowdialogHelper.CloseDialog();

        }
    }
}
