using System;

namespace PublicApi.DTO.v1
{
    public class GpsLocationCreateBulkResponse
    {
        public int LocationsAdded { get; set; }
        public int LocationsReceived { get; set; }
        public Guid GpsSessionId { get; set; }    
    }
}