using System;

namespace ee.itcollege.kaande.pitsariina.Contracts.DAL.Base
{
    public interface IBaseEntityTracker : IBaseEntityTracker<Guid>
    {
        
    }
    
    public interface IBaseEntityTracker<TKey>
        where TKey: IEquatable<TKey>
    {
        //Dictionary<IDomainEntityId<TKey>, IDomainEntityId<TKey>> EntityTracker { get;  }
        void AddToEntityTracker(Contracts.Domain.IDomainEntityId<TKey> internalEntity, Contracts.Domain.IDomainEntityId<TKey> externalEntity);
    }
}