using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;
using VehiculosMAUI.Services.HttpServices;
using VehiculosMAUI.Views;

namespace VehiculosMAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureSyncfusionCore()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // 1. Registro del Servicio de API (Cliente HTTP)
        builder.Services.AddSingleton<IApiService>(sp =>
        {
            var httpClient = new HttpClient();
            return new ApiService(httpClient);
        });

        // 2. Registro de las Vistas (Páginas)
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<AgregarCervezaPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}