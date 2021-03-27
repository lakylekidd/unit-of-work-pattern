using System;

namespace UnitOfWorkPattern
{
    public abstract class Entity : IEntity
    {
        protected Entity(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }

        public void Dispose()
        { }
    }
}
