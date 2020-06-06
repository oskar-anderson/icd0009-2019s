using System;
using DAL.Base;

namespace Domain

{
    public class OwnerAnimal : OwnerAnimal<Guid>
    {
    }

    public class OwnerAnimal<TKey> : DomainEntity<TKey> 
        where TKey : struct, IEquatable<TKey>
    {

        public virtual TKey OwnerId { get; set; }
        public virtual Owner? Owner { get; set; }

        public virtual TKey AnimalId { get; set; }
        public virtual Animal? Animal { get; set; }

        public virtual int OwnedPercentage { get; set; }
        
    }
}