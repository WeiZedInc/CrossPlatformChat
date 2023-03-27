using CrossPlatformChat.MVVM.ViewModels;

namespace CrossPlatformChat.MVVM.Views;

public partial class ChatsView : ContentPage
{
    public ChatsView()
	{
        InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Refresh the ItemsSource of the CollectionView
        var vm = (ChatsVM)BindingContext;
        if (vm.AllChats.Count == 0)
            vm.NoChats = true;
    }
}