using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class PizzaUser : DomainEntityIdMetadata
    {
        public Guid PizzaId { get; set; } = default!;
        public Pizza? Pizza { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }

        [MinLength(1)] [MaxLength(256)] public string Changes { get; set; } = default!;

        [NotMapped] public Dictionary<string, int> DifferenceWithTemplate = new Dictionary<string, int>();
        
        public ICollection<ComponentPizzaUser>? ComponentPizzaUser { get; set; }
        public ICollection<CartMeal>? CartMeals { get; set; }
    }
}