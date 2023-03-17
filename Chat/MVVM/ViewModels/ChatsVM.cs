using CrossPlatformChat.MVVM.Models.Chat;
using CrossPlatformChat.MVVM.Models.Users;
using CrossPlatformChat.Services.Database;

namespace CrossPlatformChat.MVVM.ViewModels
{
    public class ChatsVM
    {
        public bool NoChats { get; set; } = true;
        public ICommand AddExternal { get; set; }
        public ICommand AddCurrent { get; set; }

        public readonly ISQLiteService _dbservice;

        public ChatsVM(ISQLiteService dbservice)
        {
            _dbservice = dbservice;

            #region For Testing
            AddExternal = new Command(async () =>
            {
                await _dbservice.InsertAsync(new GeneralUserData
                {
                    AvatarSource = "test.png",
                    LastLoginTime = DateTime.Now,
                    IsOnline = false,
                    Username = "Test"
                });
                App.Current.MainPage.DisplayAlert("ok", _dbservice.TableToListAsync<GeneralUserData>().Result.Count.ToString(), "ok").GetAwaiter(); //
            });

            _dbservice.DeleteAllInTableAsync<ClientData>(); //
            AddCurrent = new Command(async () =>
            {
                await _dbservice.InsertAsync(new ClientData
                {
                    AvatarSource = "test.png",
                    LastLoginTime = DateTime.Now,
                    IsOnline = false,
                    Username = "Test",
                    KeyWord = "Test"
                });
                App.Current.MainPage.DisplayAlert("ok", _dbservice.TableToListAsync<ClientData>().Result.Count.ToString(), "ok").GetAwaiter(); //
            });
            #endregion
        }

        public string GetLastMessageInChat(ChatModel chat)
        {
            // get last users message in chat by chat id
            return string.Empty;
        }
    }
}
