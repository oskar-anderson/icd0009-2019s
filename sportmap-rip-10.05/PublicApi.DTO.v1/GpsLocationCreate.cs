using System;

namespace PublicApi.DTO.v1
{
    public class GpsLocationCreate
    {
        public DateTime RecordedAt { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Accuracy { get; set; }
        public double Altitude { get; set; }
        public double VerticalAccuracy { get; set; }
        public Guid GpsLocationTypeId { get; set; }
    }
}