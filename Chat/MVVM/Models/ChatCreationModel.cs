using CrossPlatformChat.Database.Entities;

namespace CrossPlatformChat.MVVM.Models
{
    internal class ChatCreationModel : INotifyPropertyChanged
    {
        public string ChatNameInput { get; set; }
        public string KeyWordInput { get; set; }
        public List<GeneralUserEntity> UsersToAdd { get; set; }

        string _UsernameToAdd;
        public string UsernameToAdd
        {
            get { return _UsernameToAdd; }
            set { _UsernameToAdd = value; OnPropertyChanged(); }
        }

        bool _isSavingKeyword;
        public bool IsSavingKeyword
        {
            get { return _isSavingKeyword; }
            set { _isSavingKeyword = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public ChatCreationModel()
        {
            UsersToAdd = new();
        }
    }
}
