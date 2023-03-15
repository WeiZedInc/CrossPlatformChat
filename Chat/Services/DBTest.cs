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
            //Get all notes.
            return database.Table<DBTestModel>().ToListAsync();
        }

        public Task<DBTestModel> GetTestAsync(int id)
        {
            // Get a specific note.
            return database.Table<DBTestModel>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveTestAsync(DBTestModel note)
        {
            if (note.ID != 0)
            {
                // Update an existing note.
                return database.UpdateAsync(note);
            }
            else
            {
                // Save a new note.
                return database.InsertAsync(note);
            }
        }

        public Task<int> DeleteTestAsync(DBTestModel note)
        {
            // Delete a note.
            return database.DeleteAsync(note);
        }
    }
}