using System;

namespace Contracts.DAL.Base
{
    public interface IDomainBaseEntity : IDomainBaseEntity<Guid>
    {
    }

    public interface IDomainBaseEntity<TKey> 
        where TKey : struct, IComparable
    {
        public TKey Id { get; set; }
    }
}