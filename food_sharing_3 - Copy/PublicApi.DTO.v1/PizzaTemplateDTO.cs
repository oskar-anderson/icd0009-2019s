using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class PizzaTemplateDTO
    {
        public Guid Id { get; set; }
        
        public Guid CategoryId { get; set; } = default!;
        public CategoryDTO? Category { get; set; }

        [MaxLength(128)] public string Name { get; set; } = default!;
        
        [MaxLength(128)] public string? Picture { get; set; }

        [Range(0, 6)] public int Modifications { get; set; } = default!;

        [Range(0, 8)] public int Extras { get; set; } = default!;

        [MaxLength(128)] [MinLength(4)] public string? Description { get; set; }

    }
}