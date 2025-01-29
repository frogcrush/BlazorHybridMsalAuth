using BlazorHybridMsalAuth.Config;
using BlazorHybridMsalAuth.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;

namespace BlazorHybridMsalAuth
{
    public static class MauiProgram
    {
        public static IServiceProvider? Services { get; private set; }

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            builder.Configuration.AddJsonFile("appsettings.json");

            builder.Services.AddOptions();
            // Add auth
            builder.Services.Configure<AzureAdConfig>(options => builder.Configuration.GetSection("MsalAuthentication").Bind(options));
            builder.Services.AddSingleton<MSALClientHelper>();
            builder.Services.AddAuthorizationCore();
            builder.Services.TryAddScoped<AuthenticationStateProvider, MsalAuthenticationStateProvider>();

            var app = builder.Build();
            Services = app.Services;

            Services.GetRequiredService<MSALClientHelper>().InitializePublicClientApp();

            return app;
        }
    }
}