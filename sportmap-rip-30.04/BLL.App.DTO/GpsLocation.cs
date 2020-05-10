using System;
using BLL.App.DTO.Identity;
using Contracts.DAL.Base;
using System.Text.Json.Serialization;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class GpsLocation : IDomainEntityId
    { 
        public Guid Id { get; set; }

        public Guid AppUserId { get; set; }
        [JsonIgnore]
        public AppUser? AppUser { get; set; }

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