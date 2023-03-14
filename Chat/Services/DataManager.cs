using CrossPlatformChat.MVVM.Models;
using System.Collections.ObjectModel;

namespace CrossPlatformChat.Services
{
    internal class DataManager
    {
        readonly ChatAppContext db;
        public static ObservableCollection<ChatInfoModel> AllChatsForClient { get; set; }
        public static ObservableCollection<UserInfoModel> AllUsersForClient { get; set; }
        public static ObservableCollection<MessageInfoModel> AllMessagesForClient { get; set; }

        public DataManager(ChatAppContext context)
        {
            db = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
