using BookMoodMAUI2.ViewModel;
using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Toolkit.Hosting;

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
            builder.Services.AddDbContext<BookDbContext>(options => options.UseSqlite("Data Source=library.db"));

#if DEBUG
            builder.Logging.AddDebug();
    		builder.Services.AddLogging(configure => configure.AddDebug());

#endif

            builder.Services.AddSingleton<IBookRepository, EFBookRepository>();
            builder.Services.AddSingleton<IBookFactory, DefaultBookFactory>();
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddTransient<MainPage>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<BookDbContext>();
                db.Database.EnsureCreated();
            }

            return app;
        }
    }
}
