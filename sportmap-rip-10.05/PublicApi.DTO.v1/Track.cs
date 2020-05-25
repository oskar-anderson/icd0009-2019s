using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO;
using Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class Track: IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid NameId { get; set; }
        public string Name { get; set; } = default!;
        
        public Guid DescriptionId { get; set; }
        public string Description { get; set; } = default!;

        [MaxLength(512)]
        public string? MapFile { get; set; }

        public Guid AppUserId { get; set; }
       
    }
}