using CrossPlatformChat.MVVM.Models.Users;
using CrossPlatformChat.MVVM.Views;
using CrossPlatformChat.Services.Database;
using CrossPlatformChat.Utils;

namespace CrossPlatformChat;

public partial class App : Application
{
    public App()
	{
        InitializeComponent();

        NavigationPage page;
        if (ClientManager.Instance.Client == null)
            page = new NavigationPage(new LoginView());
        else
            page = new NavigationPage(new ChatsView());

        MainPage = page;
    }
}
