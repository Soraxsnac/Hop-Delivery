using HopDelivery.Services.HttpServices;
using HopDelivery.App.Services.HttpServices;
using Microsoft.Extensions.Logging;

namespace HopDelivery.App
{
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

#if DEBUG
            builder.Logging.AddDebug();
#endif

            
            builder.Services.AddHttpClient<IApiService, ApiService>(client =>
            {
                client.BaseAddress = new Uri("http://127.0.0.1:5032/");
                client.Timeout = TimeSpan.FromSeconds(10);
            });

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<Views.LoginPage>();

            return builder.Build();
        }
    }
}