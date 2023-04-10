using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace Chat.API.Managers
{
    public class ChatHub : Hub
    {
        private readonly ChatAppContext db;
        public ChatHub(ChatAppContext context) => db = context ?? throw new ArgumentNullException(nameof(context));

        public async Task MessageFromClient(int chatID, object messageEntity) // recieve and send back
        {
            //int[]? users = JsonConvert.DeserializeObject<int[]>(db.Chats.Find(chatID).GeneralUsersID_JSON);
            await Clients.Others.SendAsync("MessageFromServer", chatID, messageEntity);
        }
    }
}
