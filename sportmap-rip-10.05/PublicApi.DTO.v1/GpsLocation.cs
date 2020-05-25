using System;
using Contracts.DAL.Base;
using Contracts.Domain;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1
{
    public class GpsLocation : IDomainEntityId
    { 
        public Guid Id { get; set; }


        public DateTime RecordedAt { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Accuracy { get; set; }
        public double Altitude { get; set; }
        public double VerticalAccuracy { get; set; }
        public Guid AppUserId { get; set; }

        public Guid GpsSessionId { get; set; }

        public Guid GpsLocationTypeId { get; set; }
    }
}