using CrossPlatformChat.Database;
using CrossPlatformChat.Database.Entities;
using CrossPlatformChat.Helpers;

namespace CrossPlatformChat.Services
{
    public class ClientHandler
    {
        public static ClientEntity LocalClient { get; set; }

        static ClientHandler()
        {
            LocalClient = ServiceHelper.Get<ISQLiteService>().FirstOrDefault<ClientEntity>().Result;
        }
    }
}
