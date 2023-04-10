using Chat.API.Entities;
using Chat.API.Managers.Chat.Utils;

namespace Chat.API.Managers
{
    public interface IChatManager
    {
        int CreateNewChat(ChatCreationRequest request);
    }
    public class ChatManager : IChatManager
    {
        private readonly ChatAppContext db;
        public ChatManager(ChatAppContext context) => db = context ?? throw new ArgumentNullException(nameof(context));


        public int CreateNewChat(ChatCreationRequest request)
        {
            try
            {
                return db.Chats.Add(new Chats()
                {
                    CreatedDate = request.CreatedDate,
                    GeneralUsersID_JSON = request.GeneralUsersID_JSON,
                    AvatarSource = request.AvatarSource
                }).Entity.ID;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
