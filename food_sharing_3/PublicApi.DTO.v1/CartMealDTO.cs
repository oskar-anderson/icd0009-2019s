using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublicApi.DTO.v1
{
    public class CartMealDTO
    {
        public Guid Id { get; set; }
        
        public Guid? MealId { get; set; }
        public MealDTO? Meal { get; set; }
        
        public Guid? PizzaUserId { get; set; }
        public PizzaUserDTO? PizzaUser { get; set; }
        
        [MinLength(1)] [MaxLength(128)] public string Name { get; set; } = default!;
        
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Gross { get; set; } = default!;
    }
}