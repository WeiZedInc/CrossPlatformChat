using CrossPlatformChat.MVVM.Models.Users;
using CrossPlatformChat.MVVM.Views;

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
                    HashedPassword = ClientManager.Instance.Local.HashedPassword
                };
                var response = await APIManager.Instance.HttpRequest<AuthenticationResponse>(request, RequestPath.Authenticate);
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
