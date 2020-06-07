using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.kaande.pitsariina.Domain.Base;

namespace Domain.App
{
    public class Pizza : DomainEntityIdMetadata
    {
        public Guid PizzaTemplateId { get; set; } = default!;
        public PizzaTemplate? PizzaTemplate { get; set; }

        [Range(0, 6)] public int SizeNumber { get; set; } = default!;
        [MaxLength(64)] public string SizeName { get; set; } = default!;

        [MinLength(4), MaxLength(128)] public string Name { get; set; } = default!;
        
        public ICollection<RestaurantFood>? RestaurantFoods { get; set; }
        public ICollection<CartMeal>? CartMeals { get; set; }
    }
}