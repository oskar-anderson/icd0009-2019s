using System;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class ComponentPizzaTemplate : Contracts.Domain.IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid ComponentId { get; set; } = default!;
        public Component? Component { get; set; }

        public Guid? PizzaTemplateId { get; set; }
        public PizzaTemplate? PizzaTemplate { get; set; }
    }
}