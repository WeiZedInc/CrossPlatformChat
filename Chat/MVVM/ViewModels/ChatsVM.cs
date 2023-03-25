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
        public bool NoChats { get; set; } = false;
        public ICommand NewChatCMD { get; set; }

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