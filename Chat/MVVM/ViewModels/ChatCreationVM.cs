using CrossPlatformChat.Database.Entities;
using CrossPlatformChat.MVVM.Models;
using CrossPlatformChat.Services.Base;
using CrossPlatformChat.Utils.Request_Response.Chat;

namespace CrossPlatformChat.MVVM.ViewModels
{
    internal class ChatCreationVM : ChatCreationModel
    {
        public ChatCreationVM()
        {
            AddUserCMD = new Command(async () =>
            {
                if (UsernameToAdd == ClientManager.Instance.Local.Username || UsersToAdd.Where(x=> x.Username == UsernameToAdd).FirstOrDefault() != null)
                    return;

                GeneralUserEntity user = await GetUserByUsername();
                if (user != null)
                    UsersToAdd.Add(user);
            });
        }

        public async Task<GeneralUserEntity> GetUserByUsername()
        {
            try
            {
                var request = new BaseRequest { Login = UsernameToAdd, HashedPassword = string.Empty};
                var response = await APIManager.Instance.HttpRequest<UserEntityResponse>(request, RequestPath.GetUserByUsername, HttpMethod.Post);

                if (response.StatusCode == 200)
                {
                    await App.Current.MainPage.DisplayAlert("OK", response.Username, "ok");
                    return new GeneralUserEntity()
                    {
                        AvatarSource = response.AvatarSource,
                        ID = response.ID,
                        IsOnline = response.IsOnline,
                        LastLoginTime = response.LastLoginTime,
                        Username = response.Username
                    };
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", response.StatusMessage, "ok");
                    return null;
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("GetUserByUsername", ex.Message, "ok");
                return null;
            }
        }
    }
}
