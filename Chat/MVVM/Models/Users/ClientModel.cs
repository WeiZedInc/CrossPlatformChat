using SQLite;

namespace CrossPlatformChat.MVVM.Models.Users
{
    [Table("client")]
    public class ClientData : GeneralUserData, INotifyPropertyChanged
    {
        string _KeyWord, _HashedPassword, _Password = null!, _Login = null!;
        DateTime _RegistrationTime;
        byte[] _StoredSalt = null!;

        public string KeyWord
        {
            get { return _KeyWord; }
            set { _KeyWord = value; OnPropertyChanged(); }
        }
        public string HashedPassword
        {
            get { return _HashedPassword; }
            set { _HashedPassword = value; OnPropertyChanged(); }
        }
        public string Login
        {
            get { return _Login; }
            set { _Login = value; OnPropertyChanged(); }
        }
        public string Password
        {
            get { return _Password; }
            set { _Password = value; OnPropertyChanged(); }
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

    public class ClientModel : ClientData, INotifyPropertyChanged
    {
        private string _loginInput;
        private string _passwordInput;
        private string _keyWordInput;
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
        public string KeyWordInput
        {
            get { return _keyWordInput; }
            set { _keyWordInput = value; OnPropertyChanged(); }
        }
        public bool IsProcessing
        {
            get { return _isProcessing; }
            set { _isProcessing = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public ClientModel()
        {
            LoginInput = string.Empty;
            PasswordInput = string.Empty;
            IsProcessing = false;
        }
    }
}
