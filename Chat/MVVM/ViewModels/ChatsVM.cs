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
        }

        public string GetLastMessageInChat(ChatModel chat)
        {
            // get last users message in chat by chat id
            return string.Empty;
        }

        public void InitClientDB()
        {
            App.Current.MainPage.DisplayAlert("ok", ClientManager.Instance.Client.Login, "ok").GetAwaiter();
        }
    }
}