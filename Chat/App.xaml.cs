using CrossPlatformChat.MVVM.Views;

namespace CrossPlatformChat;

public partial class App : Application
{
    public App()
	{
        InitializeComponent();

        MainPage = new NavigationPage(new LoginView());
    }
}
