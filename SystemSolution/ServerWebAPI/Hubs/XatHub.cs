using Microsoft.AspNetCore.SignalR;

namespace ServerWebAPI.Hubs
{
    public class XatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
