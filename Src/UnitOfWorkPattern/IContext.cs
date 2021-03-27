using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace UnitOfWorkPattern
{
    public interface IContext : IDisposable
    {
        EntityEntry Entry(object entity);
        Task<int> SaveChangesAsync();
        DbSet<TEntity> Set<TEntity>() where TEntity : class, IEntity;
    }
}
