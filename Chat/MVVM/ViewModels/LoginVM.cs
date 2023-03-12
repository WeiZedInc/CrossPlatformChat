using CrossPlatformChat.MVVM.Models;
using CrossPlatformChat.MVVM.Views;

namespace CrossPlatformChat.MVVM.ViewModels
{
    internal class LoginVM
    {
        public BaseUserModel UserModel { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand GoToRegisterViewCommand { get; set; }
        public LoginVM()
        {
            UserModel = new BaseUserModel();
            LoginCommand = new Command(() =>
            {
                if (UserModel.IsProcessing) return;
                if (string.IsNullOrWhiteSpace(UserModel.LoginInput) || string.IsNullOrWhiteSpace(UserModel.PasswordInput)) return;

                UserModel.IsProcessing = true;
                TryLoginAsync().GetAwaiter().OnCompleted(() => UserModel.IsProcessing = false);  // can return login completion
            });
            GoToRegisterViewCommand = new Command(async () => await App.Current.MainPage.Navigation.PushAsync(new RegisterView()));
        }

        async Task<bool> TryLoginAsync()
        {
            try
            {
                var request = new AuthenticationRequest
                {
                    Login = UserModel.LoginInput,
                    Password = UserModel.PasswordInput
                };
                var response = await ServiceProvider.Instance.Authenticate(request);
                if (response.StatusCode == 200)
                {
                    UserModel.Test = $"Logined!\nUsername: {response.UserName}\nToken:{response.Token}";
                    return true;
                }
                else
                {
                    UserModel.Test = response.StatusMessage;
                    return false;
                }
            }
            catch (Exception ex)
            {
                UserModel.Test = ex.Message;
                return false;
            }
        }
    }
}
