using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.kaande.pitsariina.Contracts.BLL.Base;
using ee.itcollege.kaande.pitsariina.Contracts.DAL.Base;

namespace ee.itcollege.kaande.pitsariina.BLL.Base
{
    public abstract class BaseBLL<TUnitOfWork> : IBaseBLL
        where TUnitOfWork : IBaseUnitOfWork
    {
        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly TUnitOfWork UOW;
 


        private readonly Dictionary<Type, object> _serviceCache = new Dictionary<Type, object>();

        protected BaseBLL(TUnitOfWork uow)
        {
            UOW = uow;
        }
        
        public Task<int> SaveChangesAsync()
        {
            return UOW.SaveChangesAsync();
        }

        public TService GetService<TService>(Func<TService> serviceCreationMethod)
            where TService : class
        {
            if (_serviceCache.TryGetValue(typeof(TService), out var repo))
            {
                return (TService) repo;
            }

            var newRepoInstance = serviceCreationMethod();
            _serviceCache.Add(typeof(TService), newRepoInstance);
            return newRepoInstance;
        }
    }
}