using CrossPlatformChat.Database;
using CrossPlatformChat.MVVM.ViewModels;
using CrossPlatformChat.MVVM.Views;
using Microsoft.Extensions.Logging;

namespace CrossPlatformChat;

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

                fonts.AddFont("NotoSans-ExtraLight.ttf", "NotoSansExtraLight");
                fonts.AddFont("NotoSans-Light.ttf", "Light");
                fonts.AddFont("NotoSans-Medium.ttf", "NotoSansMedium");
                fonts.AddFont("NotoSans-Regular.ttf", "Regular");
                fonts.AddFont("NotoSans-Thin.ttf", "NotoSansThin");

                fonts.AddFont("MaterialIcons-Regular", "Icons");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        //db's
        builder.Services.AddSingleton<ISQLiteService, SQLiteService>();
        builder.Services.AddSingleton<APIManager>();
        builder.Services.AddSingleton<ClientHandler>();

        builder.Services.AddSingleton<ChatsCollectionVM>();
        builder.Services.AddSingleton<ChatCreationView>();


        return builder.Build();
    }
}
