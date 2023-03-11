using CrossPlatformChat.MVVM.Views;

namespace CrossPlatformChat.MVVM.ViewModels
{
    internal class LoginVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public LoginVM()
        {
            UserName = "";
            Password = "";
            IsProcessing = false;

            LoginCommand = new Command(() =>
            {
                if (IsProcessing) return;
                if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password)) return;
                IsProcessing = true;
                Login().GetAwaiter().OnCompleted(() => IsProcessing = false);
            });
        }

        async Task Login()
        {
            try
            {
                var request = new AuthenticationRequest
                {
                    Login = UserName,
                    Password = Password
                };
                var response = await ServiceProvider.Instance.Authenticate(request);
                if (response.StatusCode == 200)
                {
                    Test = $"Logined\n!Username: {response.UserName}\nToken:{response.Token}";
                }
                else
                    Test = response.StatusMessage;
            }
            catch (Exception ex)
            {
                Test = ex.Message;
            }

        }

        private string _username;
        private string _password;
        private string _test;
        private bool _isProcessing;
        public string Test
        {
            get { return _test; }
            set { _test = value; OnPropertyChanged(); }
        }
        public string UserName
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }
        public bool IsProcessing
        {
            get { return _isProcessing; }
            set { _isProcessing = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; set; }
    }
}
