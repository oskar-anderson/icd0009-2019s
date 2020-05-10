using System;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class CartMeal : Contracts.Domain.IDomainEntityId
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