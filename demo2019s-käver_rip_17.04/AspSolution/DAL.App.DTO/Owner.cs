using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    
    public class Owner : Owner<Guid>, IDomainBaseEntity
    {
        
    }
    public class Owner<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>

    {
        public TKey Id { get; set; } = default!;

        public virtual string FirstName { get; set; } = default!;
        public virtual string LastName { get; set; } = default!;

        public virtual ICollection<OwnerAnimal>? Animals { get; set; }
        
        public virtual int AnimalCount { get; set; }   

        public virtual TKey AppUserId{ get; set; } = default!;
        public virtual AppUser<TKey>? AppUser { get; set; }
        
        public string Description { get; set; }
        public string Biography { get; set; }
    }

    public class OwnerDisplay
    {
        public Guid Id { get; set; } = default!;
        public virtual string FirstName { get; set; } = default!;
        public virtual string LastName { get; set; } = default!;
        public virtual int AnimalCount { get; set; }
        
        public string Description { get; set; }
        public string Biography { get; set; }

    }
    
    
}