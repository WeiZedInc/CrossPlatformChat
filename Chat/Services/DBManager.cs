using CrossPlatformChat.MVVM.Models;
using System.Collections.ObjectModel;

namespace CrossPlatformChat.Services
{
    internal class DBManager
    {
        readonly ChatAppContext db;
        public static ObservableCollection<ChatInfoModel> AllChatsForClient { get; set; }
        public static ObservableCollection<ExternalUsersInfoModel> AllUsersForClient { get; set; }
        public static ObservableCollection<MessageInfoModel> AllMessagesForClient { get; set; }

        public DBManager()
        {
            db = new ChatAppContext();
        }
    }
}
