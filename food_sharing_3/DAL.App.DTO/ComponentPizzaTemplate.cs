using System;
using ee.itcollege.kaande.pitsariina.Contracts.Domain;

namespace DAL.App.DTO
{
    public class ComponentPizzaTemplate : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid ComponentId { get; set; } = default!;
        public Component? Component { get; set; }

        public Guid? PizzaTemplateId { get; set; }
        public PizzaTemplate? PizzaTemplate { get; set; }
    }
}