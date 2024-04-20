using MailSender.MAUI.ViewModels;
using MailSender.MAUI.Views;
using MailSender.Services;
using Microsoft.Extensions.Logging;

namespace MailSender.MAUI
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
            // Szolgáltatások hozzáadása
            builder.Services.AddTransient<EmailService>(x =>
            {
                return new EmailService(
                    "levelkuldo@vasvari.org", "Vasv4riLev3l", "smtp.gmail.com", 587);
            });
            builder.Services.AddSingleton<LogService>();

            builder.Services.AddTransient<LogViewModel>();
            builder.Services.AddTransient<MailViewModel>();

            builder.Services.AddTransient<LogPage>();
            builder.Services.AddTransient<MailPage>();

            return builder.Build();
        }
    }
}
