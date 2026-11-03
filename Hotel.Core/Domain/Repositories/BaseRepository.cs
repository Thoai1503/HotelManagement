using Hotel.Core.Domain.Entities;
using Hotel.Core.Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MySqlConnector;

namespace Hotel.Core.Domain.Repositories
{
    public abstract class BaseRepository : IBaseRepository
    {
        protected DbContext DbContext { get; }

        public MySqlConnection Connection { get; }

        public MySqlTransaction? Transaction { get; private set; }

        protected BaseRepository(DbContext dbContext)
        {
            DbContext = dbContext;

            if (dbContext.Database.GetDbConnection() is MySqlConnection mySqlConnection)
            {
                Connection = mySqlConnection;
            }
            else
            {
                throw new InvalidOperationException("The database connection is not a MySQL connection.");
            }
        }

        public DbSet<TEntity> GetDbSet<TEntity>(DbContext dbContext) where TEntity : class
        {
            return dbContext.Set<TEntity>();
        }

        public async Task<MySqlConnection> BeginTransaction()
        {
            if (Connection.State != System.Data.ConnectionState.Open)
            {
                await Connection.OpenAsync();
            }

            Transaction ??= await Connection.BeginTransactionAsync();
            return Connection;
        }
    }

    public abstract class BaseRepository<TModel, TEntity> : BaseRepository, IBaseRepository<TModel>
        where TModel : class
        where TEntity : class, new()
    {
        private DbSet<TEntity> Entities => DbContext.Set<TEntity>();

        protected BaseRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<TModel> Insert(TModel model)
        {
            ArgumentNullException.ThrowIfNull(model);

            var entity = new TEntity();
            var entityType = GetEntityType();
            EntityModelMapper.CopyToEntity(model, entity, entityType);
            await Entities.AddAsync(entity);
            await DbContext.SaveChangesAsync();

            EntityModelMapper.CopyToModel(entity, model, entityType);
            return model;
        }

        public async Task<TModel> Update(TModel model)
        {
            ArgumentNullException.ThrowIfNull(model);

            var entityType = GetEntityType();
            var entity = await FindEntityAsync(model, entityType);
            if (entity is null)
            {
                throw new KeyNotFoundException(
                    $"A {typeof(TEntity).Name} entity matching the supplied key was not found.");
            }

            EntityModelMapper.CopyToEntity(model, entity, entityType, skipKeys: true);
            await DbContext.SaveChangesAsync();

            EntityModelMapper.CopyToModel(entity, model, entityType);
            return model;
        }

        public async Task<TModel> Delete(TModel model)
        {
            ArgumentNullException.ThrowIfNull(model);

            var entityType = GetEntityType();
            var entity = await FindEntityAsync(model, entityType);
            if (entity is null)
            {
                throw new KeyNotFoundException(
                    $"A {typeof(TEntity).Name} entity matching the supplied key was not found.");
            }

            Entities.Remove(entity);
            await DbContext.SaveChangesAsync();
            return model;
        }

        public async Task<TModel?> GetById(params object[] keyValues)
        {
            ArgumentNullException.ThrowIfNull(keyValues);
            if (keyValues.Length == 0)
            {
                throw new ArgumentException("At least one key value is required.", nameof(keyValues));
            }

            var entity = await Entities.FindAsync(keyValues);
            if (entity is null)
            {
                return null;
            }

            var model = Activator.CreateInstance<TModel>();
            EntityModelMapper.CopyToModel(entity, model, GetEntityType());
            return model;
        }

        private IEntityType GetEntityType()
        {
            return DbContext.Model.FindEntityType(typeof(TEntity))
                ?? throw new InvalidOperationException(
                    $"The EF Core model does not contain entity type {typeof(TEntity).Name}.");
        }

        private async Task<TEntity?> FindEntityAsync(TModel model, IEntityType entityType)
        {
            var primaryKey = entityType.FindPrimaryKey()
                ?? throw new InvalidOperationException(
                    $"Entity type {typeof(TEntity).Name} does not have a primary key.");

            var keyValues = primaryKey.Properties
                .Select(property => EntityModelMapper.GetModelValue(model, property.Name))
                .ToArray();

            if (keyValues.Any(value => value is null))
            {
                throw new ArgumentException(
                    $"The model must provide all key values for {typeof(TEntity).Name}.",
                    nameof(model));
            }

            return await Entities.FindAsync(keyValues);
        }
    }
}
