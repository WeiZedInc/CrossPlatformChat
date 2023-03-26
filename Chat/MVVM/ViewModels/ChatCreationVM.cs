using CrossPlatformChat.MVVM.Models;
using CrossPlatformChat.Services.Base;
using CrossPlatformChat.Utils.Request_Response.Chat;

namespace CrossPlatformChat.MVVM.ViewModels
{
    internal class ChatCreationVM : ChatCreationModel
    {
        public async Task<bool> GetUserByUsername()
        {
            try
            {
                var request = new BaseRequest { Login = UsernameToAdd };
                var response = await APIManager.Instance.HttpRequest<UserEntityResponse>(request, RequestPath.GetUserByUsername, HttpMethod.Post);

                if (response.StatusCode == 200)
                {
                    await App.Current.MainPage.DisplayAlert("OK", response.Username, "ok");
                    return true;
                }
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
        }
    }
}
