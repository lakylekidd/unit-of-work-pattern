using System;
using System.Threading.Tasks;

namespace UnitOfWorkPattern
{
    public interface IRepository<TEntity> : IRepositoryBase
        where TEntity : IEntity
    {
        Task<TEntity> FindAsync(Guid id);
        Task CreateAsync(TEntity entity);
    }
}
