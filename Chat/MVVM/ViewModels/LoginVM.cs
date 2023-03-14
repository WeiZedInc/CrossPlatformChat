using CrossPlatformChat.MVVM.Models;
using CrossPlatformChat.MVVM.Views;

namespace CrossPlatformChat.MVVM.ViewModels
{
    internal class LoginVM : UserInfoModel
    {
        public ICommand LoginCommand { get; set; }
        public ICommand GoToRegisterViewCommand { get; set; }
        public LoginVM() : base()
        {
            LoginCommand = new Command(() =>
            {
                if (IsProcessing) return;
                if (string.IsNullOrWhiteSpace(LoginInput) || string.IsNullOrWhiteSpace(PasswordInput)) return;

                IsProcessing = true;
                TryLoginAsync().GetAwaiter().OnCompleted(() => IsProcessing = false);  // can return login completion
            });
            GoToRegisterViewCommand = new Command(async () => await App.Current.MainPage.Navigation.PushAsync(new RegisterView()));
        }

        async Task<bool> TryLoginAsync()
        {
            try
            {
                var request = new AuthenticationRequest
                {
                    Login = LoginInput,
                    Password = PasswordInput
                };
                var response = await ServerProvider.Instance.Authenticate(request);
                if (response.StatusCode == 200)
                {
                    Test = $"Logined!\nUsername: {response.UserName}\nToken:{response.Token}";
                    return true;
                }
                else
                {
                    Test = response.StatusMessage;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Test = ex.Message;
                return false;
            }
        }
    }
}
