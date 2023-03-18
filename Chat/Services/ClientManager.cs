using CrossPlatformChat.MVVM.Models.Users;
using CrossPlatformChat.Services.Database;
using CrossPlatformChat.Utils;

namespace CrossPlatformChat.Services
{
    public class ClientManager
    {
        static ClientManager _instance;
        public ClientData Local;
        public List<GeneralUserData> ExternalUsersList;
        ISQLiteService db;

        public static ClientManager Instance { 
            get 
            {
                if (_instance == null)
                {
                    GetLocalData();
                    GetExternalData();
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
                var clientData = _instance.db.FirstOrDefault<ClientData>().Result;

                _instance.Local = clientData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        static void GetExternalData()
        {
            _instance.ExternalUsersList = new List<GeneralUserData>();
        }
    }
}
