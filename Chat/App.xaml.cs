using CrossPlatformChat.MVVM.ViewModels;
using CrossPlatformChat.MVVM.Views;

namespace CrossPlatformChat;

public partial class App : Application
{
    public App(ChatsVM vm)
	{
        InitializeComponent();

        MainPage = new NavigationPage(new ChatsView(vm));
    }
}
