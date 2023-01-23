using BlazorSignalrOrleans.Grains.Interfaces.Observers;

namespace BlazorSignalrOrleans.Grains.Interfaces
{
    public interface IMessageRelayGrain : Orleans.IGrainWithGuidKey
    {
        Task SendMessage(string user, string message);

        public Task Unsubscribe(IChatObserver observer);

        public Task Subscribe(IChatObserver observer);
    }
}
