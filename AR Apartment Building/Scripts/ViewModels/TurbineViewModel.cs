#region Using Directives

using System;
using System.Windows.Input;
using Assets.Scripts.Entities;
using Assets.Scripts.Enums;
using Assets.Scripts.GameObjects;

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
        private bool _isInfoShown;
        private bool _isMuted;

        #endregion

        #endregion

        #region Properties

        #region Regular Properties

        public string InfoText
        {
            get { return _infoText; }
            set
            {
                if(value == _infoText)
                    return;
                _infoText = value;
                OnPropertyChanged("InfoText");
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
                OnPropertyChanged("IsInfoShown");
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
                OnPropertyChanged("IsMuted");
            }
        }

        #endregion

        #region Static Properties

        public static TurbineViewModel Instance
        {
            get { return _instance ?? (_instance = new TurbineViewModel()); }
        }

        #endregion

        #region Commands

        public ICommand InfoCommand
        {
            get { return new RelayCommand(p => IsInfoShown = (bool) p, o => CanExecute()); }
        }

        public ICommand MuteCommand
        {
            get { return new RelayCommand(p => SceneControllerTurbine.Instance.Mute((bool) p)); }
        }

        public ICommand PlayCommand
        {
            get { return new RelayCommand(p => StartAnimation((TurbineState) p), p => CanExecute((TurbineState) p)); }
        }

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        private bool CanExecute(TurbineState? state = null)
        {
            bool isCorrectState = state == null || SceneControllerTurbine.Instance.SceneState != state;
            return !SceneControllerTurbine.Instance.IsAnimating && isCorrectState;
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
                    throw new ArgumentOutOfRangeException("state", state, null);
            }
            SceneControllerTurbine.Instance.StartAnimations(animationType);
        }

        #endregion

        #endregion
    }
}