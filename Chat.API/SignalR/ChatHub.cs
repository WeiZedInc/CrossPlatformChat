using Microsoft.AspNetCore.SignalR;

namespace Chat.API.SignalR
{
    public class ChatHub : Hub
    {
        public async Task Send(string username, string message)
        {
            await Clients.All.SendAsync("Receive", username, message);
        }
    }
}
