using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class ComponentPizzaUser : DomainEntityIdMetadata
    {
        public Guid ComponentId { get; set; } = default!;
        public Component? Component { get; set; }

        public Guid PizzaUserId { get; set; } = default!;
        public PizzaUser? PizzaUser { get; set; }
    }
}