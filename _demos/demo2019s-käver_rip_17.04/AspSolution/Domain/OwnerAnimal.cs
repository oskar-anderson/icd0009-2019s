using System;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace Domain

{
    public class OwnerAnimal : OwnerAnimal<Guid, AppUser>, IDomainEntityUser<AppUser>
    {
    }

    public class OwnerAnimal<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey> 
        where TUser : AppUser<TKey>
    {

        public virtual TKey OwnerId { get; set; } = default!;
        public virtual Owner? Owner { get; set; }

        public virtual TKey AnimalId { get; set; } = default!;
        public virtual Animal? Animal { get; set; }

        public virtual int OwnedPercentage { get; set; }

        public TKey AppUserId { get; set; }  = default!;
        public TUser? AppUser { get; set; }
    }
}