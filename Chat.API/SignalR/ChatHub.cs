using Microsoft.AspNetCore.SignalR;

namespace Chat.API.SignalR
{
    public class ChatHub : Hub
    {
        public async Task GetConnectionFromServer(string username, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", username, message);
            await Console.Out.WriteLineAsync(message);
        }
    }
}
