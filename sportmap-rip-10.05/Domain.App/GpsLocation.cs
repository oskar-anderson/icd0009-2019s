using System;
using DAL.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class GpsLocation : DomainEntityIdMetadataUser<AppUser>
    {
        public DateTime RecordedAt { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Accuracy { get; set; }
        public double Altitude { get; set; }
        public double VerticalAccuracy { get; set; }

        public Guid GpsSessionId { get; set; }
        public GpsSession? GpsSession { get; set; }

        public Guid GpsLocationTypeId { get; set; }
        public GpsLocationType? GpsLocationType { get; set; }

        public Guid? TrackPointId { get; set; }
        public TrackPoint? TrackPoint { get; set; }
    }
}