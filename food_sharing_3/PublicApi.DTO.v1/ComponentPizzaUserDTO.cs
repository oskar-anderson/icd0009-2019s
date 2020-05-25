using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class ComponentPizzaUserDTO
    {
        public Guid Id { get; set; }
        
        public Guid ComponentId { get; set; } = default!;
        public ComponentDTO? Component { get; set; }

        public Guid PizzaUserId { get; set; } = default!;
        public PizzaUserDTO? PizzaUser { get; set; }
        
    }
}