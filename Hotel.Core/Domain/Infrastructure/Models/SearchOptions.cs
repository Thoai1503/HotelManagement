namespace Hotel.Core.Domain.Infrastructure.Models
{
    public class SearchOptions<TEntity> 
    {
        public bool IsSearchAll { get; set; }

        public int Limit { get; set; }

        public int Offset { get; set; }

        public TEntity Example { get; set; }

        public IDictionary<string, string> Sort { get; set; }
    }
}
