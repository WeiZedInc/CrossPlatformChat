using CrossPlatformChat.Database;
using CrossPlatformChat.Database.Entities;
using CrossPlatformChat.MVVM.Models;
using CrossPlatformChat.Services.Base;
using CrossPlatformChat.Utils.Helpers;
using CrossPlatformChat.Utils.Request_Response.Chat;
using Newtonsoft.Json;

namespace CrossPlatformChat.MVVM.ViewModels
{
    internal class ChatCreationVM : ChatCreationModel
    {
        public ChatCreationVM()
        {
            AddUserCMD = new Command(AddUser);
            RemoveUserCMD = new Command<GeneralUserEntity>(RemoveUser);
            CreateChatCMD = new Command(CreateChat);
        }

        public async void CreateChat()
        {
            try
            {
                if (KeyWordInput.Length < 4 || ChatNameInput.Length < 4)
                {
                    await App.Current.MainPage.DisplayAlert("Oops", "Keyword and chat name must contains at least of 4 symbols.", "Ok");
                    return;
                }

                (string Hash, byte[] Salt) key = new();
                key = CryptoManager.CreateHash(KeyWordInput);


                int[] UsersID = new int[UsersToAdd.Count];
                for (int i = 0; i < UsersToAdd.Count; i++)
                    UsersID[i] = UsersToAdd[i].ID;

                ChatEntity chat = new ChatEntity()
                {
                    ID = GenerateChatID(),
                    CreatedDate = DateTime.Now,
                    MissedMessagesCount = 0,
                    Name = ChatNameInput,
                    HashedKeyword = key.Hash,
                    StoredSalt = key.Salt,
                    MessagesID = string.Empty,
                    GeneralUsersID_JSON = JsonConvert.SerializeObject(UsersID)
                };

                await ServiceHelper.GetService<ISQLiteService>().InsertAsync(chat);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Oops", ex.Message, "Ok");
            }
        }

        int GenerateChatID()
        {
            Random rnd = new Random();
            return Math.Abs(ClientManager.Instance.Local.Username.Length + rnd.Next() / (DateTime.Now.Second + 1) - UsernameToAdd.Length);
        }

        public void RemoveUser(GeneralUserEntity user)
        {
            if (UsersToAdd.Contains(user))
                UsersToAdd.Remove(user);
        }

        public async void AddUser()
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

            UsernameToAdd = string.Empty;
        }

        public async Task<GeneralUserEntity> GetUserByUsername() // refactor to make check database on containing of user before request the api
        {
            var request = new BaseRequest { Login = UsernameToAdd, HashedPassword = string.Empty };
            var response = await APIManager.Instance.HttpRequest<UserEntityResponse>(request, RequestPath.GetUserByUsername, HttpMethod.Post);

            if (response.StatusCode == 200)
            {
                var newUser = new GeneralUserEntity()
                {
                    ID = response.ID,
                    AvatarSource = response.AvatarSource,
                    IsOnline = response.IsOnline,
                    LastLoginTime = response.LastLoginTime,
                    Username = response.Username
                };

                if (ServiceHelper.GetService<ISQLiteService>().FindInTableAsync<GeneralUserEntity>(newUser.ID)?.Result != null) // saving user for further operations with found user
                    await ServiceHelper.GetService<ISQLiteService>().InsertAsync(newUser);

                return newUser;
            }
            else
                return null;

        }
    }
}
