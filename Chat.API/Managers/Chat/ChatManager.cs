using Chat.API.Entities;

namespace Chat.API.Managers.Chat
{
    public interface IChatManager
    {
        int CreateNewChat(ChatInfoRequest request);
    }
    public class ChatManager : IChatManager
    {
        private readonly ChatAppContext db;
        public ChatManager(ChatAppContext context) => db = context ?? throw new ArgumentNullException(nameof(context));


        public int CreateNewChat(ChatInfoRequest request)
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
