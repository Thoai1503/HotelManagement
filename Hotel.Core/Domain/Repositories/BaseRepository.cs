using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace Hotel.Core.Domain.Repositories
{
    public abstract class BaseRepository : IBaseRepository
    {
        public MySqlConnection Connection { get; }

        public MySqlTransaction Transaction { get; private set; }

        public BaseRepository(DbContext dbContext)
        {

            if (dbContext.Database.GetDbConnection() is MySqlConnection mySqlConnection)
            {
                Connection = mySqlConnection;
                Transaction = Connection.BeginTransaction();
            }
            else
            {
                throw new InvalidOperationException("The database connection is not a MySQL connection.");
            }
           
        }

      
        public Task<MySqlConnection> BeginTransaction()
        {
           
            if (Connection.State != System.Data.ConnectionState.Open)
            {
                Connection.Open();
            }
            Transaction = Connection.BeginTransaction();
            return Task.FromResult(Connection);
        }
    }
    public abstract class BaseRepository<T> : BaseRepository , IBaseRepository<T> where T : class
    {
        public BaseRepository(DbContext dbContext) : base(dbContext) 
        { 
        
        }

        public Task<T> Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
