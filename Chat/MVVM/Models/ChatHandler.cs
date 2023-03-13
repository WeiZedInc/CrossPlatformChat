using CommunityToolkit.Mvvm.ComponentModel;

namespace CrossPlatformChat.MVVM.Models
{
    partial class ChatHandler : ObservableObject
    {
        [ObservableProperty]
        int id;

        [ObservableProperty]
        int missedMsgsCount;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string usersID; //convert array of users id's to JSON

        [ObservableProperty]
        string messagesID; //convert array of messages id's to JSON

        [ObservableProperty]
        DateTime createdDate;
    }
}
