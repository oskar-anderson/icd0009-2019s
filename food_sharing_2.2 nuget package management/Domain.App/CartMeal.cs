using System;
using Domain.Base;

namespace Domain.App
{
    public class CartMeal : DomainEntityIdMetadata
    {
        public Guid CartId { get; set; } = default!;
        public Cart? Cart { get; set; }

        public Guid? MealId { get; set; }
        public Meal? Meal { get; set; }
        
        public Guid? PizzaFinalId { get; set; }
        public PizzaFinal? PizzaFinal { get; set; }
    }
}