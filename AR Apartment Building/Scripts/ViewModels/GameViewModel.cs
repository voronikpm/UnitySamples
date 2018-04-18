namespace Assets.Scripts.ViewModels
{
    public class GameViewModel : PageViewModelBase
    {
        #region Fields

        #region Static Fields and Constants

        private static GameViewModel _instance;

        #endregion

        #endregion

        #region Properties

        #region Static Properties

        public static GameViewModel Instance
        {
            get { return _instance ?? (_instance = new GameViewModel()); }
        }

        #endregion

        #endregion
    }
}