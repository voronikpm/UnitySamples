#region Using Directives

using System;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using Assets.Scripts.EventArgs;
using Assets.Scripts.GameObjects;

#endregion

namespace Assets.Scripts.ViewModels
{
    public class OverlayViewModel : ViewModelBase
    {
        #region Fields

        #region Static Fields and Constants

        private static OverlayViewModel _instance;

        #endregion

        #region  Private Fields

        private readonly Dictionary<SceneType, ViewModelBase> _viewModels = new Dictionary<SceneType, ViewModelBase>
                                                                            {
                                                                                {SceneType.Menu, MainMenuViewModel.Instance},
                                                                                {SceneType.Turbine, TurbineViewModel.Instance},
                                                                                {SceneType.House, HouseViewModel.Instance},
                                                                                {SceneType.Game, GameViewModel.Instance},
                                                                                {SceneType.None, null}
                                                                            };

        private SceneType _currentScene;
        private bool _isEnabled = true;

        #endregion

        #endregion

        #region Constructors

        ~OverlayViewModel()
        {
            SceneControllerCommon.Instance.OnSceneLoaded -= OnSceneLoaded;
        }

        #endregion

        #region Properties

        #region Regular Properties

        public SceneType CurrentScene
        {
            get { return _currentScene; }
            private set
            {
                if(value == _currentScene)
                    return;
                _currentScene = value;
                OnPropertyChanged();
                ShowCurrentSceneView();
            }
        }

        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                if(Equals(value, _currentViewModel))
                    return;
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if(value == _isEnabled)
                    return;
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        private float _framerate;
        private ViewModelBase _currentViewModel;

        public float Framerate
        {
            get { return _framerate; }
            set
            {
                if(Math.Abs(_framerate - value) < float.Epsilon)
                    return;
                _framerate = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Static Properties

        public static OverlayViewModel Instance => _instance ?? (_instance = new OverlayViewModel());

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        public void Init()
        {
            SceneControllerCommon.Instance.OnSceneLoaded += OnSceneLoaded;
        }

        public void ShowSettings()
        {
            //CurrentViewModel = SettingsViewModel.Instance;
            SettingsViewModel.Instance.IsVisible = true;
            SettingsViewModel.Instance.SelectedScene = CurrentScene;
        }
        
        public void ShowCurrentSceneView()
        {
            SettingsViewModel.Instance.IsVisible = false;
            CurrentViewModel = _viewModels[CurrentScene];
        }

        public SettingsViewModel SettingsContext => SettingsViewModel.Instance;

        #endregion

        #region Event Handlers

        private void OnSceneLoaded(object sender, SceneLoadedEventArgs args)
        {
            CurrentScene = args;
            IsEnabled = true;
        }

        public void RefreshLanguages()
        {
            RefreshLanguage();
            TurbineViewModel.Instance.RefreshLanguage();
        }

        #endregion

        #endregion
    }
}