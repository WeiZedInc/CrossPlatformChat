using CrossPlatformChat.Database.Entities;

namespace CrossPlatformChat.Database
{
    public interface ISQLiteService
    {
        Task<int> InsertAsync(object entity);
        Task<int> InsertAllAsync<T>(IEnumerable<T> entityCollection);

        Task<int> DeleteAsync(object entity);
        Task<int> DeleteAllInTableAsync<T>() where T : class, new();

        Task<int> UpdateAsync(object entity);
        Task<int> UpdateAllAsync<T>(IEnumerable<T> entityCollection);

        Task<List<T>> TableToListAsync<T>() where T : class, new();


        /// <summary>
        /// This method does not block the calling thread.
        /// </summary>
        /// <typeparam name="T">TableClass</typeparam>
        /// <param name="ID">entity primary key</param>
        /// <returns>null value if the entity is not found in the database, otherwise returns found entity</returns>
        Task<T> FindByIdAsync<T>(int ID) where T : class, new();

        /// <summary>
        /// This method does not block the calling thread.
        /// </summary>
        /// <typeparam name="T">TableClass</typeparam>
        /// <param name="Username">entity username</param>
        /// <returns>null value if the entity is not found in the database, otherwise returns found generalUser</returns>
        Task<T> FindUserByNameAsync<T>(string username) where T : GeneralUserEntity, new();


        /// <summary>
        /// This method blocks the callers thread.
        /// </summary>
        /// <typeparam name="T">TableClass</typeparam>
        /// <param name="ID">entity primary key</param>
        /// <returns>entity as soon as it is retrieved from the database, otherwise throws an exception if the entity is not found in the database</returns>
        Task<T> GetFromTableAsync<T>(int ID) where T : class, new();

        Task<T> FirstOrDefault<T>() where T : class, new();
    }
}
