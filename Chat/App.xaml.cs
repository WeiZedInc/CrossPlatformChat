using CrossPlatformChat.MVVM.Views;

namespace CrossPlatformChat;

public partial class App : Application
{
    public App()
	{
        InitializeComponent();

        NavigationPage page;
        //if (ClientManager.Instance.Local == null)
        //    page = new NavigationPage(new LoginView());
        //else
        //    page = new NavigationPage(new ChatsCollectionView());
        page = new NavigationPage(new ChatView());
        MainPage = page;
    }
}
