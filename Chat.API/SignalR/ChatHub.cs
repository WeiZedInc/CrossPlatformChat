﻿using Microsoft.AspNetCore.SignalR;

namespace Chat.API.SignalR
{
    public class ChatHub : Hub
    {
        public async Task MessageFromClient(object chatID, object messageEntity) // recieve and send back
        {
            await Clients.Others.SendAsync("MessageFromServer", chatID, messageEntity);
        }
    }
}
