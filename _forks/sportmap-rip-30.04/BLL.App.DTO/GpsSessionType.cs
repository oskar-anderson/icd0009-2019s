using System;
using System.Collections.Generic;
using Contracts.Domain;

namespace BLL.App.DTO
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

        public ICollection<GpsSession>? GpsSessions { get; set; }
    }
}