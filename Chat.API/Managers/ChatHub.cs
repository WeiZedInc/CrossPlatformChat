using Microsoft.AspNetCore.SignalR;

namespace Chat.API.Managers
{
    public class ChatHub : Hub
    {
        public async Task SendMessageToGroup(string chatID, object messageEntity) // recieve and send
        {
            //await Clients.Group(chatID).SendAsync("ReceiveMessage", messageEntity);
            await Clients.GroupExcept(chatID, Context.ConnectionId).SendAsync("ReceiveMessage", messageEntity);
            Console.ForegroundColor = ConsoleColor.Yellow;
            await Console.Out.WriteLineAsync($"Sending to group of chat[{chatID}]");
            Console.ResetColor();
        }

        public async Task SendChatToClient(string receiverID, object chat) // recieve and send chatinfo
        {
            await Clients.Group(receiverID).SendAsync("ReceiveChat", chat);
            Console.ForegroundColor = ConsoleColor.Yellow;
            await Console.Out.WriteLineAsync($"Sending to client id[{receiverID}]");
            Console.ResetColor();
        }

        public async Task AddToGroup(string chatID)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatID);
            Console.ForegroundColor = ConsoleColor.Green;
            await Console.Out.WriteLineAsync($"User {Context.ConnectionId} added to chat[{chatID}]");
            Console.ResetColor();
        }

        public async Task RemoveFromGroup(string chatID)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatID);
            Console.ForegroundColor = ConsoleColor.Red;
            await Console.Out.WriteLineAsync($"User {Context.ConnectionId} removed from chat[{chatID}]");
            Console.ResetColor();
        }

        public async Task AddToClientsGroup(string clientID)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, clientID);
            Console.ForegroundColor = ConsoleColor.Green;
            await Console.Out.WriteLineAsync($"User {Context.ConnectionId} added to clients group[{clientID}]");
            Console.ResetColor();
        }

        public async Task RemoveFromClientsGroup(string clientID)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, clientID);
            Console.ForegroundColor = ConsoleColor.Red;
            await Console.Out.WriteLineAsync($"User {Context.ConnectionId} removed from clients group[{clientID}]");
            Console.ResetColor();
        }
    }
}