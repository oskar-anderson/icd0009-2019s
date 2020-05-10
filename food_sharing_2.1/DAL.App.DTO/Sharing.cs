using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class Sharing : IDomainBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        
        public string Name { get; set; } = default!;
        
        public ICollection<SharingItem>? SharingItems { get; set; } 
        public ICollection<Item>? Items { get; set; }
    }
}