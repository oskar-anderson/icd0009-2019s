﻿using System;
using Contracts.DAL.Base;
using Domain.Base.App.DTO.Identity;

namespace Domain.Base.App.DTO
{
    public class UserLocation : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }

        public string District { get; set; } = default!;
        public string StreetName { get; set; } = default!;
        public string BuildingNumber { get; set; } = default!;
        public string? ApartmentNumber { get; set; }

    }
}