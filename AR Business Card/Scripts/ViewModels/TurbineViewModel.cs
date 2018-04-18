#region Using Directives

using System;
using System.Windows.Input;
using Assets.Scripts.AR;
using Assets.Scripts.Entities;
using Assets.Scripts.Enums;
using Assets.Scripts.GameObjects;
using Noesis;

#endregion

namespace Assets.Scripts.ViewModels
{
    public class TurbineViewModel : PageViewModelBase
    {
        #region Fields

        #region Static Fields and Constants

        private static TurbineViewModel _instance;

        #endregion

        #region  Private Fields

        private string _infoText;
        private bool _isElementSelectorShown;
        private bool _isInfoShown;
        private bool _isMuted;
        private bool _isTargetContainerShown;
        private bool _isTargetShown = true;
        private Canvas _renderTextureCanvas;
        private TurbineState _sceneState;
        private TurbineElement _selectedElement;
        private TargetQuality _targetQuality;

        #endregion

        #endregion

        #region Properties

        #region Regular Properties

        public bool IsElementSelectorShown
        {
            get { return _isElementSelectorShown; }
            set
            {
                if(value == _isElementSelectorShown)
                    return;
                _isElementSelectorShown = value;
                OnPropertyChanged();
            }
        }

        public bool IsInfoShown
        {
            get { return _isInfoShown; }
            set
            {
                if(_isInfoShown == value)
                    return;
                _isInfoShown = value;
                OnPropertyChanged();
            }
        }

        public bool IsMuted
        {
            get { return _isMuted; }
            set
            {
                if(value == _isMuted)
                    return;
                _isMuted = value;
                OnPropertyChanged();
            }
        }

        public bool IsTargetContainerShown
        {
            get { return _isTargetContainerShown; }
            set
            {
                if(_isTargetContainerShown == value)
                    return;
                _isTargetContainerShown = value;
                OnPropertyChanged();
            }
        }

        public bool IsTargetShown
        {
            get { return _isTargetShown; }
            set
            {
                if(_isTargetShown == value)
                    return;
                _isTargetShown = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PlayCommand));
            }
        }

        public Canvas RenderTextureCanvas
        {
            get { return _renderTextureCanvas; }
            set
            {
                _renderTextureCanvas = value;
                if(value != null && SceneControllerTurbine.Instance)
                    SceneControllerTurbine.Instance.SetRenderTexture();
            }
        }

        public TurbineState SceneState
        {
            get { return _sceneState; }
            set
            {
                if(_sceneState == value)
                    return;
                _sceneState = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PlayCommand));
                SelectedElement = TurbineElement.None;
            }
        }

        public TurbineElement SelectedElement
        {
            get { return _selectedElement; }
            set
            {
                if(_selectedElement == value)
                    return;
                _selectedElement = value;
                OnPropertyChanged();
                SceneControllerTurbine.Instance.SelectElement(value);
                IsElementSelectorShown = value != TurbineElement.None;
            }
        }

        public TargetQuality TargetQuality
        {
            get { return _targetQuality; }
            set
            {
                if(_targetQuality == value)
                    return;
                _targetQuality = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Static Properties

        public static TurbineViewModel Instance => _instance ?? (_instance = new TurbineViewModel());

        #endregion

        #region Commands

        public ICommand InfoCommand => new RelayCommand(p => IsInfoShown = (bool) p);
        public ICommand PlayCommand => new RelayCommand(p => StartAnimation((TurbineState) p), p => CanExecute((TurbineState) p));
        public ICommand ScanCommand => new RelayCommand(() => UDTEventHandler.Instance.BuildNewTarget());
        public ICommand SelectElementCommand => new RelayCommand(p => SelectedElement = (TurbineElement) p);
        public ICommand SettingsCommand => new RelayCommand(() => OverlayViewModel.Instance.ShowSettings());
        public ICommand ShareCommand => new RelayCommand(() => SceneControllerCommon.Instance.ShareImage());

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        public void RefreshCommands()
        {
            OnPropertyChanged(nameof(PlayCommand));
            OnPropertyChanged(nameof(ScanCommand));
            OnPropertyChanged(nameof(SceneState));
        }

        public void RefreshProperties()
        {
            OnPropertyChanged(nameof(IsMuted));
            OnPropertyChanged(nameof(IsTargetShown));
            OnPropertyChanged(nameof(SceneState));
            OnPropertyChanged(nameof(TargetQuality));
            OnPropertyChanged(nameof(PlayCommand));
        }

        private bool CanExecute(TurbineState? state = null)
        {
            bool isCorrectState = state == null || SceneControllerTurbine.Instance.SceneState != state;
            return !SceneControllerTurbine.Instance.IsAnimating && isCorrectState && !IsTargetShown;
        }

        private void StartAnimation(TurbineState state)
        {
            IsInfoShown = false;
            TurbineAnimationType animationType;
            switch(state)
            {
                case TurbineState.Default:
                {
                    animationType = TurbineAnimationType.None;
                    break;
                }
                case TurbineState.Cut:
                {
                    animationType = TurbineAnimationType.Cutoff;
                    break;
                }
                case TurbineState.Exploded:
                {
                    animationType = TurbineAnimationType.Move;
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
            SceneControllerTurbine.Instance.StartAnimations(animationType);
        }

        #endregion

        #endregion
    }
}