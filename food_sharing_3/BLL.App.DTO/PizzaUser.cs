using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class PizzaUser : Contracts.Domain.IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid PizzaId { get; set; } = default!;
        public Pizza? Pizza { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }

        public string Changes { get; set; } = default!;

        public Dictionary<string, int> DifferenceWithTemplate = new Dictionary<string, int>();
        
        public ICollection<ComponentPizzaUser>? PizzaComponents { get; set; }
        public ICollection<CartMeal>? CartMeals { get; set; }

    }
}