namespace Bioworld.CQRS.Events.Dispatchers
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;

    public class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public EventDispatcher(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task PublishAsync<T>(T @event) where T : class, IEvent
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var handlers = scope.ServiceProvider.GetServices<IEventHandler<T>>();
                foreach (var handler in handlers)
                {
                    await handler.HandleAsync(@event);
                }
            }
        }
    }
}