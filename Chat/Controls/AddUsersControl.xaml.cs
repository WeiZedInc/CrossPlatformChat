using CrossPlatformChat.Database.Entities;
using System.Collections.ObjectModel;

namespace CrossPlatformChat.Controls;

public partial class AddUsersControl : ContentView
{
	public AddUsersControl()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty RemoveUserCMDProperty = BindableProperty.Create(
    nameof(RemoveUserCMD),
    typeof(ICommand),
    typeof(AddUsersControl));

    public ICommand RemoveUserCMD
    {
        get => (ICommand)GetValue(RemoveUserCMDProperty);
        set => SetValue(RemoveUserCMDProperty, value);
    }


    public static readonly BindableProperty UsersToAddProperty = BindableProperty.Create(
    nameof(UsersToAdd),
    typeof(ObservableCollection<GeneralUserEntity>),
    typeof(AddUsersControl));

    public ObservableCollection<GeneralUserEntity> UsersToAdd
    {
        get => (ObservableCollection<GeneralUserEntity>)GetValue(UsersToAddProperty);
        set => SetValue(UsersToAddProperty, value);
    }
}