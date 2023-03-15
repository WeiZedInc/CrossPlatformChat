namespace CrossPlatformChat.MVVM.Models
{
    public enum MessageStatus
    {
        Read = 0,
        Received,
        SentAndNotRead
    }

    internal class MessageInfoModel : INotifyPropertyChanged
    {
        int _ID, _ChatID;
        string _SenderID, _Content;
        DateTime _SentDate;
        MessageStatus _Status;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; OnPropertyChanged(); }
        }
        public string SenderID
        {
            get { return _SenderID; }
            set { _SenderID = value; OnPropertyChanged(); }
        }
        public int ChatID
        {
            get { return _ChatID; }
            set { _ChatID = value; OnPropertyChanged(); }
        }
        public string Content
        {
            get { return _Content; }
            set { _Content = value; OnPropertyChanged(); }
        }
        public DateTime SentDate
        {
            get { return _SentDate; }
            set { _SentDate = value; OnPropertyChanged(); }
        }
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
