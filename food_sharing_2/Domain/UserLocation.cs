﻿using System.ComponentModel.DataAnnotations;
 using DAL.Base;
 using Domain.Identity;

 namespace Domain
{
    public class UserLocation : DomainEntityMetadata
    {
        [MaxLength(32)] public string AppUserId { get; set; } = default!;
        public virtual AppUser? AppUser { get; set; }

        [MinLength(1)] [MaxLength(32)] public string District { get; set; } = default!;

        [MinLength(1)] [MaxLength(32)] public string ApartmentName { get; set; } = default!;

        [MinLength(1)] [MaxLength(32)] public string StreetName { get; set; } = default!;

        [MinLength(1)] [MaxLength(32)] public string BuildingOrApartmentNumber { get; set; } = default!;

    }
}