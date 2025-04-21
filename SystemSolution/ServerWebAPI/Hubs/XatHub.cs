using Microsoft.AspNetCore.SignalR;

namespace ServerWebAPI.Hubs
{
    public class XatHub : Hub
    {
		public async Task EnviarMissatge(string message)
		{
			await Clients.All.SendAsync("ReceiveMessage", message);
		}
	}
}
