using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Animal : Animal<Guid>, IDomainEntity
    {
    }

    public class Animal<TKey> : DomainEntity<TKey>
        where TKey : struct, IEquatable<TKey>
    {
        [MinLength(1)] [MaxLength(64)] public virtual string AnimalName { get; set; } = default!;

        public virtual int? BirthYear { get; set; }

        public virtual TKey AppUserId { get; set; }
        public virtual AppUser? AppUser { get; set; }


        public virtual ICollection<OwnerAnimal>? Owners { get; set; }
    }
}