using SQLite;

namespace CrossPlatformChat.MVVM.Models
{
    internal class CurrentUserInfo : INotifyPropertyChanged
    {
        int _ID;
        string _KeyWord, _HashedPassword, _UserName = null!, _Password = null!, _Login = null!, _AvatarSource = "avatar.png";
        bool _isOnline;
        DateTime _LastLoginTime, _RegistrationTime;
        byte[] _StoredSalt = null!;

        [PrimaryKey, AutoIncrement, Unique]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; OnPropertyChanged(); }
        }
        public string KeyWord
        {
            get { return _KeyWord; }
            set { _KeyWord = value; OnPropertyChanged(); }
        }
        public string HashedPassword {
            get { return _HashedPassword; }
            set { _HashedPassword = value; OnPropertyChanged(); }
        }
        public string Login
        {
            get { return _Login; }
            set { _Login = value; OnPropertyChanged(); }
        }
        [Unique]
        public string Username
        {
            get { return _UserName; }
            set { _UserName = value; OnPropertyChanged(); }
        } 
        public string Password
        {
            get { return _Password; }
            set { _Password = value; OnPropertyChanged(); }
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
        public DateTime RegistrationTime
        {
            get { return _RegistrationTime; }
            set { _RegistrationTime = value; OnPropertyChanged(); }
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

    internal class CurrentUserInfoModel : CurrentUserInfo, INotifyPropertyChanged
    {
        private string _loginInput;
        private string _passwordInput;
        private string _test;
        private bool _isProcessing;
        public string Test
        {
            get { return _test; }
            set { _test = value; OnPropertyChanged(); }
        }
        public string LoginInput
        {
            get { return _loginInput; }
            set { _loginInput = value; OnPropertyChanged(); }
        }
        public string PasswordInput
        {
            get { return _passwordInput; }
            set { _passwordInput = value; OnPropertyChanged(); }
        }
        public bool IsProcessing
        {
            get { return _isProcessing; }
            set { _isProcessing = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public CurrentUserInfoModel()
        {
            LoginInput = string.Empty;
            PasswordInput = string.Empty;
            IsProcessing = false;
        }
    }
}
