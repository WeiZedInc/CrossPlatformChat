using CrossPlatformChat.MVVM.ViewModels;

namespace CrossPlatformChat.MVVM.Views;

public partial class LoginView : ContentPage
{
	public LoginView()
	{
		InitializeComponent();
        App.Current.MainPage = this;
    }
}