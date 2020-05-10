using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class PizzaComponent : DomainEntityIdMetadata
    {
        public Guid ComponentId { get; set; } = default!;
        public Component? Component { get; set; }

        public Guid? PizzaFinalId { get; set; }
        public PizzaFinal? PizzaFinal { get; set; }
        
        public Guid? PizzaTemplateId { get; set; }
        public PizzaTemplate? PizzaTemplate { get; set; }
    }
}