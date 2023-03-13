using CrossPlatformChat.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformChat.MVVM.Models
{
    internal class ChatHandler : INotifyPropertyChanged
    {
        int _ID;
        string _ChatName;
        string _ChatLogo;
        int _LastMessageID;
        string _LastChatMessage;
        bool _IsOneByOne;
        int _UsersQuantity;
        DateTime _ChatCreatedDate;
        public int ID 
        {
            get => _ID;
            set { _ID = value; OnPropertyChanged(); }
        }
        public string ChatName
        {
            get => _ChatName;
            set { _ChatName = value; OnPropertyChanged(); }
        } 
        public string ChatLogo
        {
            get => _ChatLogo;
            set { _ChatLogo = value; OnPropertyChanged(); }
        } 
        public int LastMessageID
        {
            get => _LastMessageID;
            set { _LastMessageID = value; OnPropertyChanged(); }
        }
        public string LastChatMessage
        {
            get => _LastChatMessage;
            set { _LastChatMessage = value; OnPropertyChanged(); }
        }
        public bool IsOneByOne
        {
            get => _IsOneByOne;
            set { _IsOneByOne = value; OnPropertyChanged(); }
        }
        public int UsersQuantity
        {
            get => _UsersQuantity;
            set { _UsersQuantity = value; OnPropertyChanged(); }
        }
        public DateTime ChatCreatedDate
        {
            get => _ChatCreatedDate;
            set { _ChatCreatedDate = value; OnPropertyChanged(); }
        } 

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public ChatHandler()
        {
            _ChatName = "New chat";
            _ChatLogo = "dotnet_bot.svg";
            _LastChatMessage = "UserName: test";
            _ChatCreatedDate = DateTime.Now;
        }
    }
}
