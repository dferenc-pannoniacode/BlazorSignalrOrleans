using BlazorSignalrOrleans.Grains.Interfaces;
using BlazorSignalrOrleans.Grains.Interfaces.Observers;
using Microsoft.Extensions.Logging;
using Orleans.Runtime;
using Orleans.Utilities;

namespace BlazorSignalrOrleans.Grains
{
    public class MessageRelayGrain : Grain, IMessageRelayGrain
    {
        private readonly ObserverManager<IChatObserver> _subsManager;

        public MessageRelayGrain(ILogger<MessageRelayGrain> logger)
        {
            _subsManager =
                new ObserverManager<IChatObserver>(TimeSpan.FromMinutes(5), logger);
        }

        // Clients call this to subscribe.
        public Task Subscribe(IChatObserver observer)
        {
            _subsManager.Subscribe(observer, observer);

            return Task.CompletedTask;
        }

        //Clients use this to unsubscribe and no longer receive messages.
        public Task Unsubscribe(IChatObserver observer)
        {
            _subsManager.Unsubscribe(observer);

            return Task.CompletedTask;
        }

        public Task SendMessage(string user, string message)
        {
            _subsManager.Notify(s => s.ReceiveMessage($"MessageRelayGrain ({GrainReference.GrainId.GetGuidKey()}): <{user}>: {message}"));

            return Task.CompletedTask;
        }
    }
}
