using MySqlConnector;

namespace Hotel.Core.Domain.Repositories
{
    public interface IBaseRepository
    {
        Task<MySqlConnection> BeginTransaction();
    }

    public interface    IBaseRepository<TEntity> : IBaseRepository
    {
        Task<IReadOnlyList<TEntity>> GetAll();
        Task<TEntity> Insert(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> Delete(TEntity entity);
        Task<TEntity?> GetById(params object[] keyValues);
    }
}
