using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using TechnicalAxos_HernanLagrava.Services;
using TechnicalAxos_HernanLagrava.ViewModels;
using TechnicalAxos_HernanLagrava.Views;

namespace TechnicalAxos_HernanLagrava
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });



            // Add Services

            builder.Services.AddSingleton<IMainThreadInvoker, MainThreadInvoker>();
            builder.Services.AddSingleton<HttpClient>(sp => new HttpClient());

            builder.Services.AddSingleton<ICountryService, CountryService>();
            builder.Services.AddSingleton<ICustomAppInfo, AppInfoImplementation>();

            // Register ViewModel y MainPage
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();


#if DEBUG
            builder.Logging.AddDebug();
#endif


            var serviceProvider = builder.Services.BuildServiceProvider();
            DependencyResolver.SetServiceProvider(serviceProvider);

            return builder.Build();
        }
    }
}
