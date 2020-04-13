﻿using System;
 using System.ComponentModel.DataAnnotations;
 using DAL.Base;
 using Domain.Identity;

 namespace Domain
{
    public class UserLocation : DomainEntity
    {
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }

        [MinLength(1)] [MaxLength(32)] public string District { get; set; } = default!;
        [MinLength(1)] [MaxLength(32)] public string StreetName { get; set; } = default!;
        [MinLength(1)] [MaxLength(32)] public string BuildingNumber { get; set; } = default!;
        [MinLength(1)] [MaxLength(32)] public string? ApartmentNumber { get; set; }

    }
}