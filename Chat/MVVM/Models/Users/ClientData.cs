using SQLite;

namespace CrossPlatformChat.MVVM.Models.Users
{
    [Table("clients")]
    public class ClientData : GeneralUserData
    {
        string _KeyWord, _HashedPassword, _Password = null!, _Login = null!, _Token = null!;
        DateTime _RegistrationTime;
        byte[] _StoredSalt = null!;
        string _FriendsJSON = string.Empty;

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
        public string Token
        {
            get { return _Token; }
            set { _Token = value; OnPropertyChanged(); }
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
    }
}
