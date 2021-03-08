namespace Bioworld.CQRS.Queries
{
    using System.Threading.Tasks;

    public interface IQueryHandler<in TQuery, TResult> where TQuery : class, IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}