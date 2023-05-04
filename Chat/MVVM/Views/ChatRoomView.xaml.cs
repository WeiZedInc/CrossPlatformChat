using CrossPlatformChat.MVVM.ViewModels;

namespace CrossPlatformChat.MVVM.Views;

public partial class ChatRoomView : ContentPage
{
    ChatRoomVM vm;
    public ChatRoomView(ChatRoomVM vm)
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        this.vm = vm;
        BindingContext = vm;
    }

    protected override async void OnDisappearing()
    {
        base.OnDisappearing();
        await vm?.ChatHub?.Disconnect();
        vm = null;
    }
}