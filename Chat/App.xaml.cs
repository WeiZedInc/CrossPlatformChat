using CrossPlatformChat.MVVM.Views;
using CrossPlatformChat.Utils.Helpers;

namespace CrossPlatformChat;

public partial class App : Application
{
    public App()
	{
        InitializeComponent();

        NavigationPage page;
        if (ClientHandler.LocalClient == null)
            page = new NavigationPage(new LoginView());
        else
            page = new NavigationPage(new ChatsCollectionView());
        //page = new NavigationPage(new ChatView());
        MainPage = page;
    }
}
