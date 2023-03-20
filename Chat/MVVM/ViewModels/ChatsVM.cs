using CrossPlatformChat.MVVM.Models.Chat;
using CrossPlatformChat.Services.Database;
using CrossPlatformChat.Utils.Helpers;

namespace CrossPlatformChat.MVVM.ViewModels
{
    public class ChatsVM
    {
        public bool NoChats { get; set; } = true;
        public ICommand AddExternal { get; set; }
        public ICommand AddCurrent { get; set; }

        public readonly ISQLiteService _dbservice;

        public ChatsVM()
        {
            _dbservice = ServiceHelper.GetService<ISQLiteService>();
        }

        public string GetLastMessageInChat(ChatModel chat)
        {
            // get last users message in chat by chat id
            return string.Empty;
        }
    }
}