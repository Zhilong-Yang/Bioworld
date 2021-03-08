namespace Bioworld.CQRS.Queries
{
    public interface IPageQuery : IQuery
    {
        int Page { get; }

        int Results { get; }

        string OrderBy { get; }

        string SortOrder { get; }
    }
}