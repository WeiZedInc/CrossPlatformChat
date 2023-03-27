using CrossPlatformChat.MVVM.ViewModels;

namespace CrossPlatformChat.MVVM.Views;

public partial class ChatsCollectionView : ContentPage
{
    public ChatsCollectionView()
	{
        InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Refresh the ItemsSource of the CollectionView
        var vm = (ChatsCollectionVM)BindingContext;
        if (vm.AllChats.Count == 0)
            vm.NoChats = true;
    }
}