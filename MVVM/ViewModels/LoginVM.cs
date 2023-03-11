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
                    await AppShell.Current.DisplayAlert("Info", $"Logined\n!Username: {response.UserName}\nToken:{response.Token}", "OK");
                }
                else
                    await AppShell.Current.DisplayAlert("ChatApp", response.StatusMessage, "OK");
            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("ChatApp", ex.Message, "OK");
            }

        }

        private string _username;
        private string _password;
        private bool _isProcessing;

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
