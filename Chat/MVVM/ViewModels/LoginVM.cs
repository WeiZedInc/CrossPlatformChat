using CrossPlatformChat.MVVM.Models.Users;
using CrossPlatformChat.MVVM.Views;
using CrossPlatformChat.Utils;

namespace CrossPlatformChat.MVVM.ViewModels
{
    public class LoginVM : ClientModel
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
            GoToRegisterViewCommand = new Command(async () => await App.Current.MainPage.Navigation.PushAsync(new RegisterView(DependencyHelper.GetService<RegisterVM>()))); // remake
        }

        async Task<bool> TryLoginAsync()
        {
            try
            {
                var request = new AuthenticationRequest
                {
                    Login = LoginInput,
                    HashedPassword = ClientManager.Instance._data.HashedPassword
                };
                var response = await APIManager.Instance.Authenticate(request);
                if (response.StatusCode == 200)
                {
                    Test = $"Logined!\nUsername: {response.UserName}\nToken:{response.Token}"; // token not received
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
