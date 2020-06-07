using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using ee.itcollege.kaande.pitsariina.Domain.Base;

namespace Domain.App
{
    public class Sharing : DomainEntityIdMetadata
    {
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        
        [MinLength(1)] [MaxLength(64)] public string Name { get; set; } = default!;
        
        public ICollection<SharingItem>? SharingItems { get; set; } 
        public ICollection<Item>? Items { get; set; } 
    }
}