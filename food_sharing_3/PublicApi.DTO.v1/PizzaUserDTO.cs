using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1
{
    public class PizzaUserDTO
    {
        public Guid Id { get; set; }
        
        public Guid PizzaId { get; set; } = default!;
        public PizzaDTO? Pizza { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }

        [MinLength(1)] [MaxLength(256)] public string Changes { get; set; } = default!;
        
        public Dictionary<string, int> DifferenceWithTemplate = new Dictionary<string, int>();

    }
}