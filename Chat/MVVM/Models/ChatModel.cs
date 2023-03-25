using CrossPlatformChat.Database;
using CrossPlatformChat.Database.Entities;
using CrossPlatformChat.Utils.Helpers;
using SQLite;
using System.Collections.ObjectModel;

namespace CrossPlatformChat.MVVM.Models
{
    [Table("chats")]
    public class ChatModel : ChatEntity
    {
        public bool NoChats { get; set; } = false;
        public static ObservableCollection<ChatModel> AllChats { get; set; }
        public ICommand NewChatCMD { get; set; }
        public readonly ISQLiteService _dbservice;

        public ChatModel()
        {
            _dbservice = ServiceHelper.GetService<ISQLiteService>();
            AllChats = new ObservableCollection<ChatModel>(_dbservice.TableToListAsync<ChatModel>().Result);
        }
    }
}
