using BlazorSignalrOrleans.Grains.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace BlazorSignalrOrleans.Server.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public static readonly Guid InstanceGuid = Guid.NewGuid();

        private readonly IClusterClient _client;

        public ChatHub(IClusterClient client)
        {
            _client = client;
        }

        public async Task SendMessage(string message)
        {
            var messageRelayGrain = _client.GetGrain<IMessageRelayGrain>(InstanceGuid);
            await messageRelayGrain.SendMessage(Context.User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value ?? "Unknown user", message);
        }
    }
}
