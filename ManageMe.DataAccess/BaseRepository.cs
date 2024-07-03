using ManageMe.Common.Interfaces;

namespace ManageMe.DataAccess
{
    public class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly ApplicationDbContext Context;

        public BaseRepository(ApplicationDbContext context)
        {
            this.Context = context;
        }

        public IQueryable<TEntity> Get()
        {
            return Context.Set<TEntity>().AsQueryable();
        }

        public TEntity Insert(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            return entity;
        }

        public TEntity Update(TEntity entitty)
        {
            Context.Set<TEntity>().Update(entitty);

            return entitty;
        }

        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }
    }
}
