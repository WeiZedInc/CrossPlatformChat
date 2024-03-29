﻿using CrossPlatformChat.Database.Entities;
using SQLite;

namespace CrossPlatformChat.Database
{
    internal class SQLiteService : ISQLiteService
    {
        public SQLiteAsyncConnection connection;
        public SQLiteService()
        {
            if (connection == null)
                SetupConnection();
        }

        void SetupConnection()
        {
            string dbpath = Path.Combine(FileSystem.AppDataDirectory, "localdata.db3");
            connection = new SQLiteAsyncConnection(dbpath);

            connection.CreateTablesAsync<ClientEntity, ChatEntity, MessageEntity, GeneralUserEntity>().Wait();
        }

        public Task<int> InsertAsync(object entity) => connection.InsertAsync(entity);

        public Task<int> DeleteAsync(object entity) => connection.DeleteAsync(entity);

        public Task<int> UpdateAsync(object entity) => connection.UpdateAsync(entity);

        public Task<T> FirstOrDefault<T>() where T : class, new()
        {
            return connection.Table<T>().FirstOrDefaultAsync();
        }

        public Task<int> InsertAllAsync<T>(IEnumerable<T> entityCollection)
        {
            return connection.InsertAllAsync(entityCollection);
        }
        public Task<int> UpdateAllAsync<T>(IEnumerable<T> entityCollection)
        {
            return connection.UpdateAllAsync(entityCollection);
        }

        public Task<int> DeleteAllInTableAsync<T>() where T : class, new()
        {
            return connection.DeleteAllAsync<T>();
        }

        public Task<List<T>> TableToListAsync<T>() where T : class, new()
        {
            return connection.Table<T>().ToListAsync();
        }

        public Task<T> FindByIdAsync<T>(int ID) where T : class, new()
        {
            return connection.FindAsync<T>(ID);
        }

        public Task<T> FindUserByNameAsync<T>(string username) where T : GeneralUserEntity, new()
        {
            return connection.FindAsync<T>(x => x.Username == username);
        }

        public Task<T> GetFromTableAsync<T>(int ID) where T : class, new()
        {
            return connection.GetAsync<T>(ID);
        }
    }
}
