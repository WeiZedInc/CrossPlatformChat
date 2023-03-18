using CrossPlatformChat.MVVM.ViewModels;

namespace CrossPlatformChat.MVVM.Views;

public partial class ChatsView : ContentPage
{
    bool loaded = false;
    ChatsVM chatsVM;
    public ChatsView(ChatsVM vm)
	{
        chatsVM = vm;

        InitializeComponent();
		BindingContext = chatsVM;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (loaded == false)
        {
            chatsVM.InitClientDB();
            loaded = true;
        }
    }
}