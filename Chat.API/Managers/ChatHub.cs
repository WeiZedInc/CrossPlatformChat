using Microsoft.AspNetCore.SignalR;

namespace Chat.API.Managers
{
    public class ChatHub : Hub
    {
        public async Task SendMessageToGroup(string chatID, object messageEntity) // recieve and send back
        {
            await Clients.Group(chatID).SendAsync("ReceiveMessage", messageEntity);
        }

        public async Task AddToGroup(string chatID)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatID);
        }

        public async Task RemoveFromGroup(string chatID)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatID);
        }
    }
}