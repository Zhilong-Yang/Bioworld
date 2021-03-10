namespace Bioworld.CQRS.Events
{
    using System.Threading.Tasks;

    public interface IEventDispatcher
    {
        Task PublishAsync<T>(T @event) where T : class, IEvent;
    }
}