﻿using System.ComponentModel.DataAnnotations;
 using DAL.Base;
 using Domain.Identity;

 namespace Domain
{
    public class Friend : DomainEntity
    {
        [MaxLength(32)] public string AppUserId { get; set; } = default!;
        public virtual AppUser? AppUser { get; set; }

        [MaxLength(128)][MinLength(1)] public string FirstName { get; set; } = default!;
        
        [MaxLength(128)] public string? LastName { get; set; }
    }
}