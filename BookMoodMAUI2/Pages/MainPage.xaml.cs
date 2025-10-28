using BookMoodMAUI2.ViewModel;


namespace BookMoodMAUI2.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}