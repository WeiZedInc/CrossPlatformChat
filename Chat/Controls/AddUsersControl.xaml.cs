using CrossPlatformChat.Database.Entities;
using CrossPlatformChat.MVVM.Models;
using System.Collections.ObjectModel;

namespace CrossPlatformChat.Controls;

public partial class AddUsersControl : ContentView
{
	public AddUsersControl()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty UsersToAddProperty = BindableProperty.Create(
        nameof(UsersToAdd),
        typeof(ChatCreationModel),
        typeof(AddUsersControl),
        propertyChanged: OnChatCreationModelChanged);

    public static readonly BindableProperty RemoveUserCMDProperty = BindableProperty.Create(
        nameof(RemoveUserCMD),
        typeof(ChatCreationModel),
        typeof(AddUsersControl),
        propertyChanged: OnChatCreationModelChanged);

    public ChatCreationModel RemoveUserCMD
    {
        get => (ChatCreationModel)GetValue(RemoveUserCMDProperty);
        set => SetValue(RemoveUserCMDProperty, value);
    }

    public ChatCreationModel UsersToAdd
    {
        get => (ChatCreationModel)GetValue(UsersToAddProperty);
        set => SetValue(UsersToAddProperty, value);
    }

    static void OnChatCreationModelChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (AddUsersControl)bindable;
        control.BindingContext = newValue;
    }
}