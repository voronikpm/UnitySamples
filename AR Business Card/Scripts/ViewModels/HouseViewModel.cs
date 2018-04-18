namespace Assets.Scripts.ViewModels
{
    public class HouseViewModel : PageViewModelBase
    {
        private static HouseViewModel _instance;

        public static HouseViewModel Instance => _instance ?? (_instance = new HouseViewModel());
    }
}