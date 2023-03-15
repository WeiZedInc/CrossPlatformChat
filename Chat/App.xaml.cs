using CrossPlatformChat.MVVM.Views;

namespace CrossPlatformChat;

public partial class App : Application
{
    static DBTest database;
    public static DBTest Database
    {
        get
        {
            if (database == null)
            {
                database = new DBTest(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3"));
            }
            return database;
        }
    }

    public App()
	{
		InitializeComponent();

        MainPage = new NavigationPage(new ChatsView());
    }
}
