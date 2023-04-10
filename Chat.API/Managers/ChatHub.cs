using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Chat.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chat.API.Managers
{
    public class ChatHub : Hub
    {
        public async Task SendMessageToGroup(string chatID, object messageEntity) // recieve and send back
        {
            await Clients.Group(chatID).SendAsync("ReceiveMessage", chatID, messageEntity);
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