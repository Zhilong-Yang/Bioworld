namespace Bioworld.WebApi.Requests
{
    using System.Threading.Tasks;

    public interface IRequestHandler<in TRequest, TResult> where TRequest:class, IRequest
    {
        Task<TResult> HandleAsync(TRequest request);
    }
}