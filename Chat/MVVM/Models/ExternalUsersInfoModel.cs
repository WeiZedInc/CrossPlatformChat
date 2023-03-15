using SQLite;

namespace CrossPlatformChat.MVVM.Models
{
    [Table("users")]
    internal class ExternalUsersInfoModel : INotifyPropertyChanged
    {
        int _ID;
        string _UserName = null!, _AvatarSource = "avatar.png";
        bool _isOnline;
        DateTime _LastLoginTime;

        [PrimaryKey, AutoIncrement, Unique]
        [Column("id")]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; OnPropertyChanged(); }
        }

        [Unique]
        [Column("username")]
        public string Username
        {
            get { return _UserName; }
            set { _UserName = value; OnPropertyChanged(); }
        }

        [Column("avatarSource")]
        public string AvatarSource
        {
            get { return _AvatarSource; }
            set { _AvatarSource = value; OnPropertyChanged(); }
        }

        [Column("isOnline")]
        public bool IsOnline
        {
            get { return _isOnline; }
            set { _isOnline = value; OnPropertyChanged(); }
        }

        [Column("lastLoginTime")]
        public DateTime LastLoginTime
        {
            get { return _LastLoginTime; }
            set { _LastLoginTime = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public class DBTestModel
    {
        [PrimaryKey, AutoIncrement, Unique]
        [Column("id")]
        public int ID { get;set; }

        [Unique]
        [Column("username")]
        public string Username { get; set; }

        [Column("avatarSource")]
        public string AvatarSource { get; set; }

        [Column("isOnline")]
        public bool IsOnline { get; set; }

        [Column("lastLoginTime")]
        public DateTime LastLoginTime { get; set; }
    }
}
