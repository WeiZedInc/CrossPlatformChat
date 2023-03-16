using SQLite;

namespace CrossPlatformChat.MVVM.Models
{
    public enum MessageStatus
    {
        Read = 0,
        Received,
        SentAndNotRead
    }
    [Table("messages")]
    internal class MessageInfoModel : INotifyPropertyChanged
    {
        int _ID, _ChatID;
        string _SenderID, _Content;
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
        public string SenderID
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

        [Column("content")]
        public string Content
        {
            get { return _Content; }
            set { _Content = value; OnPropertyChanged(); }
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
