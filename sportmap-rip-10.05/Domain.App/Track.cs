using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Track: DomainEntityIdMetadataUser<AppUser>
    {
        public Guid NameId { get; set; }
        public LangStr? Name { get; set; }
        
        public Guid DescriptionId { get; set; }
        public LangStr? Description { get; set; }

        [MaxLength(512)]
        public string? MapFile { get; set; }

        public ICollection<TrackPoint>? TrackPoints { get; set; }
    }
}