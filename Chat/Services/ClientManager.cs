using CrossPlatformChat.MVVM.Models.Users;
using CrossPlatformChat.Services.Database;
using CrossPlatformChat.Utils;

namespace CrossPlatformChat.Services
{
    public class ClientManager : ClientData
    {
        ClientManager _instance;
        ISQLiteService db;

        public ClientManager Instance { 
            get 
            {
                if (_instance == null) 
                    Init();

                return _instance; 
            } 
        }
        private ClientManager() { }

        void Init()
        {
            _instance = new ClientManager();
            _instance.db = DependencyHelper.GetService<ISQLiteService>();
            var clientData = db.TableToListAsync<ClientData>().Result;
            //_instance.Login = clientData.Where()
        }
    }
}
