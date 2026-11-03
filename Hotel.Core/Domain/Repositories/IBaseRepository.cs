using MySqlConnector;

namespace Hotel.Core.Domain.Repositories
{
    public interface IBaseRepository
    {
        Task<MySqlConnection> BeginTransaction();
    }

    public interface IBaseRepository<T> : IBaseRepository where T : class
    {
         Task<T>  Insert (T entity);
        Task<T> Update (T entity);
        Task<T> Delete (T entity);
         
        Task<T> GetById (int id);

    }
}
