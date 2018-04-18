namespace Assets.Scripts.ViewModels
{
    public class GameViewModel : PageViewModelBase
    {
        private static GameViewModel _instance;

        public static GameViewModel Instance => _instance ?? (_instance = new GameViewModel());
    }
}