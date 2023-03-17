using CrossPlatformChat.MVVM.ViewModels;

namespace CrossPlatformChat.MVVM.Views;

public partial class RegisterView : ContentPage
{
	public RegisterView(RegisterVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
        NavigationPage.SetHasNavigationBar(this, false);
    }
}