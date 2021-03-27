using System;
using System.Threading.Tasks;

namespace UnitOfWorkPattern
{
    public interface IUnitOfWorkBase : IDisposable
    {
        Task CommitAsync();
    }
}
 