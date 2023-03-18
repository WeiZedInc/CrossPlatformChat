using CrossPlatformChat.MVVM.Models.Users;
using CrossPlatformChat.MVVM.Views;
using CrossPlatformChat.Services.Database;
using CrossPlatformChat.Utils;

namespace CrossPlatformChat.MVVM.ViewModels
{
    public class LoginVM : ClientModel
    {
        public ICommand LoginCommand { get; set; }
        public ICommand GoToRegisterViewCommand { get; set; }
        public ICommand GoToChatsViewCommand { get; set; }

        public LoginVM() : base()
        {
            LoginCommand = new Command(async () =>
            {
                if (IsProcessing) return;
                if (string.IsNullOrWhiteSpace(LoginInput) || string.IsNullOrWhiteSpace(PasswordInput)) return; // remake checks to be optimal

                IsProcessing = true;
                if (await TryLoginAsync())
                {
                    await App.Current.MainPage.Navigation.PushAsync(new ChatsView());
                }
            });

            GoToRegisterViewCommand = new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new RegisterView());
            });
        }

        async Task<bool> TryLoginAsync()
        {
            try
            {
                var request = new AuthenticationRequest
                {
                    Login = LoginInput,
                    HashedPassword = ClientManager.Instance.Client.HashedPassword
                };
                var response = await APIManager.Instance.Authenticate(request);
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
            finally
            {
                IsProcessing = false;
            }
        }
    }
}
