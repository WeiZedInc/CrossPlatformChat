using CrossPlatformChat.MVVM.Models;
using CrossPlatformChat.MVVM.Views;
using CrossPlatformChat.Services.Base;

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
                var request = new BaseRequest
                {
                    Login = LoginInput,
                    HashedPassword = ClientManager.Instance.Local.HashedPassword
                };
                var response = await APIManager.Instance.HttpRequest<AuthenticationResponse>(request, RequestPath.Authenticate, HttpMethod.Post);

                if (response.StatusCode == 200) 
                    return true;
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", response.StatusMessage, "ok");
                    return false;
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "ok");
                return false;
            }
            finally
            {
                IsProcessing = false;
            }
        }
    }
}
