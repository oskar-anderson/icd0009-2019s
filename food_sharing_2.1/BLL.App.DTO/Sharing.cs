using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;
using Contracts.DAL.Base;

namespace BLL.App.DTO
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