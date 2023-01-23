using BlazorSignalrOrleans.Grains.Interfaces.Observers;
using Serilog;

namespace BlazorSignalrOrleans.Server.Services
{
    public class ChatObserverHostedService : IHostedService, IDisposable
    {
        private readonly IChatObserver _chatObserver;
        private Timer? _timer = null;

        public ChatObserverHostedService(IChatObserver chatObserver)
        {
            _chatObserver = chatObserver;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Log.Debug("ChatObserverHostedService starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(3));
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            Log.Debug("ChatObserverHostedService stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            await _chatObserver.Unsubscribe();
        }

        private async void DoWork(object? state)
        {
            Log.Debug("ChatObserverHostedService triggered.");

            await _chatObserver.Subscribe();
        }
    }
}
