using Microsoft.Extensions.Logging;
using CopilotTest2.Services;
using CopilotTest2.Views;

namespace CopilotTest2;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });


#if DEBUG
        builder.Logging.AddDebug();
#endif

        #region Registering the items created
        builder.Services.AddSingleton<FlowerService>();

        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<MainPage>();
        #endregion

        return builder.Build();
    }
}