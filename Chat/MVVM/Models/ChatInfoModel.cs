using SQLite;

namespace CrossPlatformChat.MVVM.Models
{
    [Table("chats")]
    internal class ChatInfoModel : INotifyPropertyChanged
    {
        int _ID, _MissedMessagesCount = 0;
        string _Name, _UsersID, _MessagesID;
        DateTime _CreatedDate;
        byte[] _StoredKeyWord = null!;

        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; OnPropertyChanged(); }
        }

        [Column("missedMessagesCount")]
        public int MissedMessagesCount
        {
            get { return _MissedMessagesCount; }
            set { _MissedMessagesCount = value; OnPropertyChanged(); }
        }

        [Column("name")]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged(); }
        }

        [Column("usersID")]
        public string UsersID
        {
            get { return _UsersID; }
            set { _UsersID = value; OnPropertyChanged(); }
        } //convert array of users id's to JSON

        [Column("messagesID")]
        public string MessagesID
        {
            get { return _MessagesID; }
            set { _MessagesID = value; OnPropertyChanged(); }
        } //convert array of messages id's to JSON

        [Column("createdDate")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; OnPropertyChanged(); }
        }

        [Column("storedKeyWord")]
        public byte[] StoredKeyWord
        {
            get { return _StoredKeyWord; }
            set { _StoredKeyWord = value; OnPropertyChanged(); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
