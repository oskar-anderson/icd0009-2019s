using System;

namespace Contracts.DAL.Base
{
    public interface IDomainBaseMetadata : IDomainBaseMetadata<Guid>
    {
    }

    public interface IDomainBaseMetadata<TKey> : IDomainBaseEntity<TKey>, IDomainMetadata
        where TKey : struct, IComparable
    {
    }
}