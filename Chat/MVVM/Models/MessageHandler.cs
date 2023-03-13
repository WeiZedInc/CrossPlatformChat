using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformChat.MVVM.Models
{
    public enum MessageStatus
    {
        Read = 0,
        Received,
        SentAndNotRead
    }
    partial class MessageHandler : ObservableObject
    {
        [ObservableProperty]
        int id;

        [ObservableProperty]
        string userID; // who sent id

        [ObservableProperty]
        string text;

        [ObservableProperty]
        int chatID; // belongs to wich chat

        [ObservableProperty]
        DateTime sentDate;

        [ObservableProperty]
        MessageStatus status;
    }
}
