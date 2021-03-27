using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitOfWorkPattern
{
    public abstract class UnitOfWorkBase<TContext> : IUnitOfWorkBase
        where TContext : IContext
    {
        protected readonly TContext Context;
        private readonly Dictionary<Type, IRepositoryBase> _instantiatedRepositories = new Dictionary<Type, IRepositoryBase>();
        private readonly Dictionary<Type, Type> _repositoryMappings = new Dictionary<Type, Type>();

        protected UnitOfWorkBase(TContext context)
        {
            Context = context;
        }

        protected TRepository Get<TRepository>()
            where TRepository : IRepositoryBase
        {
            var type = typeof(TRepository);

            try
            {
                if (_instantiatedRepositories[type] is { } repository)
                    return (TRepository)repository;
            }
            catch (KeyNotFoundException)
            { }

            var repoType = _repositoryMappings[type];
            var instantiatedRepository = (TRepository)Activator.CreateInstance(repoType, Context);
            _instantiatedRepositories.Add(typeof(TRepository), instantiatedRepository);
            return instantiatedRepository;
        }

        protected void AddMapping<TInterface, TRepository>()
            where TInterface : IRepositoryBase
            where TRepository : class, IRepositoryBase
        {
            var isInterfaceType = typeof(TRepository).GetInterfaces().Any(x =>
                x.IsGenericType && x.GetGenericTypeDefinition() == typeof(TInterface));
            if (!isInterfaceType)
                throw new Exception($"The {nameof(TRepository)} does not implement interface {nameof(TInterface)}");

            _repositoryMappings.Add(typeof(TInterface), typeof(TRepository));
        }

        public void Dispose()
            => Context.Dispose();

        public async Task CommitAsync()
            => await Context.SaveChangesAsync();
    }
}
