namespace Bioworld.WebApi.Requests
{
    using System.Threading.Tasks;

    public interface IRequestDispatcher
    {
        Task<TResult> DispatchAsync<TRequest, TResult>(TRequest request) where TRequest : class, IRequest;
    }
}