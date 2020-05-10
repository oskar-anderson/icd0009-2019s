using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DAL.Base;

namespace Domain
{
    public class GpsLocationType : DomainEntityIdMetadata
    {
        [Required]
        [MinLength(1)]
        [MaxLength(64)]
        public string Name { get; set; } = default!;

        [Required] [MaxLength(4096)] public string Description { get; set; } = default!;

        [JsonIgnore] public ICollection<GpsLocation>? GpsLocations { get; set; }
    }
}