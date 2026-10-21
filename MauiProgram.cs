using Microsoft.Extensions.Logging;
using GymApp.Services;
using GymApp.ViewModels;
using GymApp.Views;

namespace GymApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<RoutineService>();
        builder.Services.AddTransient<RoutinesViewModel>();
		builder.Services.AddTransient<RoutinesPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}