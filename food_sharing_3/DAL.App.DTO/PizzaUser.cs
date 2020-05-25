using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class PizzaUser : IDomainEntityId
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