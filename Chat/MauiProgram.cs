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
				fonts.AddFont("NotoSans-Light.ttf", "NotoSansLight");
				fonts.AddFont("NotoSans-Medium.ttf", "NotoSansMedium");
				fonts.AddFont("NotoSans-Regular.ttf", "NotoSansRegular");
				fonts.AddFont("NotoSans-Thin.ttf", "NotoSansThin");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
