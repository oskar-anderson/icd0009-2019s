using System;
using BLL.App.DTO.Identity;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class UserLocation : Contracts.Domain.IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }

        public string District { get; set; } = default!;
        public string StreetName { get; set; } = default!;
        public string BuildingNumber { get; set; } = default!;
        public string? ApartmentNumber { get; set; }

        public string FullName
        {
            get { return $"{District}, {StreetName}, {BuildingNumber}-{ApartmentNumber}"; }
        }
    }
}