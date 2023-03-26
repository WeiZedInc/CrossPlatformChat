using CrossPlatformChat.MVVM.Models;
using CrossPlatformChat.Services.Base;
using CrossPlatformChat.Utils.Request_Response.Chat;

namespace CrossPlatformChat.MVVM.ViewModels
{
    internal class ChatCreationVM : ChatCreationModel
    {
        public async Task<bool> GetUserByUsername(object sender, EventArgs e)
        {
            try
            {
                var request = new BaseRequest { Login = UserIDToAdd };
                var response = await APIManager.Instance.HttpRequest<UserEntityResponse>(request, RequestPath.GetUserByUsername, HttpMethod.Get);

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
        }
    }
}
