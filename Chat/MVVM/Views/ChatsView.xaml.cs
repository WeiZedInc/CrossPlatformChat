using CrossPlatformChat.MVVM.ViewModels;

namespace CrossPlatformChat.MVVM.Views;

public partial class ChatsView : ContentPage
{
	public ChatsView(ChatsVM chatsVM)
	{
		InitializeComponent();
		BindingContext = chatsVM;
	}
}