using Microsoft.AspNetCore.SignalR;

namespace Chat.API.SignalR
{
    public class ChatHub : Hub
    {
        public async Task SendMessageToAll(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
            await Console.Out.WriteLineAsync(message);
        }
    }
}
