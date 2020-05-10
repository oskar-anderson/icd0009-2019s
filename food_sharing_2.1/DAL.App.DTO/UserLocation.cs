using System;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class UserLocation : IDomainBaseEntity<Guid>
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