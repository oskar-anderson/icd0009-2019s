using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Owner : Owner<Guid>, IDomainEntity
    {
        
    }

    public class Owner<TKey> : DomainEntity<TKey>
        where TKey : struct, IEquatable<TKey>
    {
        [MinLength(1)] [MaxLength(64)] 
        [Display(Name = nameof(FirstName), ResourceType = typeof(Resources.Domain.Owner))]
        public virtual string FirstName { get; set; } = default!;
        [MinLength(1)] [MaxLength(64)] 
        [Display(Name = nameof(LastName), ResourceType = typeof(Resources.Domain.Owner))]
        public virtual string LastName { get; set; } = default!;

        public virtual ICollection<OwnerAnimal>? Animals { get; set; }


        public virtual TKey AppUserId{ get; set; }
        public virtual AppUser? AppUser { get; set; }
        
        public virtual string FirstLastName => FirstName + " " + LastName;
        public virtual string LastFirstName => LastName + " " + FirstName;
    }
    
}