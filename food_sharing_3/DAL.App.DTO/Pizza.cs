using System;
using System.Collections.Generic;
using ee.itcollege.kaande.pitsariina.Contracts.Domain;

namespace DAL.App.DTO
{
    public class Pizza : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid PizzaTemplateId { get; set; } = default!;
        public PizzaTemplate? PizzaTemplate { get; set; }
        
        public int SizeNumber { get; set; } = default!;
        public string SizeName { get; set; } = default!;
        
        public string Name { get; set; } = default!;
        
        public ICollection<RestaurantFood>? RestaurantFoods { get; set; }
        public ICollection<CartMeal>? CartMeals { get; set; }
    }
}