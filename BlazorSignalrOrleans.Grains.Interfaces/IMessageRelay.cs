using System.Threading.Tasks;

namespace BlazorSignalrOrleans.Grains.Interfaces
{
    public interface IMessageRelay : Orleans.IGrainWithIntegerKey
    {
        Task<string> RelayMessage(string user, string message);
    }
}
