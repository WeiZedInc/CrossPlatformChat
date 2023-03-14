using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrossPlatformChat.MVVM.Models
{
    [Table("currentuser")]
    internal class CurrentUserInfoModel : INotifyPropertyChanged
    {
        #region DataBase
        [Key] public int ID { get; set; }
        public string KeyWord { get; set; }
        public string HashedPassword { get; set; }
        public string Login { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string AvatarSource { get; set; } = "avatar.png";
        public bool IsOnline { get; set; }
        public DateTime LastLoginTime { get; set; }
        public DateTime RegistrationTime { get; set; }
        public byte[] StoreSalt { get; set; } = null!;
        #endregion

        #region BussinessLogic
        [NotMapped] private string _loginInput;
        [NotMapped] private string _passwordInput;
        [NotMapped] private string _test;
        [NotMapped] private bool _isProcessing;
        [NotMapped]
        public string Test
        {
            get { return _test; }
            set { _test = value; OnPropertyChanged(); }
        }
        [NotMapped]
        public string LoginInput
        {
            get { return _loginInput; }
            set { _loginInput = value; OnPropertyChanged(); }
        }
        [NotMapped]
        public string PasswordInput
        {
            get { return _passwordInput; }
            set { _passwordInput = value; OnPropertyChanged(); }
        }
        [NotMapped]
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
        #endregion
    }
}
