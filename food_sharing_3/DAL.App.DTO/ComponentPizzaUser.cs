using System;
using Contracts.DAL.Base;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class ComponentPizzaUser : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid ComponentId { get; set; } = default!;
        public Component? Component { get; set; }

        public Guid PizzaUserId { get; set; } = default!;
        public PizzaUser? PizzaUser { get; set; }
    }
}