using CrossPlatformChat.Database;
using CrossPlatformChat.MVVM.Models;
using CrossPlatformChat.MVVM.Models.Users;
using CrossPlatformChat.Services.Base;
using CrossPlatformChat.Services.Chat.Friends;
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
            if (_dbservice.TableToListAsync<ChatModel>().Result.Count == 0)
                NoChats = true;

            NewChatCMD = new Command(CreateChat);
        }

        void CreateChat()
        {
            ChatModel chat = new ChatModel()
            {
                CreatedDate = DateTime.Now,
                Name = "test",
                StoredKeyWord = new byte[] { 1, 2, 3 }
            };
        }

        public string GetLastMessageInChat(ChatModel chat)
        {
            // get last users message in chat by chat id
            return string.Empty;
        }
    }
}