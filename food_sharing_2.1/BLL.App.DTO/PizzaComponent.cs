using System;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class PizzaComponent : IDomainBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        
        public Guid ComponentId { get; set; } = default!;
        public Component? Component { get; set; }

        public Guid? PizzaFinalId { get; set; }
        public PizzaFinal? PizzaFinal { get; set; }
        
        public Guid? PizzaTemplateId { get; set; }
        public PizzaTemplate? PizzaTemplate { get; set; }
        
        public int Amount { get; set; } = default!;
    }
}