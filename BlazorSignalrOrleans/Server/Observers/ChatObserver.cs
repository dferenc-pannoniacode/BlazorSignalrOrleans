using BlazorSignalrOrleans.Grains.Interfaces.Observers;
using BlazorSignalrOrleans.Grains.Interfaces;
using Microsoft.AspNetCore.SignalR;
using BlazorSignalrOrleans.Server.Hubs;
using Serilog;

namespace BlazorSignalrOrleans.Server.Observers
{
    public class ChatObserver : IChatObserver
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IGrainFactory _grainFactory;

        private IChatObserver? _obj;

        public ChatObserver(IHubContext<ChatHub> hubContext, IGrainFactory grainFactory)
        {
            _hubContext = hubContext;
            _grainFactory = grainFactory;
        }

        public async void ReceiveMessage(string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task Subscribe()
        {
            Log.Debug("ChatObserver subscribing to grain.");

            var messageRelayGrain = _grainFactory.GetGrain<IMessageRelayGrain>(ChatHub.InstanceGuid);

            _obj ??= _grainFactory.CreateObjectReference<IChatObserver>(this);

            await messageRelayGrain.Subscribe(_obj);
        }

        public async Task Unsubscribe()
        {
            if (_obj != null)
            {
                Log.Debug("ChatObserver unsubscribing from grain.");

                var messageRelayGrain = _grainFactory.GetGrain<IMessageRelayGrain>(ChatHub.InstanceGuid);

                await messageRelayGrain.Unsubscribe(_obj);
            }
        }
    }
}
