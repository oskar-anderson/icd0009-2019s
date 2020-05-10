using System;

namespace PublicApi.DTO.v1
{
    public class CartMealDTO
    {
        public Guid Id { get; set; }
        
        public Guid CartId { get; set; } = default!;
        public CartDTO Cart { get; set; }

        public Guid? MealId { get; set; }
        public MealDTO Meal { get; set; }
        
        public Guid? PizzaFinalId { get; set; }
        public PizzaFinalDTO PizzaFinal { get; set; }
    }
}