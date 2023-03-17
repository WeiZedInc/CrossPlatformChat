using CrossPlatformChat.MVVM.ViewModels;
using CrossPlatformChat.MVVM.Views;
using CrossPlatformChat.Services.Database;
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

        //views
        builder.Services.AddSingleton<ChatsView>();
		builder.Services.AddTransient<LoginView>();
		builder.Services.AddTransient<RegisterView>();

        //viewmodels
        builder.Services.AddSingleton<ChatsVM>();
        builder.Services.AddTransient<LoginVM>();
        builder.Services.AddTransient<RegisterVM>();

        //db's
        builder.Services.AddSingleton<ISQLiteService, SQLiteService>();


        return builder.Build();
	}
}
