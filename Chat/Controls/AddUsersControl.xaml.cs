using CrossPlatformChat.MVVM.Models;

namespace CrossPlatformChat.Controls;

public partial class AddUsersControl : ContentView
{
	public AddUsersControl()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty ChatCreationModelProperty = BindableProperty.Create(
        nameof(ChatCreationModel),
        typeof(ChatCreationModel),
        typeof(AddUsersControl),
        propertyChanged: OnChatCreationModelChanged);

    public ChatCreationModel ChatCreationModel
    {
        get => (ChatCreationModel)GetValue(ChatCreationModelProperty);
        set => SetValue(ChatCreationModelProperty, value);
    }

    static void OnChatCreationModelChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (AddUsersControl)bindable;
        control.BindingContext = newValue;
    }
}