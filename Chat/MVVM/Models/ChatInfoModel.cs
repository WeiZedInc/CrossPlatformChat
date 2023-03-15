namespace CrossPlatformChat.MVVM.Models
{
    internal class ChatInfoModel : INotifyPropertyChanged
    {
        int _ID, _MissedMessagesCount = 0;
        string _Name, _UsersID, _MessagesID;
        DateTime _CreatedDate;
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
        public string UersID
        {
            get { return _UsersID; }
            set { _UsersID = value; OnPropertyChanged(); }
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
