namespace Bioworld.CQRS.Commands.Dispatchers
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;

    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public CommandDispatcher(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task SendAsync<T>(T command) where T : class, ICommand
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<T>>();
            await handler.HandleAsync(command);
        }
    }
}