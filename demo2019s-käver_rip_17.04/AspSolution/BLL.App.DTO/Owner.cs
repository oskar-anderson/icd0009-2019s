using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;
using Contracts.DAL.Base;

namespace BLL.App.DTO
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


        public string Description { get; set; }
        public string Biography { get; set; }
        
        public virtual TKey AppUserId{ get; set; } = default!;
        public virtual AppUser<TKey>? AppUser { get; set; }
    }
}