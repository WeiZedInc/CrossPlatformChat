using SQLite;

namespace CrossPlatformChat.Database.Entities
{
    [Table("clients")]
    public class ClientEntity : GeneralUserEntity
    {
        string _HashedPassword, _Login = null!, _Token = null!;
        DateTime _RegistrationTime;
        byte[] _StoredSalt = null!;

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
