using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;

namespace PublicApi.DTO.v1
{
    public class GpsLocationType : IDomainEntityId
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(64)]
        public string Name { get; set; } = default!;

        [Required] [MaxLength(4096)] public string Description { get; set; } = default!;

    }
}