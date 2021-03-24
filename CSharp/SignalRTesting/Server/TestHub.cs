using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Shared;

namespace Server
{
    [AllowAnonymous]
    public class TestHub : Hub
    {
        public void SendMessage(TheMessage message)
        {
            Clients.All.SendAsync("ReceiveMessage", JsonSerializer.Serialize(message));
        }
    }
}
