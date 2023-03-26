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
                if (UsersToAdd.Where(x => x.Username == UsernameToAdd).FirstOrDefault() != null)
                {
                    await App.Current.MainPage.DisplayAlert("Oops", $"You have already added {UsernameToAdd} to the chat!", "Ok");
                    return;
                }
                else if (UsernameToAdd == ClientManager.Instance.Local.Username)
                {
                    await App.Current.MainPage.DisplayAlert("Oops", "You are the owner of the chat, and will be there already!", "Ok");
                    return;
                }

                GeneralUserEntity user = await GetUserByUsername();
                if (user != null)
                    UsersToAdd.Add(user);
                else
                    await App.Current.MainPage.DisplayAlert("Oops", "There is no user with such username!", "Ok");
            });

            RemoveUserCMD = new Command<GeneralUserEntity>(user =>
            {
                if (UsersToAdd.Contains(user))
                    UsersToAdd.Remove(user);
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
                    await App.Current.MainPage.DisplayAlert("ChatCreationVM (GetUserByUsername)", response.StatusMessage, "ok");
                    return null;
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("ChatCreationVM (GetUserByUsername)", ex.Message, "ok");
                return null;
            }
        }
    }
}
