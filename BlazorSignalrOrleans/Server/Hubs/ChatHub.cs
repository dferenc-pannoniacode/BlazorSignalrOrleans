using BlazorSignalrOrleans.Grains.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Orleans;
using System.Threading.Tasks;

namespace BlazorSignalrOrleans.Server.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IClusterClient _client;

        public ChatHub(IClusterClient client)
        {
            _client = client;
        }

        public async Task SendMessage(string user, string message)
        {
            var friend = _client.GetGrain<IMessageRelay>(0);
            var response = await friend.RelayMessage(user, message);
            await Clients.All.SendAsync("ReceiveMessage", user, response);
        }
    }
}
