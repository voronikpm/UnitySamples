using System.Windows.Input;
using Assets.Scripts.Entities;
using Assets.Scripts.Enums;
using Assets.Scripts.GameObjects;

namespace Assets.Scripts.ViewModels
{
    public abstract class PageViewModelBase: ViewModelBase
    {
        public ICommand MainMenuCommand
        {
            get { return new RelayCommand(() => SceneControllerCommon.Instance.LoadScene(SceneType.Menu)); }
        }
    }
}