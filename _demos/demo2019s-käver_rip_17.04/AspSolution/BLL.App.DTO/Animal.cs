using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Animal : Animal<Guid>, IDomainBaseEntity
    {
    }
    
    public class Animal<TKey> : IDomainBaseEntity<TKey>
    where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public virtual string AnimalName { get; set; } = default!;

        public virtual int? BirthYear { get; set; }

        public TKey AppUserId { get; set; } = default!;
        public AppUser<TKey>? AppUser { get; set; }
        
        public virtual ICollection<OwnerAnimal>? Owners { get; set; }        
    }
    
}