using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Meal : DomainEntityIdMetadata
    {
        public Guid CategoryId { get; set; } = default!;
        public Category? Category { get; set; }

        [MaxLength(128)] public string Name { get; set; } = default!;
        
        [MaxLength(128)] public string? Picture { get; set; }

        [MaxLength(256)] [MinLength(4)] public string? Description { get; set; }

        
        public ICollection<RestaurantFood>? RestaurantFoods { get; set; }
        public ICollection<CartMeal>? CartMeals { get; set; }
    }
}