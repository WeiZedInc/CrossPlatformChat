using CrossPlatformChat.MVVM.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformChat.Services
{
    public interface ISQLiteCRUD
    {
        Task<List<TestExternalUsers>> GetListAsync();
        Task<int> AddAsync(TestExternalUsers user);
        Task<int> DeleteAsync(TestExternalUsers user);
        Task<int> UpdateAsync(TestExternalUsers user);
    }

    internal class TestExternalUsersSerivece : ISQLiteCRUD
    {
        public SQLiteAsyncConnection connection;
        public TestExternalUsersSerivece()
        {
            if (connection == null) SetupConnection();
        }

        void SetupConnection()
        {
            string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TestExternalUsers.db3");
            connection = new SQLiteAsyncConnection(dbpath);
            connection.CreateTableAsync<TestExternalUsers>();
        }

        public Task<int> AddAsync(TestExternalUsers user)
        {
            return connection.InsertAsync(user);
        }

        public Task<int> DeleteAsync(TestExternalUsers user)
        {
            return connection.DeleteAsync(user);
        }

        public Task<List<TestExternalUsers>> GetListAsync()
        {
            var list = connection.Table<TestExternalUsers>().ToListAsync(); 
            return list;
        }

        public Task<int> UpdateAsync(TestExternalUsers user)
        {
            return connection.UpdateAsync(user);
        }
    }
}
