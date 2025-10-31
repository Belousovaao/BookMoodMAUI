using BookMoodMAUI2.ViewModel;
using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Toolkit.Hosting;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BookMoodMAUI2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureSyncfusionToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("SegoeUI-Semibold.ttf", "SegoeSemibold");
                    fonts.AddFont("FluentSystemIcons-Regular.ttf", FluentUI.FontFamily);
                });

            //EF
            builder.Services.AddDbContext<BookDbContext>(options =>
            {
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, "books.db");
                Debug.WriteLine($"File exists: {File.Exists(dbPath)}");
                options.UseSqlite($"Data Source={dbPath}");
            });

#if DEBUG
            builder.Logging.AddDebug();
    		builder.Services.AddLogging(configure => configure.AddDebug());

#endif

            builder.Services.AddSingleton<IBookRepository, EFBookRepository>();
            builder.Services.AddSingleton<IBookFactory, DefaultBookFactory>();
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<DatabaseInitializerService>();

            var app = builder.Build();

            InitializeDatabaseAsync(app.Services).GetAwaiter().GetResult();

            return app;
        }

        private static async Task InitializeDatabaseAsync(IServiceProvider services)
        {
            try
            {
                using var scope = services.CreateScope();
                var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializerService>();
                initializer.Initialize();
                Debug.WriteLine("Database initialization completed");

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Database initialization failed: {ex.Message}");
            }
        }
    }
}
