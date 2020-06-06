using System;
using System.Collections.Generic;
using Domain.Base;

namespace Domain.App
{
    public class GpsSessionType: DomainEntityIdMetadata
    {
        public Guid NameId { get; set; }
        public LangStr? Name { get; set; }
        
        public Guid DescriptionId { get; set; }
        public LangStr? Description { get; set; }

        public int PaceMin { get; set; }
        public int PaceMax { get; set; }

        public ICollection<GpsSession>? GpsSessions { get; set; }
    }
}