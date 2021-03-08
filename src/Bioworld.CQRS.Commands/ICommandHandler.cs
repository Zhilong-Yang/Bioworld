namespace Bioworld.CQRS.Commands
{
    using System.Threading.Tasks;

    public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
    {
        Task HandleAsync(TCommand command);
    }
}