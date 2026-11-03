using Hotel.Core.Domain.Infrastructure.Models;

namespace Hotel.Core.Domain.Service
{
    public interface IBaseService
    {
        Task<SearchResult<TEntity>> Search<TEntity>(SearchOptions<TEntity> options);
        Task<TEntity> Insert<TEntity>(TEntity entity);

        Task<int> Update<TEntity>(TEntity entity);


        Task<bool> Delete<TEntity>(TEntity entity);
    }
}
