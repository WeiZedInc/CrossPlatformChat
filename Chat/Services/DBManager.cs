using CrossPlatformChat.MVVM.Models;
using SQLite;

namespace CrossPlatformChat.Services
{
    internal class DBManager
    {
        public SQLiteAsyncConnection dbConnection;
        static DBManager _instance;
        public static DBManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DBManager();
                    _instance.InitializeDB().GetAwaiter();
                }

                return _instance;
            }
        }
        //public static ObservableCollection<ChatInfoModel> AllChatsForClient { get; set; }
        //public static ObservableCollection<ExternalUsersInfoModel> AllUsersForClient { get; set; }
        //public static ObservableCollection<MessageInfoModel> AllMessagesForClient { get; set; }
        //public static CurrentUserInfo CurrentUserInfo { get; set; }

        private DBManager() { }

        public async Task InitializeDB()
        {
            try
            {
                if (dbConnection != null) return;
                var DatabasePath = Path.Combine(FileSystem.AppDataDirectory, "ChatApp.db");
                dbConnection = new SQLiteAsyncConnection(DatabasePath);


                //await dbConnection.CreateTableAsync<ChatInfoModel>();
                //await dbConnection.CreateTableAsync<ExternalUsersInfoModel>();
                //await dbConnection.CreateTableAsync<MessageInfoModel>();
                //await dbConnection.CreateTableAsync<CurrentUserInfo>();
                await dbConnection.CreateTableAsync<DBTestModel>();

                //AllChatsForClient = new(GetChatsListAsync().Result);
                //AllUsersForClient = new(GetUsersListAsync().Result);
                //AllMessagesForClient = new(GetMessageListsAsync().Result);
                //CurrentUserInfo = GetCurrentUserInfoAsync().Result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<int> AddExternalUser(ExternalUsersInfoModel model)
        {
            if (model == null) return Task.FromException<int>(new ArgumentException(nameof(model)));
            return dbConnection?.InsertAsync(model);
        }

        public Task<int> AddDbTest(DBTestModel model)
        {
            if (model == null) return Task.FromException<int>(new ArgumentException(nameof(model)));
            return dbConnection?.InsertAsync(model);
        }

        public Task<List<ChatInfoModel>> GetChatsListAsync()
        {
            return dbConnection?.Table<ChatInfoModel>()?.ToListAsync();
        }
        public Task<List<ExternalUsersInfoModel>> GetUsersListAsync()
        {
            return dbConnection?.Table<ExternalUsersInfoModel>()?.ToListAsync();
        }
        public Task<List<MessageInfoModel>> GetMessageListsAsync()
        {
            return dbConnection?.Table<MessageInfoModel>()?.ToListAsync();
        }

        public Task<CurrentUserInfo> GetCurrentUserInfoAsync()
        {
            return dbConnection?.Table<CurrentUserInfo>()?.FirstOrDefaultAsync();
        }
    }
}
