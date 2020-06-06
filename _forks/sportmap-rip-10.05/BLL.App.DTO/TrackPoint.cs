using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class TrackPoint: IDomainEntityId
    {
        public Guid Id { get; set; }
       
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Accuracy { get; set; }

        public int PassOrder { get; set; }

        public Guid TrackId { get; set; }
        public Track? Track { get; set; }
        
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public ICollection<GpsLocation>? GpsLocations { get; set; }
    }
}