using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UnitOfWorkPattern
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : Entity, IDisposable
    {
        protected readonly IContext Context;
        protected readonly DbSet<TEntity> Set;

        public Repository(IContext context)
        {
            Context = context;
            Set = Context.Set<TEntity>();
        }

        public virtual async Task CreateAsync(TEntity entity)
            => await Set.AddAsync(entity);

        public virtual async Task<TEntity> FindAsync(Guid id)
            => await Set.FirstOrDefaultAsync(x => x.Id.Equals(id));

        public void Dispose()
        { }
    }
}
