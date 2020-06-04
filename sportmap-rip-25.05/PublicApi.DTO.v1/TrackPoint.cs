using System;
using System.Collections.Generic;
using com.akaver.sportmap.Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class TrackPoint: IDomainEntityId
    {
        public Guid Id { get; set; }
       
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Accuracy { get; set; }

        public int PassOrder { get; set; }

        public Guid TrackId { get; set; }

        public Guid AppUserId { get; set; }
        
    }
}