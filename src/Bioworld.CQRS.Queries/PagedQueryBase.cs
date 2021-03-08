namespace Bioworld.CQRS.Queries
{
    public abstract class PagedQueryBase : IPageQuery
    {
        public int Page { get; set; }
        public int Results { get; set; }
        public string OrderBy { get; set; }
        public string SortOrder { get; set; }
    }
}