using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class ComponentPizzaTemplate : DomainEntityIdMetadata
    {
        public Guid ComponentId { get; set; } = default!;
        public Component? Component { get; set; }
        
        public Guid PizzaTemplateId { get; set; } = default!;
        public PizzaTemplate? PizzaTemplate { get; set; }
    }
}