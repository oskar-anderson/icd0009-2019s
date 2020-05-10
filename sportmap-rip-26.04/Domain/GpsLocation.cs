using System;
using System.Text.Json.Serialization;
using DAL.Base;
using Domain.Identity;

namespace Domain
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
        [JsonIgnore]
        public GpsSession? GpsSession { get; set; }

        public Guid GpsLocationTypeId { get; set; }
        [JsonIgnore]
        public GpsLocationType? GpsLocationType { get; set; }
        
    }
}