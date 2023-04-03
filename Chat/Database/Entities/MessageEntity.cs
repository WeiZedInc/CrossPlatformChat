using SQLite;

namespace CrossPlatformChat.Database.Entities
{
    public enum MessageStatus
    {
        Read = 0,
        Received,
        SentAndNotRead
    }

    [Table("messages")]
    public class MessageEntity : INotifyPropertyChanged
    {
        int _ID, _ChatID, _SenderID;
        string _message;
        byte[] _EncryptedMessage, _InitialVector;
        DateTime _SentDate;
        MessageStatus _Status;

        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; OnPropertyChanged(); }
        }

        [Column("senderID")]
        public int SenderID
        {
            get { return _SenderID; }
            set { _SenderID = value; OnPropertyChanged(); }
        }

        [Column("chatID")]
        public int ChatID
        {
            get { return _ChatID; }
            set { _ChatID = value; OnPropertyChanged(); }
        }

        [Column("message")]
        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(); }
        }

        [Column("ecnryptedMessage")]
        public byte[] EncryptedMessage
        {
            get { return _EncryptedMessage; }
            set { _EncryptedMessage = value; OnPropertyChanged(); }
        }

        [Column("initialVector")]
        public byte[] InitialVector
        {
            get { return _InitialVector; }
            set { _InitialVector = value; OnPropertyChanged(); }
        }

        [Column("sentDate")]
        public DateTime SentDate
        {
            get { return _SentDate; }
            set { _SentDate = value; OnPropertyChanged(); }
        }

        [Column("status")]
        public MessageStatus Status
        {
            get { return _Status; }
            set { _Status = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
