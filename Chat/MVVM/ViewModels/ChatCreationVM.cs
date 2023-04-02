﻿using CrossPlatformChat.Database;
using CrossPlatformChat.Database.Entities;
using CrossPlatformChat.MVVM.Models;
using CrossPlatformChat.MVVM.Views;
using CrossPlatformChat.Services.Base;
using CrossPlatformChat.Utils.Helpers;
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
            _localClient = ServiceHelper.Get<ClientHandler>().LocalClient;
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

                (string Hash, byte[] Salt) key = new();
                key = CryptoManager.CreateHash(KeyWordInput);

                string users = string.Empty;
                if (UsersToAdd.Count != 0)
                {
                    int[] UsersID = new int[UsersToAdd.Count];
                    for (int i = 0; i < UsersToAdd.Count; i++)
                        UsersID[i] = UsersToAdd[i].ID;

                    users = JsonConvert.SerializeObject(default);
                }


                ChatEntity chat = new()
                {
                    ID = GenerateChatID(),
                    CreatedDate = DateTime.Now,
                    MissedMessagesCount = 0,
                    Name = ChatNameInput,
                    HashedKeyword = key.Hash,
                    StoredSalt = key.Salt,
                    GeneralUsersID_JSON = users,
                    LogoSource = "dotnet_bot.svg"
                };

                await ServiceHelper.Get<ISQLiteService>().InsertAsync(chat);
                ServiceHelper.Get<ChatsCollectionModel>().ChatsAndMessagessDict.Add(chat, new());
                App.Current.MainPage = new NavigationPage(ServiceHelper.Get<ChatsCollectionView>()); // dont work
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Oops", ex.Message + ex.StackTrace, "Ok");
            }
        }

        int GenerateChatID()
        {
            Random rnd = new Random();
            return Math.Abs(_localClient.Username.Length + rnd.Next() / (DateTime.Now.Second + 1));
        }

        public void RemoveUser(GeneralUserEntity user)
        {
            if (UsersToAdd.Contains(user))
                UsersToAdd.Remove(user);
        }

        public async void AddUser()
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
                else if (UsernameToAdd == _localClient.Username)
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

        public async Task<GeneralUserEntity> GetUserByUsername()
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
    }
}
