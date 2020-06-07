using System;
using ee.itcollege.kaande.pitsariina.Contracts.DAL.Base.Repositories;
using ee.itcollege.kaande.pitsariina.Contracts.Domain;

namespace ee.itcollege.kaande.pitsariina.Contracts.BLL.Base.Services
{
    public interface IBaseEntityService<TEntity> : IBaseEntityService<Guid, TEntity>
        where TEntity : class, IDomainEntityId<Guid>, new()
    {
    }

    public interface IBaseEntityService<in TKey, TEntity> : IBaseService, IBaseRepository<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IDomainEntityId<TKey>, new()
    {
        
    }

}
        
 