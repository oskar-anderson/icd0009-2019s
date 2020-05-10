using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Owner : Owner<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser>
    {
        
    }

    public class Owner<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser: AppUser<TKey>
    {
        [MinLength(1)] [MaxLength(64)] 
        [Display(Name = nameof(FirstName), ResourceType = typeof(Resources.Domain.Owner))]
        public virtual string FirstName { get; set; } = default!;
        [MinLength(1)] [MaxLength(64)] 
        [Display(Name = nameof(LastName), ResourceType = typeof(Resources.Domain.Owner))]
        public virtual string LastName { get; set; } = default!;

        public virtual ICollection<OwnerAnimal>? Animals { get; set; }


        public TKey DescriptionId { get; set; } = default!;
        public MultiLangString? Description { get; set; }
        
        public TKey BiographyId { get; set; } = default!;
        public MultiLangString? Biography { get; set; }
        
        
        public virtual TKey AppUserId{ get; set; }  = default!;
        public virtual TUser? AppUser { get; set; }
        
        public virtual string FirstLastName => FirstName + " " + LastName;
        public virtual string LastFirstName => LastName + " " + FirstName;
    }
    
}