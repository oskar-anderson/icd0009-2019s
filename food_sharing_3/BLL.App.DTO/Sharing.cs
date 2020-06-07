using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;
using ee.itcollege.kaande.pitsariina.Contracts.Domain;

namespace BLL.App.DTO
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