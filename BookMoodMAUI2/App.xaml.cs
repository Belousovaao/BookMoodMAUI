using Microsoft.Extensions.DependencyInjection;

namespace BookMoodMAUI2
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _serviceProvider = serviceProvider;

            MainPage = _serviceProvider.GetRequiredService<Pages.MainPage>();

            Application.Current.UserAppTheme = AppTheme.Dark;
            _serviceProvider = serviceProvider;
        }
    }
}