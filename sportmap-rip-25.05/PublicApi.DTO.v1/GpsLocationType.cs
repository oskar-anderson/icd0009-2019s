using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using com.akaver.sportmap.Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class GpsLocationType : IDomainEntityId
    {
        public Guid Id { get; set; }

        
        public Guid NameId { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(64)]
        public string Name { get; set; } = default!;

        public Guid DescriptionId { get; set; }
        [Required] [MaxLength(4096)] public string Description { get; set; } = default!;

    }
}