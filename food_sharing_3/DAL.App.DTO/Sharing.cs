using System;
using System.Collections.Generic;
using ee.itcollege.kaande.pitsariina.Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class Sharing : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        
        public string Name { get; set; } = default!;
        
        public ICollection<SharingItem>? SharingItems { get; set; } 
        public ICollection<Item>? Items { get; set; }
    }
}