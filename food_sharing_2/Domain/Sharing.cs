﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
 using DAL.Base;
 using Domain.Identity;

 namespace Domain
{
    public class Sharing : DomainEntityMetadata
    {
        [MaxLength(32)] public string AppUserId { get; set; } = default!;
        public virtual AppUser? AppUser { get; set; }
        
        public virtual ICollection<SharingItem>? SharingItems { get; set; } 
        public virtual ICollection<Item>? Items { get; set; } 
    }
}