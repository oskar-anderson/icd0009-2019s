using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class PizzaDTO
    {
        public Guid Id { get; set; }
        
        public Guid PizzaTemplateId { get; set; } = default!;
        public PizzaTemplateDTO? PizzaTemplate { get; set; }
        
        public Guid SizeId { get; set; } = default!;
        public SizeDTO? Size { get; set; }

        [MinLength(4), MaxLength(128)] public string Name { get; set; } = default!;

    }
}