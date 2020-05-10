using System;
using Contracts.DAL.Base;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class PizzaComponent : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid ComponentId { get; set; } = default!;
        public Component? Component { get; set; }

        public Guid? PizzaFinalId { get; set; }
        public PizzaFinal? PizzaFinal { get; set; }
        
        public Guid? PizzaTemplateId { get; set; }
        public PizzaTemplate? PizzaTemplate { get; set; }
    }
}