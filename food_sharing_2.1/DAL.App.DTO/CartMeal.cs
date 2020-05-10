using System;
using Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class CartMeal : IDomainBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        
        public Guid CartId { get; set; } = default!;
        public Cart Cart { get; set; }

        public Guid? MealId { get; set; }
        public Meal Meal { get; set; }
        
        public Guid? PizzaFinalId { get; set; }
        public PizzaFinal PizzaFinal { get; set; }
    }
}