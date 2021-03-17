using System.Threading.Tasks;

namespace Bioworld.MessageBrokers.RawRabbit
{
    public interface IMessageProcessor
    {
        Task<bool> TryProcessAsync(string id);
        Task RemoveAsync(string id);
    }
}