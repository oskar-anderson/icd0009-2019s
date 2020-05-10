using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class GpsLocationType : IDomainEntityId
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(64)]
        public string Name { get; set; } = default!;

        [Required] [MaxLength(4096)] public string Description { get; set; } = default!;

        [JsonIgnore]
        public ICollection<GpsLocation>? GpsLocations { get; set; }
    }
}