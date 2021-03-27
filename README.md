﻿# Unit of Work Pattern

This repository implements a very generic unit of work pattern that uses the repository pattern at its base.

## How to Use

The following gist is an example on how to use the current Unit of Work pattern:

```CSharp
public interface IFooContext : IContext
{ }

public class BarEntity : Entity
{
    public BarEntity(Guid id) 
        : base(id)
    { }
}

public interface IFooRepository 
    : IRepository<BarEntity>
{ }

public class FooRepository : Repository<BarEntity>,
    IFooRepository
{
    public FooRepository(IContext context) 
        : base(context)
    { }
}

public interface IUnitOfWork : IUnitOfWorkBase
{
    IFooRepository FooRepository { get; }
}

public class UnitOfWork : UnitOfWorkBase<IFooContext>, IUnitOfWork
{
    public UnitOfWork(IFooContext context)
        : base(context)
    {
        AddMapping<IFooRepository, FooRepository>();
    }

    public IFooRepository FooRepository => Get<IFooRepository>();
}
```

## TODOs

- Create NuGet package
- Implement Unit Testss