#region Using Directives

using System.Windows.Input;
using Assets.Scripts.Entities;
using Assets.Scripts.Enums;
using Assets.Scripts.GameObjects;

#endregion

namespace Assets.Scripts.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        #region Fields

        #region Static Fields and Constants

        private static MainMenuViewModel _instance;

        #endregion

        #endregion

        #region Properties

        #region Static Properties

        public static MainMenuViewModel Instance
        {
            get { return _instance ?? (_instance = new MainMenuViewModel()); }
        }

        #endregion

        #region Commands

        public ICommand LoadCommand
        {
            get { return new RelayCommand(p => SceneControllerCommon.Instance.LoadScene((SceneType) p)); }
        }

        #endregion

        #endregion
    }
}