using CrossPlatformChat.MVVM.Models;
using CrossPlatformChat.MVVM.Views;
using CrossPlatformChat.Services.Base;
using CrossPlatformChat.Utils.Helpers;

namespace CrossPlatformChat.MVVM.ViewModels
{
    public class LoginVM : ClientModel
    {
        public LoginVM()
        {
            LoginCommand = new Command(async () =>
            {
                if (IsProcessing) return;
                if (string.IsNullOrWhiteSpace(LoginInput) || string.IsNullOrWhiteSpace(PasswordInput)) return; // remake checks to be optimal

                IsProcessing = true;
                if (await TryLoginAsync())
                {
                    await App.Current.MainPage.Navigation.PushAsync(new ChatsCollectionView());
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
                    HashedPassword = ClientHandler.LocalClient.HashedPassword
                };
                var response = await ServiceHelper.Get<APIManager>().HttpRequest<AuthenticationResponse>(request, RequestPath.Authenticate, HttpMethod.Post);

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
