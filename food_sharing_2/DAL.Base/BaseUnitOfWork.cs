using System;
using System.Collections.Generic;

namespace DAL.Base
{
    public class BaseUnitOfWork
    {
        private readonly Dictionary<Type , object> _repoCahche = new Dictionary<Type, object>();
        protected TRepository GetRepository<TRepository>(Func<TRepository> repoCreationMethod)
        {
            if (_repoCahche.TryGetValue(typeof(TRepository), out var repo))
            {
                return (TRepository) repo;
            }
            
            repo = repoCreationMethod()!;
            _repoCahche.Add(typeof(TRepository), repo);
            return (TRepository) repo;
        }
    }
}