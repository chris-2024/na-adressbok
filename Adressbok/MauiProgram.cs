using Adressbok.Services;
using Adressbok.ViewModels;
using Adressbok.Views;

namespace Adressbok
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
                    fonts.AddFont("fa-solid-900.ttf", "FontAwesome");
                });

            builder.Services.AddSingleton<ContactService>();

            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();

            builder.Services.AddTransient<DetailsViewModel>();
            builder.Services.AddTransient<DetailsPage>();

            builder.Services.AddTransient<ManageContactViewModel>();
            builder.Services.AddTransient<ManageContactPage>();

            return builder.Build();
        }
    }
}