using Microsoft.Extensions.Logging;
using SparkLogViewer.Core.Parsers;
using SparkLogViewer.Core.Services.Interfaces;
using SparkLogViewer.Infrastructure.Services.Implementations;
using SparkLogViewer.ViewModels;
using SparkLogViewer.Views;

namespace SparkLogViewer
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

            builder.Services.AddSingleton<ILogParser, MinecraftLogParser>()
                   .AddSingleton<ILogReaderService, LogFileReaderServices>();

            builder.Services.AddTransient<SparkLogViewerViewModel>()
                   .AddTransient<SparkLogViewerView>();

            return builder.Build();
        }
    }
}
