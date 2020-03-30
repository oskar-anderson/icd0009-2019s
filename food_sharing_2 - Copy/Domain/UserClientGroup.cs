﻿using System;
using System.ComponentModel.DataAnnotations;
 using DAL.Base;
 using Domain.Identity;

 namespace Domain
{
    public class UserClientGroup : DomainEntity
    {
        [MaxLength(32)] public string AppUserId { get; set; } = default!;
        public virtual AppUser? AppUser { get; set; }

        [MaxLength(32)] public string ClientGroupId { get; set; } = default!;
        public virtual ClientGroup? ClientGroup { get; set; }

        public DateTime Since { get; set; } = default!;

        public DateTime Until { get; set; } = default!;

    }
}