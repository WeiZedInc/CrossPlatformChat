namespace CrossPlatformChat.MVVM.Views;

public partial class RegisterView : ContentPage
{
	public RegisterView()
	{
		InitializeComponent();
        App.Current.MainPage = this;
    }
}