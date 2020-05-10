using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class MealDTO
    {
        public Guid Id { get; set; }
        
        public Guid CategoryId { get; set; } = default!;
        public CategoryDTO? Category { get; set; }

        [MaxLength(128)] public string Name { get; set; } = default!;
        
        [MaxLength(128)] public string? Picture { get; set; }

        [MaxLength(256)] [MinLength(4)] public string? Description { get; set; }

    }
}