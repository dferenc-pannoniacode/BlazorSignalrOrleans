using BlazorSignalrOrleans.Grains.Interfaces;
using System.Threading.Tasks;

namespace BlazorSignalrOrleans.Grains
{
    public class MessageRelayGrain : Orleans.Grain, IMessageRelay
    {
        Task<string> IMessageRelay.RelayMessage(string user, string message)
        {
            return Task.FromResult($"MessageRelayGrain ({this.GrainReference.GrainIdentity.PrimaryKeyLong}): <{user}>: {message}");
        }
    }
}
