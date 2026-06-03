using HopDelivery.App.Services.HttpServices;
using HopDelivery.Services.HttpServices;
using Microsoft.Extensions.Logging;

namespace HopDelivery.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                // Usamos global:: para asegurarle a C# que queremos la CLASE App, no el namespace
                .UseMauiApp<global::HopDelivery.App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton(sp => new HttpClient
            {
                BaseAddress = new Uri("http://127.0.0.1:5032/")
            });

            builder.Services.AddSingleton<IApiService, ApiService>();
            builder.Services.AddTransient<MainPage>();

            return builder.Build();
        }
    }
}