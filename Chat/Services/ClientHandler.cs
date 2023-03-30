using CrossPlatformChat.Database;
using CrossPlatformChat.Database.Entities;
using CrossPlatformChat.Utils.Helpers;

namespace CrossPlatformChat.Services
{
    public class ClientHandler
    {
        public ClientEntity LocalClient { get; set; }

        public ClientHandler()
        {
            LocalClient = ServiceHelper.Get<ISQLiteService>().FirstOrDefault<ClientEntity>().Result;
        }
    }
}
