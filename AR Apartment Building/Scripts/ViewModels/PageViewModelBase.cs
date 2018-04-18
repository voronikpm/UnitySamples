#region Using Directives

using System.Windows.Input;
using Assets.Scripts.Entities;
using Assets.Scripts.Enums;
using Assets.Scripts.GameObjects;

#endregion

namespace Assets.Scripts.ViewModels
{
    public abstract class PageViewModelBase : ViewModelBase
    {
        #region Properties

        #region Commands

        public ICommand MainMenuCommand
        {
            get { return new RelayCommand(() => SceneControllerCommon.Instance.LoadScene(SceneType.Menu)); }
        }

        #endregion

        #endregion
    }
}