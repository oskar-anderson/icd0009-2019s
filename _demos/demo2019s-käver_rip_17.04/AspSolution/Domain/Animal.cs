using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Animal : Animal<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser>
    {
    }

    public class Animal<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        [MinLength(1)] [MaxLength(64)] public virtual string AnimalName { get; set; } = default!;

        public virtual int? BirthYear { get; set; }

        public TKey AppUserId { get; set; } = default!;
        public TUser? AppUser { get; set; }

        public virtual ICollection<OwnerAnimal>? Owners { get; set; }
    }
}