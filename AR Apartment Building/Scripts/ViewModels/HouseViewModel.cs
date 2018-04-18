#region Using Directives

using System.Windows.Input;
using Assets.Scripts.Entities;
using Assets.Scripts.Enums;
using Assets.Scripts.GameObjects;

#endregion

namespace Assets.Scripts.ViewModels
{
    public class HouseViewModel : PageViewModelBase
    {
        #region Fields

        #region Static Fields and Constants

        private static HouseViewModel _instance;

        #endregion

        #region  Private Fields

        private int _floor;
        private BuildingState _state;

        #endregion

        #endregion

        #region Properties

        #region Regular Properties

        public int Floor
        {
            get { return _floor; }
            set
            {
                _floor = value;
                SceneControllerHouse.Instance.Floor = value;
                OnPropertyChanged("Floor");
                OnPropertyChanged("IsMaxFloor");
                OnPropertyChanged("IsMinFloor");
                OnPropertyChanged("ChangeFloorCommand");
            }
        }

        public bool IsMaxFloor
        {
            get { return Floor == MaxFloor; }
        }

        public bool IsMinFloor
        {
            get { return Floor == 0; }
        }

        public int MaxFloor { get; set; }

        public BuildingState State
        {
            get { return _state; }
            set
            {
                if(_state == value)
                    return;
                if(value == BuildingState.Floor)
                    Floor = 0;
                _state = value;
                SceneControllerHouse.Instance.State = value;
                OnPropertyChanged("State");
                OnPropertyChanged("ChangeStateCommand");
            }
        }

        #endregion

        #region Static Properties

        public static HouseViewModel Instance
        {
            get { return _instance ?? (_instance = new HouseViewModel()); }
        }

        #endregion

        #region Commands

        public ICommand ChangeFloorCommand
        {
            get
            {
                return new RelayCommand(p =>
                                        {
                                            if(Floor == MaxFloor && (int) p > 0)
                                                Floor = 0;
                                            else if(Floor == 0 && (int) p < 0)
                                                Floor = MaxFloor;
                                            else
                                                Floor += (int) p;
                                        });
            }
        }

        public ICommand ChangeStateCommand
        {
            get { return new RelayCommand(p => State = (BuildingState) p, p => State != (BuildingState) p); }
        }

        #endregion

        #endregion
    }
}