using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class ComponentPizzaTemplateDTO
    {
        public Guid Id { get; set; }
        
        public Guid ComponentId { get; set; } = default!;
        public ComponentDTO? Component { get; set; }
        
        public Guid PizzaTemplateId { get; set; } = default!;
        public PizzaTemplateDTO? PizzaTemplate { get; set; }
        
    }
}