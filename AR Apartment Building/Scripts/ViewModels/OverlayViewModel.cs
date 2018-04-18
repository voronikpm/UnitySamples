#region Using Directives

using System;
using System.Collections.Generic;
using System.Windows.Input;
using Assets.Scripts.AR;
using Assets.Scripts.Entities;
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
        private float _framerate;
        private bool _isApartmentInfoShown;
        private bool _isBuildingInfoShown;
        private bool _isEnabled = true;
        private bool _isGreenAimVisible;
        private bool _isMenuVisible;
        private bool _isSuggestionShown = true;
        private bool _isTutorialShown;
        private bool _isWhiteAimVisible = true;
        private int _tutorialStage;

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
                OnPropertyChanged("CurrentScene");
                OnPropertyChanged("CurrentViewModel");
            }
        }

        //TODO: restore in the merged version
        //get { return _viewModels[CurrentScene]; }
        public ViewModelBase CurrentViewModel
        {
            get { return _viewModels[SceneType.House]; }
        }

        public float Framerate
        {
            get { return _framerate; }
            set
            {
                if(Math.Abs(_framerate - value) < float.Epsilon)
                    return;
                _framerate = value;
                OnPropertyChanged("Framerate");
            }
        }

        public bool IsApartmentInfoShown
        {
            get { return _isApartmentInfoShown; }
            set
            {
                if(_isApartmentInfoShown == value)
                    return;
                _isApartmentInfoShown = value;
                OnPropertyChanged("IsApartmentInfoShown");
            }
        }

        public bool IsBuildingInfoShown
        {
            get { return _isBuildingInfoShown; }
            set
            {
                if(_isBuildingInfoShown == value)
                    return;
                _isBuildingInfoShown = value;
                OnPropertyChanged("IsBuildingInfoShown");
            }
        }

        public bool IsContentShown
        {
            get { return !IsSuggestionShown && !IsTutorialShown; }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if(value == _isEnabled)
                    return;
                _isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }

        public bool IsGreenAimVisible
        {
            get { return _isGreenAimVisible; }
            set
            {
                if(_isGreenAimVisible == value)
                    return;
                _isGreenAimVisible = value;
                OnPropertyChanged("IsGreenAimVisible");
                IsWhiteAimVisible = !value;
            }
        }

        public bool IsMenuVisible
        {
            get { return _isMenuVisible; }
            set
            {
                if(_isMenuVisible == value)
                    return;
                _isMenuVisible = value;
                OnPropertyChanged("IsMenuVisible");
            }
        }

        private bool _isMuted;

        public bool IsMuted
        {
            get { return _isMuted; }
            set
            {
                _isMuted = value;
                OnPropertyChanged("IsMuted");
            }
        }

        public bool IsSuggestionShown
        {
            get { return _isSuggestionShown; }
            set
            {
                if(_isSuggestionShown == value)
                    return;
                _isSuggestionShown = value;
                OnPropertyChanged("IsSuggestionShown");
                OnPropertyChanged("ToggleMenuCommand");
                if (!value)
                {
                    if (_isFirstHelp)
                    {
                        IsTutorialShown = true;
                        _isFirstHelp = false;
                    }
                }
                else
                    IsMenuVisible = false;
                OnPropertyChanged("IsTutorialShown");
                OnPropertyChanged("IsContentShown");
            }
        }

        private bool _isFirstHelp = true;

        public bool IsTutorialShown
        {
            get { return _isTutorialShown; }
            set
            {
                if(_isTutorialShown == value)
                    return;
                _isTutorialShown = value;
                if(value)
                    TutorialStage = 0;
                OnPropertyChanged("IsTutorialShown");
                OnPropertyChanged("IsContentShown");
                OnPropertyChanged("ShowTutorialCommand");
                OnPropertyChanged("ToggleMenuCommand");
                if(value)
                    IsMenuVisible = false;
            }
        }

        public bool IsWhiteAimVisible
        {
            get { return _isWhiteAimVisible; }
            set
            {
                if(_isWhiteAimVisible == value)
                    return;
                _isWhiteAimVisible = value;
                OnPropertyChanged("IsWhiteAimVisible");
                IsGreenAimVisible = !value;
            }
        }

        public int TutorialStage
        {
            get { return _tutorialStage; }
            set
            {
                if(_tutorialStage == value)
                    return;
                _tutorialStage = value;
                OnPropertyChanged("TutorialStage");
                if(value >= 4)
                {
                    IsTutorialShown = false;
                    TutorialStage = 0;
                }
            }
        }

        #endregion

        #region Static Properties

        public static OverlayViewModel Instance
        {
            get { return _instance ?? (_instance = new OverlayViewModel()); }
        }

        #endregion

        #region Commands

        public ICommand EndTutorialCommand
        {
            get { return new RelayCommand(() => IsTutorialShown = false); }
        }

        public ICommand HideInfoCommand
        {
            get
            {
                return new RelayCommand(() =>
                                        {
                                            IsApartmentInfoShown = false;
                                            IsBuildingInfoShown = false;
                                        });
            }
        }

        public ICommand MuteCommand
        {
            get
            {
                return new RelayCommand(p => SceneControllerHouse.Instance.Mute((bool) p));
            }
        }

        public ICommand NextTutorialStageCommand
        {
            get { return new RelayCommand(() => TutorialStage++); }
        }

        public ICommand ScanCommand
        {
            get { return new RelayCommand(() => UDTEventHandler.Instance.BuildNewTarget()); }
        }

        public ICommand ShowAimCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    UDTEventHandler.Instance.ClearTarget();
                    IsSuggestionShown = true;
                });
            }
        }

        public ICommand ShowTutorialCommand
        {
            get { return new RelayCommand(() => IsTutorialShown = true, p => !IsTutorialShown); }
        }

        public ICommand ToggleMenuCommand
        {
            get { return new RelayCommand(() => IsMenuVisible = !IsMenuVisible, p => !IsTutorialShown && !IsSuggestionShown); }
        }

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        public void Init()
        {
            SceneControllerCommon.Instance.OnSceneLoaded += OnSceneLoaded;
        }
        
        #endregion

        #region Event Handlers

        private void OnSceneLoaded(object sender, SceneLoadedEventArgs args)
        {
            CurrentScene = args;
            IsEnabled = true;
        }

        #endregion

        #endregion
    }
}