using Chat.API.Entities;
using Chat.API.Managers.Chat.Utils;

namespace Chat.API.Managers
{
    public interface IChatManager
    {
        ChatCreationResponse? CreateNewChat(ChatCreationRequest request);
    }
    public class ChatManager : IChatManager
    {
        private readonly ChatAppContext db;
        public ChatManager(ChatAppContext context) => db = context ?? throw new ArgumentNullException(nameof(context));


        public ChatCreationResponse? CreateNewChat(ChatCreationRequest request)
        {
            try
            {
                var chat = db.Chats.Add(new Chats()
                {
                    CreatedDate = request.CreatedDate,
                    GeneralUsersID_JSON = request.GeneralUsersID_JSON,
                    AvatarSource = request.AvatarSource
                });
                db.SaveChanges();

                return new ChatCreationResponse() { ID = chat.Entity.ID };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
