using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Assets.Scripts.Entities;
using Assets.Scripts.Enums;
using Assets.Scripts.GameObjects;

namespace Assets.Scripts.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private static SettingsViewModel _instance;
        private SceneType _selectedScene;
        public static SettingsViewModel Instance => _instance ?? (_instance = new SettingsViewModel());

        public Dictionary<SceneType, Settings> SettingsDictionary => SceneControllerCommon.Instance.SettingsDictionary;

        public SceneType SelectedScene
        {
            get { return _selectedScene; }
            set
            {
                if(value == _selectedScene)
                    return;
                _selectedScene = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CurrentSettings));
                OnPropertyChanged(nameof(SettingsDictionary));
            }
        }
        
        public Settings CurrentSettings => SettingsDictionary[SelectedScene];

        public ICommand SelectSceneCommand => new RelayCommand(p => SelectedScene = (SceneType)p);

        public ICommand BackCommand => new RelayCommand(() => OverlayViewModel.Instance.ShowCurrentSceneView());

        private bool _isVisible;

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if(_isVisible == value)
                    return;
                _isVisible = value;
                OnPropertyChanged();
            }
        }
    }
}