using System;

namespace Contracts.DAL.Base
{
    public interface IDomainBaseEntity : IDomainBaseEntity<Guid>
    {
    }

    public interface IDomainBaseEntity<TKey> 
        where TKey : struct, IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }
}