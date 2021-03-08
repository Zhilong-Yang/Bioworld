namespace Bioworld.CQRS.Commands
{
    using System.Threading.Tasks;

    public interface ICommandDispatcher : ICommand
    {
        Task SendAsync<T>(T command) where T : class, ICommand;
    }
}