﻿using System;
 using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
 using DAL.Base;
 using Domain.Identity;

 namespace Domain
{
    public class Sharing : DomainBaseMetadata
    {
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        
        [MinLength(1)] [MaxLength(64)] public string Name { get; set; } = default!;
        
        public ICollection<SharingItem>? SharingItems { get; set; } 
        public ICollection<Item>? Items { get; set; } 
    }
}