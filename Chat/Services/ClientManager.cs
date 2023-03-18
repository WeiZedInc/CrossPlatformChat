using CrossPlatformChat.MVVM.Models.Users;
using CrossPlatformChat.Services.Database;
using CrossPlatformChat.Utils;

namespace CrossPlatformChat.Services
{
    public class ClientManager
    {
        static ClientManager _instance;
        public ClientData Client;
        ISQLiteService db;

        public static ClientManager Instance { 
            get 
            {
                if (_instance == null) 
                    Init();

                return _instance; 
            } 
        }
        private ClientManager() { }

        static void Init()
        {
            try
            {
                _instance = new ClientManager();
                _instance.db = ServiceHelper.GetService<ISQLiteService>();
                var clientData = _instance.db.FirstOrDefault<ClientData>().Result;

                _instance.Client = clientData;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
