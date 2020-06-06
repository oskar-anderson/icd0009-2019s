using System;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class OwnerAnimal : OwnerAnimal<Guid>, IDomainBaseEntity
    {
    }

    public class OwnerAnimal<TKey> : IDomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;


        public virtual TKey OwnerId { get; set; } = default!;
        public virtual Owner? Owner { get; set; }

        public virtual TKey AnimalId { get; set; } = default!;
        public virtual Animal? Animal { get; set; }

        public virtual int OwnedPercentage { get; set; }

        public TKey AppUserId { get; set; } = default!;
        public AppUser<TKey>? AppUser { get; set; }
    }
}