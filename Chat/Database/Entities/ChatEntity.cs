using SQLite;

namespace CrossPlatformChat.Database.Entities
{
    [Table("chats")]
    public class ChatEntity : INotifyPropertyChanged
    {
        int _ID, _MissedMessagesCount = 0;
        string _Name, _GeneralUsersID_JSON, _MessagesID, _LogoSource = "dotnet_bot.svg";
        DateTime _CreatedDate;
        string _HashedKeyword = string.Empty;
        byte[] _StoredSalt = null!;

        [PrimaryKey, AutoIncrement]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; OnPropertyChanged(); }
        }
        public int MissedMessagesCount
        {
            get { return _MissedMessagesCount; }
            set { _MissedMessagesCount = value; OnPropertyChanged(); }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged(); }
        }
        public string LogoSource
        {
            get { return _LogoSource; }
            set { _LogoSource = value; OnPropertyChanged(); }
        }
        public string GeneralUsersID_JSON
        {
            get { return _GeneralUsersID_JSON; }
            set { _GeneralUsersID_JSON = value; OnPropertyChanged(); }
        } //convert array of users id's to JSON
        public string MessagesID
        {
            get { return _MessagesID; }
            set { _MessagesID = value; OnPropertyChanged(); }
        } //convert array of messages id's to JSON
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; OnPropertyChanged(); }
        }
        public string HashedKeyword
        {
            get { return _HashedKeyword; }
            set { _HashedKeyword = value; OnPropertyChanged(); }
        }
        public byte[] StoredSalt
        {
            get { return _StoredSalt; }
            set { _StoredSalt = value; OnPropertyChanged(); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
