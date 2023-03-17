using CrossPlatformChat.MVVM.Models.Users;
using CrossPlatformChat.Services.Database;
using CrossPlatformChat.Utils;

namespace CrossPlatformChat.Services
{
    public class ClientManager
    {
        static ClientManager _instance;
        ClientData _data;
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
            _instance = new ClientManager();
            _instance.db = DependencyHelper.GetService<ISQLiteService>();
            var clientData = _instance.db.FirstOrDefault<ClientData>().Result;

            _instance._data = clientData;
        }
    }
}
