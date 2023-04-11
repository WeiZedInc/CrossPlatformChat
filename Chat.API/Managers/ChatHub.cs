using Microsoft.AspNetCore.SignalR;
using System.Drawing;

namespace Chat.API.Managers
{
    public class ChatHub : Hub
    {
        public async Task SendMessageToGroup(string chatID, object messageEntity) // recieve and send back
        {
            //await Clients.Group(chatID).SendAsync("ReceiveMessage", messageEntity);
            await Clients.GroupExcept(chatID, Context.ConnectionId).SendAsync("ReceiveMessage", messageEntity);
            Console.ForegroundColor = ConsoleColor.Yellow;
            await Console.Out.WriteLineAsync($"Sending to group of chat[{chatID}]");
            Console.ResetColor();
        }

        public async Task AddToGroup(string chatID)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            await Console.Out.WriteLineAsync($"User {Context.ConnectionId} added to chat[{chatID}]");
            Console.ResetColor();
            await Groups.AddToGroupAsync(Context.ConnectionId, chatID);
        }

        public async Task RemoveFromGroup(string chatID)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            await Console.Out.WriteLineAsync($"User {Context.ConnectionId} removed from chat[{chatID}]");
            Console.ResetColor();
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatID);
        }
    }
}