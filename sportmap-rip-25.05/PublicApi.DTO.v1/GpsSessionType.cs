using System;
using System.Collections.Generic;
using com.akaver.sportmap.Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class GpsSessionType: IDomainEntityId
    {
        public Guid Id { get; set; }

        public Guid NameId { get; set; }
        public string Name { get; set; } = default!;
        
        public Guid DescriptionId { get; set; }
        public string Description { get; set; } = default!;

        public int PaceMin { get; set; }
        public int PaceMax { get; set; }

    }
}