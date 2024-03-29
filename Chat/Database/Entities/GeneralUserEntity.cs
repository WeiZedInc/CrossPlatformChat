﻿using SQLite;

namespace CrossPlatformChat.Database.Entities
{
    [Table("users")]
    public class GeneralUserEntity : INotifyPropertyChanged
    {
        int _ID;
        string _UserName = null!, _AvatarSource = "dotnet_bot.svg";
        bool _isOnline;
        DateTime _LastLoginTime;

        [PrimaryKey]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; OnPropertyChanged(); }
        }
        public string Username
        {
            get { return _UserName; }
            set { _UserName = value; OnPropertyChanged(); }
        }
        public string AvatarSource
        {
            get { return _AvatarSource; }
            set { _AvatarSource = value; OnPropertyChanged(); }
        }
        public bool IsOnline
        {
            get { return _isOnline; }
            set { _isOnline = value; OnPropertyChanged(); }
        }
        public DateTime LastLoginTime
        {
            get { return _LastLoginTime; }
            set { _LastLoginTime = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
