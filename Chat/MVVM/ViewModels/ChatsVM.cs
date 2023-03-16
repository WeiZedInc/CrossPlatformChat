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
            AddExternal = new Command(async () =>
            {
                await _dbservice.InsertAsync(new BaseUserModel
                {
                    AvatarSource = "test.png",
                    LastLoginTime = DateTime.Now,
                    IsOnline = false,
                    Username = "Test"
                });
                App.Current.MainPage.DisplayAlert("ok", _dbservice.TableToListAsync<BaseUserModel>().Result.Count.ToString(), "ok").GetAwaiter();
            });

            AddCurrent = new Command(async () =>
            {
                await _dbservice.InsertAsync(new ClientData
                {
                    AvatarSource = "test.png",
                    LastLoginTime = DateTime.Now,
                    IsOnline = false,
                    Username = "Test",
                });
                App.Current.MainPage.DisplayAlert("ok", _dbservice.TableToListAsync<ClientData>().Result.Count.ToString(), "ok").GetAwaiter();
            });
        }

        public string GetLastMessageInChat(ChatModel chat)
        {
            // get last users message in chat by chat id
            return string.Empty;
        }
    }
}
