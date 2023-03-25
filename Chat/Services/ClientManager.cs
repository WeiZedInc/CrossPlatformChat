using CrossPlatformChat.Database;
using CrossPlatformChat.Database.Entities;
using CrossPlatformChat.Utils.Helpers;

namespace CrossPlatformChat.Services
{
    public class ClientManager
    {
        static ClientManager _instance;
        public ClientEntity? Local;
        public List<GeneralUserEntity> Friends;
        ISQLiteService db;

        public static ClientManager Instance { 
            get 
            {
                if (_instance == null)
                {
                    GetLocalData();
                    LoadFriends();
                }

                return _instance; 
            } 
        }
        private ClientManager() { }

        static void GetLocalData()
        {
            try
            {
                _instance = new ClientManager();
                _instance.db = ServiceHelper.GetService<ISQLiteService>();
                var clientData = _instance.db.FirstOrDefault<ClientEntity>().Result;

                _instance.Local = clientData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        static void LoadFriends()
        {
            _instance.Friends = new List<GeneralUserEntity>();
        }
    }
}
