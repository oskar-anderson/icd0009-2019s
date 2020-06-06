using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using DAL.Base;
using Domain.Base;

namespace Domain.App
{
    public class GpsLocationType : DomainEntityIdMetadata
    {
        public Guid NameId { get; set; }
        public LangStr? Name { get; set; }
        
        public Guid DescriptionId { get; set; }
        public LangStr? Description { get; set; }

        public ICollection<GpsLocation>? GpsLocations { get; set; }
    }
}