namespace Hotel.Core.Domain.Infrastructure.Models
{
    public class SearchResult<TEntity> 
    {
        public IEnumerable<TEntity> EntityList { get; set; }

        public int Count { get; set; }

        public int PageIndex { get; set; }

        public int TotalPages { get; set; }
    }
}
