using CrossPlatformChat.MVVM.ViewModels;

namespace CrossPlatformChat.MVVM.Views;

public partial class ChatView : ContentPage
{
    ChatVM vm;
    public ChatView()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        vm = new ChatVM();
    }

    protected override async void OnDisappearing()
    {
        base.OnDisappearing();
        await vm?.ChatHub?.Disconnect();
        vm = null;
    }
}