namespace CrossPlatformChat.MVVM.Models
{
    internal class BaseUserModel : INotifyPropertyChanged
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

        public BaseUserModel()
        {
            LoginInput = string.Empty;
            PasswordInput = string.Empty;
            IsProcessing = false;
        }
    }
}
