using System;
using Contracts.DAL.Base;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class CartMeal : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid CartId { get; set; } = default!;
        public Cart? Cart { get; set; }

        public Guid? MealId { get; set; }
        public Meal? Meal { get; set; }
        
        public Guid? PizzaFinalId { get; set; }
        public PizzaFinal? PizzaFinal { get; set; }
    }
}