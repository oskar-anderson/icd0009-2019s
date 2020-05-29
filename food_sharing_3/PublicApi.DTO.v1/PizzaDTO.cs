using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class PizzaDTO
    {
        public Guid Id { get; set; }
        
        public Guid PizzaTemplateId { get; set; } = default!;
        public PizzaTemplateDTO? PizzaTemplate { get; set; }

        [Range(0, 6)] public int SizeNumber { get; set; } = default!;
        [MaxLength(64)] public string SizeName { get; set; } = default!;

        [MinLength(4), MaxLength(128)] public string Name { get; set; } = default!;


    }
}