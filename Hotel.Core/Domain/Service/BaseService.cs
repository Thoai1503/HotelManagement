using Hotel.Core.Domain.Infrastructure.Models;
using Hotel.Core.Domain.Repositories;

namespace Hotel.Core.Domain.Service
{
    public class BaseService<TRepository> : IBaseService
    {

        public TRepository Repository { get; }

        public IBaseRepository   baseRepository { get; }

        private IHttpContextAccessor _httpContextAccessor;

        public  BaseService(TRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            Repository = repository;
            _httpContextAccessor = httpContextAccessor;
            baseRepository = (IBaseRepository)repository as IBaseRepository;
        }
        public Task<bool> Delete<TEntity>(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public  async Task<TEntity> Insert<TEntity>(TEntity entity)
        {
           return await (Repository as IBaseRepository<TEntity>).Insert(entity);
        }

        public Task<SearchResult<TEntity>> Search<TEntity>(SearchOptions<TEntity> options)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Update<TEntity>(TEntity entity)
        {
            return (Repository as IBaseRepository<TEntity>).Update(entity);
        }

        public Task<TEntity> Create<TEntity>(TEntity entity)
        {
            return (Repository as IBaseRepository<TEntity>).Insert(entity);
        }

        public Task<TEntity?> GetById<TEntity>(Guid id)
        {
            return (Repository as IBaseRepository<TEntity>).GetById(id);
        }

        public Task<IReadOnlyList<TEntity>> GetAll<TEntity>()
        {
           return (Repository as IBaseRepository<TEntity>).GetAll();
        }
    }
}
