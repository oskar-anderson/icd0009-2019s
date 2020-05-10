using System;
using System.Collections.Generic;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class TrackPoint: DomainEntityIdMetadataUser<AppUser>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Accuracy { get; set; }

        public int PassOrder { get; set; }

        public Guid TrackId { get; set; }
        public Track? Track { get; set; }

        public ICollection<GpsLocation>? GpsLocations { get; set; }
    }
}