using System;

namespace UnitOfWorkPattern
{
    public interface IEntity : IDisposable
    {
        Guid Id { get; }
    }
}
