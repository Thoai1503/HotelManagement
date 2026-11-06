using Hotel.Core.Domain.Infrastructure.Models;

namespace Hotel.Core.Domain.Service
{
    public interface IBaseService
    {
        Task<SearchResult<TEntity>> Search<TEntity>(SearchOptions<TEntity> options);
        Task<TEntity> Create<TEntity>(TEntity entity);

        Task<TEntity> Update<TEntity>(TEntity entity);

        Task<TEntity?> GetById<TEntity>(Guid id);

        Task<IReadOnlyList<TEntity>> GetAll<TEntity>();
        Task<bool> Delete<TEntity>(TEntity entity);
    }
}
