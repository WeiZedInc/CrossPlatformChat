using CrossPlatformChat.MVVM.ViewModels;

namespace CrossPlatformChat.MVVM.Views;

public partial class ChatView : ContentPage
{
    ChatVM vm;
    public ChatView()
	{
		InitializeComponent();
        vm = BindingContext as ChatVM;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await vm.ChatHub.Connect();
    }

    protected override async void OnDisappearing()
    {
        base.OnDisappearing();
        await vm.ChatHub.Disconnect();
    }
}