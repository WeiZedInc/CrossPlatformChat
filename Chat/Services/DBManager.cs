using CrossPlatformChat.MVVM.Models;
using SQLite;
using System.Collections.ObjectModel;

namespace CrossPlatformChat.Services
{
    internal class DBManager
    {
        //const SQLite.SQLiteOpenFlags Flags =
        //    SQLite.SQLiteOpenFlags.ReadWrite |
        //    SQLite.SQLiteOpenFlags.Create |
        //    SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, "ChatApp.db3");
            }
        }
        public static readonly SQLiteAsyncConnection dbConnection;
        public static ObservableCollection<ChatInfoModel> AllChatsForClient { get; set; }
        public static ObservableCollection<ExternalUsersInfoModel> AllUsersForClient { get; set; }
        public static ObservableCollection<MessageInfoModel> AllMessagesForClient { get; set; }
        public static CurrentUserInfo CurrentUserInfo { get; set; }

        public DBManager()
        {
            dbConnection.CreateTableAsync<ChatInfoModel>();
            dbConnection.CreateTableAsync<ExternalUsersInfoModel>();
            dbConnection.CreateTableAsync<MessageInfoModel>();
            dbConnection.CreateTableAsync<CurrentUserInfo>();

            AllChatsForClient = new(GetChatsListAsync().Result);
            AllUsersForClient = new(GetUsersListAsync().Result);
            AllMessagesForClient = new(GetMessageListsAsync().Result);
            CurrentUserInfo = GetCurrentUserInfoAsync().Result;
        }
        static DBManager() => dbConnection = new SQLiteAsyncConnection(DatabasePath) ?? throw new ArgumentNullException(nameof(dbConnection));

        public Task<List<ChatInfoModel>> GetChatsListAsync()
        {
            return dbConnection?.Table<ChatInfoModel>().ToListAsync();
        }
        public Task<List<ExternalUsersInfoModel>> GetUsersListAsync()
        {
            return dbConnection?.Table<ExternalUsersInfoModel>().ToListAsync();
        }
        public Task<List<MessageInfoModel>> GetMessageListsAsync()
        {
            return dbConnection?.Table<MessageInfoModel>().ToListAsync();
        }

        public Task<CurrentUserInfo> GetCurrentUserInfoAsync()
        {
            return dbConnection?.Table<CurrentUserInfo>().FirstOrDefaultAsync();
        }
    }
}
