using CrossPlatformChat.MVVM.Views;

namespace CrossPlatformChat;

public partial class App : Application
{
    static DBTest _instance;
    public static DBTest Instance
    {
        get
        {
            return _instance;
        }
    }

    public App()
	{
        _instance = new DBTest(Path.Combine(FileSystem.AppDataDirectory, "Test.db"));
        InitializeComponent();

        MainPage = new NavigationPage(new ChatsView());
    }
}
