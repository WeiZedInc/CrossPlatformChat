using CrossPlatformChat.MVVM.Models;
using SQLite;

namespace CrossPlatformChat.Services
{
    public class DBTest
    {
        readonly SQLiteAsyncConnection database;

        public DBTest(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<DBTestModel>().Wait();
        }

        public Task<List<DBTestModel>> GetTestsAsync()
        {
            return database.Table<DBTestModel>().ToListAsync();
        }

        public Task<DBTestModel> GetTestAsync(int id)
        {
            return database.Table<DBTestModel>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveTestAsync(DBTestModel note)
        {
            return database.InsertOrReplaceAsync(note);
        }

        public Task<int> DeleteTestAsync(DBTestModel note)
        {
            return database.DeleteAsync(note);
        }

        public Task<List<SQLiteConnection.ColumnInfo>> GetInfo(string tableName)
        {
            return database.GetTableInfoAsync(tableName);
        }
    }
}