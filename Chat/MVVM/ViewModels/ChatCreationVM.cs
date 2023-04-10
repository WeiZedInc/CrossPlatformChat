using CrossPlatformChat.Database;
using CrossPlatformChat.Database.Entities;
using CrossPlatformChat.Helpers;
using CrossPlatformChat.MVVM.Models;
using CrossPlatformChat.MVVM.Views;
using CrossPlatformChat.Services.Base;
using CrossPlatformChat.Utils.Request_Response.Chat;
using Newtonsoft.Json;

namespace CrossPlatformChat.MVVM.ViewModels
{
    internal class ChatCreationVM : ChatCreationModel
    {
        bool _isSearchingUser = false;
        ClientEntity _localClient;
        public ChatCreationVM()
        {
            AddUserCMD = new Command(AddUser);
            RemoveUserCMD = new Command<GeneralUserEntity>(RemoveUser);
            CreateChatCMD = new Command(CreateChat);
            UsersToAdd = new();
            _localClient = ClientHandler.LocalClient;
        }

        public async void CreateChat()
        {
            try
            {
                if ((string.IsNullOrWhiteSpace(KeyWordInput) || KeyWordInput.Length < 4) || (string.IsNullOrWhiteSpace(ChatNameInput) || ChatNameInput.Length < 4))
                {
                    await App.Current.MainPage.DisplayAlert("Oops", "Keyword and chat name must contains at least of 4 symbols.", "Ok");
                    return;
                }

                string logo = "dotnet_bot.svg";
                string users = string.Empty;
                if (UsersToAdd.Count != 0)
                {
                    int[] UsersID = new int[UsersToAdd.Count];
                    for (int i = 0; i < UsersToAdd.Count; i++)
                        UsersID[i] = UsersToAdd[i].ID;

                    users = JsonConvert.SerializeObject(UsersID);
                }

                int ID = await CreateChatOnAPI(users, logo);
                if (ID == -1)
                {
                    await App.Current.MainPage.DisplayAlert("Oops", "Error on requesting chat ID from API", "Ok");
                    return;
                }

                ChatEntity chat = new()
                {
                    ID = ID,
                    CreatedDate = DateTime.Now,
                    MissedMessagesCount = 0,
                    Name = ChatNameInput,
                    StoredSalt = CryptoManager.CreateSalt(KeyWordInput),
                    GeneralUsersID_JSON = users,
                    LogoSource = logo
                };

                await ServiceHelper.Get<ISQLiteService>().InsertAsync(chat);
                ServiceHelper.Get<ChatsCollectionModel>().ChatsAndMessagessDict.Add(chat, new());
                App.Current.MainPage = new NavigationPage(new ChatsCollectionView());
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Oops", ex.Message + ex.StackTrace, "Ok");
            }
        }

        void RemoveUser(GeneralUserEntity user)
        {
            if (UsersToAdd.Contains(user))
                UsersToAdd.Remove(user);
        }

        async void AddUser()
        {
            try
            {
                if (_isSearchingUser) return;
                _isSearchingUser = true;

                if (UsersToAdd.Where(x => x.Username == UsernameToAdd).FirstOrDefault() != null)
                {
                    await App.Current.MainPage.DisplayAlert("Oops", $"You have already added {UsernameToAdd} to the chat!", "Ok");
                    return;
                }
                else if (UsernameToAdd == _localClient.Username) // _localClient == null after registration
                {
                    await App.Current.MainPage.DisplayAlert("Oops", "You are the owner of the chat, and will be there already!", "Ok");
                    return;
                }


                GeneralUserEntity user =  await GetUserByUsername(); //returns null on any second try add user
                if (user != null)
                    UsersToAdd.Add(user);
                else
                    await App.Current.MainPage.DisplayAlert("Oops", "There is no user with such username!", "Ok");

                UsernameToAdd = string.Empty;
                _isSearchingUser = false;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("ChatCreationVM: AddUser", ex.Message, "Ok");
                return;
            }
        }

        async Task<GeneralUserEntity> GetUserByUsername()
        {
            try
            {
                var existingUser = await ServiceHelper.Get<ISQLiteService>().FindUserByNameAsync<GeneralUserEntity>(UsernameToAdd); // saving user for further operations with found user
                if (existingUser is not null) 
                    return existingUser;

                var request = new BaseRequest { Login = UsernameToAdd, HashedPassword = string.Empty };
                var response = await ServiceHelper.Get<APIManager>().HttpRequest<UserEntityResponse>(request, RequestPath.GetUserByUsername, HttpMethod.Post);

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
                    await ServiceHelper.Get<ISQLiteService>().InsertAsync(newUser);

                    return newUser;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("ChatCreationVM: GetUserByUsername", ex.Message, "Ok");
                return null;
            }
        }

        async Task<int> CreateChatOnAPI(string users, string logo)
        {
            try
            {
                ChatInfoRequest chatInfoRequest = new() 
                {
                    AvatarSource = logo,
                    GeneralUsersID_JSON = users,
                    CreatedDate = DateTime.Now
                };

                var response = await ServiceHelper.Get<APIManager>().HttpRequest<ChatInfoResponse>(chatInfoRequest, RequestPath.CreateChat, HttpMethod.Post);
                if (response.StatusCode == 200)
                    return response.ID;
                else
                    return -1;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Oops", ex.Message + ex.StackTrace, "Ok");
                return -1;
            }
        }
    }
}
