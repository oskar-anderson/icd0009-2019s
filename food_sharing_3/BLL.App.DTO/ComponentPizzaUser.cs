using System;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class ComponentPizzaUser : Contracts.Domain.IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid ComponentId { get; set; } = default!;
        public Component? Component { get; set; }

        public Guid PizzaUserId { get; set; } = default!;
        public PizzaUser? PizzaUser { get; set; }
    }
}