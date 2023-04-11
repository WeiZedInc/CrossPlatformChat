using CrossPlatformChat.MVVM.ViewModels;

namespace CrossPlatformChat.MVVM.Views;

public partial class ChatRoomView : ContentPage
{
    ChatRoomVM vm;
    public ChatRoomView(int id)
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        vm = new ChatRoomVM();
        vm.InitChat(id);
    }

    protected override async void OnDisappearing()
    {
        base.OnDisappearing();
        await vm?.ChatHub?.Disconnect();
        vm = null;
    }
}