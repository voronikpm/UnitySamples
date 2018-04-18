#region Using Directives

using System;
using System.Linq;
using System.Windows.Input;
using Assets.Scripts.Entities;
using Assets.Scripts.ExtensionMethods;
using Assets.Scripts.GameObjects;
using Assets.Scripts.Helpers;
using UnityEngine.SceneManagement;

#endregion

namespace Assets.Scripts.ViewModels
{
    public class ApartmentViewModel : PageViewModelBase
    {
        #region Fields

        #region Static Fields and Constants

        private static ApartmentViewModel _instance;

        #endregion

        #region  Private Fields

        private ChatControlViewModel _chatViewModel;
        private bool _isChatVisible;
        private bool _isFurnitureSelected;
        private bool _isInfoShown;
        private bool _isTutorialShown = true;
        private float _lookX;
        private float _lookY;
        private float _moveX;
        private float _moveY;
        private FurnitureViewModel _selectedViewModel;

        #endregion

        #endregion

        #region Constructors

        public ApartmentViewModel()
        {
            SelectedViewModel = FurnitureViewModel.Instance;
            ChatViewModel = new ChatControlViewModel();
        }

        #endregion

        #region Properties

        #region Regular Properties

        public ChatControlViewModel ChatViewModel
        {
            get { return _chatViewModel; }
            set
            {
                if(_chatViewModel == value)
                    return;
                _chatViewModel = value;
                OnPropertyChanged();
            }
        }

        public bool IsChatVisible
        {
            get { return _isChatVisible; }
            set
            {
                if(_isChatVisible == value)
                    return;
                _isChatVisible = value;
                OnPropertyChanged("IsChatVisible");
                OnPropertyChanged("ShowChatCommand");
                OnPropertyChanged("ShowInfoCommand");
                SceneControllerApartment.Instance.Joysticks.Select(x => x.gameObject.transform.parent.gameObject).InvokeAction(x => x.SetActive(!value));
            }
        }

        public bool IsFurnitureSelected
        {
            get { return _isFurnitureSelected; }
            set
            {
                if(value == _isFurnitureSelected)
                    return;
                _isFurnitureSelected = value;
                OnPropertyChanged("IsFurnitureSelected");
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
                SceneControllerApartment.Instance.Joysticks.Select(x => x.gameObject.transform.parent.gameObject).InvokeAction(x => x.SetActive(!value));
                OnPropertyChanged("ShowInfoCommand");
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

        public bool IsTutorialShown
        {
            get { return _isTutorialShown; }
            set
            {
                if(_isTutorialShown == value)
                    return;
                _isTutorialShown = value;
                OnPropertyChanged("IsTutorialShown");
                OnPropertyChanged("ShowChatCommand");
                SceneControllerApartment.Instance.Joysticks.ForEach(x => x.enabled = !value);
            }
        }

        public float LookX
        {
            get { return _lookX; }
            set
            {
                if(Math.Abs(_lookX - value) < float.Epsilon)
                    return;
                _lookX = value;
                CustomInput.Axes["Look X"] = value;
                OnPropertyChanged("LookX");
            }
        }

        public float LookY
        {
            get { return _lookY; }
            set
            {
                if(Math.Abs(_lookY - value) < float.Epsilon)
                    return;
                _lookY = value;
                CustomInput.Axes["Look Y"] = value;
                OnPropertyChanged("LookY");
            }
        }

        public float MoveX
        {
            get { return _moveX; }
            set
            {
                if(Math.Abs(_moveX - value) < float.Epsilon)
                    return;
                _moveX = value;
                CustomInput.Axes["Move X"] = value;
                OnPropertyChanged("MoveX");
            }
        }

        public float MoveY
        {
            get { return _moveY; }
            set
            {
                if(Math.Abs(_moveY - value) < float.Epsilon)
                    return;
                _moveY = value;
                CustomInput.Axes["Move Y"] = value;
                OnPropertyChanged("MoveY");
            }
        }

        public FurnitureViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                if(_selectedViewModel == value)
                    return;
                _selectedViewModel = value;
                OnPropertyChanged("SelectedViewModel");
            }
        }

        #endregion

        #region Static Properties

        public static ApartmentViewModel Instance
        {
            get { return _instance ?? (_instance = new ApartmentViewModel()); }
        }

        #endregion

        #region Commands

        //TODO Replace with a SceneControllerCommon call
        public ICommand BackCommand
        {
            get { return new RelayCommand(() => SceneControllerApartment.Instance.LoadScene("SceneHouse")); }
        }

        public ICommand HideChatCommand
        {
            get { return new RelayCommand(() => IsChatVisible = false); }
        }

        public ICommand HideInfoCommand
        {
            get { return new RelayCommand(() => IsInfoShown = false); }
        }

        public ICommand HideTutorialCommand
        {
            get { return new RelayCommand(() => IsTutorialShown = false); }
        }

        public ICommand MuteCommand
        {
            get
            {
                return new RelayCommand(p => SceneControllerApartment.Instance.Mute((bool) p));
            }
        }

        public ICommand ShowChatCommand
        {
            get { return new RelayCommand(() => IsChatVisible = true, p => !IsChatVisible && !IsInfoShown && !IsOrderVisible); }
        }

        public ICommand ShowInfoCommand
        {
            get { return new RelayCommand(() => IsInfoShown = true, p => !IsChatVisible && !IsInfoShown && !IsOrderVisible); }
        }

        public ICommand ShowOrderCommand
        {
            get { return new RelayCommand(() => IsOrderVisible = true, p => !IsChatVisible && !IsInfoShown && !IsOrderVisible); }
        }

        public ICommand HideOrderCommand
        {
            get { return new RelayCommand(() => IsOrderVisible = false, p => !IsChatVisible && !IsInfoShown && IsOrderVisible); }
        }

        public ICommand SendOrderCommand
        {
            get { return new RelayCommand(() => IsOrderVisible = false, p => !string.IsNullOrEmpty(CustomerName) && !string.IsNullOrEmpty(CustomerPhone));}
        }

        private string _customerName;

        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                if(_customerName == value)
                    return;
                _customerName = value;
                OnPropertyChanged("CustomerName");
                OnPropertyChanged("CustomerPhone");
                OnPropertyChanged("SendOrderCommand");
            }
        }

        private string _customerPhone;

        public string CustomerPhone
        {
            get { return _customerPhone; }
            set
            {
                if(_customerPhone == value)
                    return;
                _customerPhone = value;
                OnPropertyChanged("CustomerName");
                OnPropertyChanged("CustomerPhone");
                OnPropertyChanged("SendOrderCommand");
            }
        }

        private bool _isOrderVisible;

        public bool IsOrderVisible
        {
            get { return _isOrderVisible; }
            set
            {
                if(_isOrderVisible == value)
                    return;
                _isOrderVisible = value;
                OnPropertyChanged("IsOrderVisible");
                OnPropertyChanged("HideOrderCommand");
                OnPropertyChanged("ShowOrderCommand");
                SceneControllerApartment.Instance.Joysticks.Select(x => x.gameObject.transform.parent.gameObject).InvokeAction(x => x.SetActive(!value));
            }
        }

        #endregion

        #endregion

        #region Methods

        #region Regular Methods
        

        #endregion

        #endregion
    }
}