using CrossPlatformChat.MVVM.Models.Chat;
using CrossPlatformChat.MVVM.Models.Users;
using CrossPlatformChat.Services.Base;
using CrossPlatformChat.Services.Chat.Friends;
using CrossPlatformChat.Services.Database;
using CrossPlatformChat.Utils.Helpers;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace CrossPlatformChat.MVVM.ViewModels
{
    public class ChatsVM
    {
        public bool NoChats { get; set; } = true;
        public bool HasFriends { get; set; } = false;
        public ICommand AddExternal { get; set; }
        public ICommand AddCurrent { get; set; }
        ClientData _localClient;

        public readonly ISQLiteService _dbservice;

        public ChatsVM()
        {
            _dbservice = ServiceHelper.GetService<ISQLiteService>();
            _localClient = ClientManager.Instance.Local;

            if (GetFriendsData().Result)
            {
                HasFriends = true;
                //show
            }
        }

        public string GetLastMessageInChat(ChatModel chat)
        {
            // get last users message in chat by chat id
            return string.Empty;
        }

        async Task<bool> GetFriendsData()
        {
            try
            {
                var request = new BaseRequest
                {
                    Login = _localClient.Login,
                    HashedPassword = _localClient.HashedPassword
                };
                var response = await APIManager.Instance.HttpRequest<FriendsResponse>(request, RequestPath.Authenticate, HttpMethod.Post);
                if (response.StatusCode == 200 && !string.IsNullOrWhiteSpace(response.FriendsJSON))
                {
                    ClientManager.Instance.Friends.AddRange(JsonConvert.DeserializeObject<List<GeneralUserData>>(response.FriendsJSON));
                    _localClient.FriendsJSON = response.FriendsJSON;
                    await _dbservice.UpdateAsync(_localClient);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "ok");
                return false;
            }
        }
    }
}