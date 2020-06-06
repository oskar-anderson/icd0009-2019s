using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;

namespace DAL.Base
{
    public abstract class DomainBaseEntity : DomainBaseEntity<Guid>, IDomainBaseEntity
    {
    }
    
    public abstract class DomainBaseEntity<TKey> : IDomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; } = default!;
    }
}