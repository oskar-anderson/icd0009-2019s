using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class Meal : IDomainBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        
        public Guid CategoryId { get; set; } = default!;
        public Category? Category { get; set; }

        public string Name { get; set; } = default!;
        
        public string? Picture { get; set; }

        public string? Description { get; set; }

        
        public ICollection<RestaurantFood>? RestaurantFoods { get; set; }
        public ICollection<CartMeal>? CartMeals { get; set; }

    }
}