using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.App
{
    public class CartMeal : DomainEntityIdMetadata
    {
        public Guid CartId { get; set; } = default!;
        public Cart? Cart { get; set; }

        public Guid? MealId { get; set; }
        public Meal? Meal { get; set; }
        
        public Guid? PizzaUserId { get; set; }
        public PizzaUser? PizzaUser { get; set; }
        
        [MinLength(1)] [MaxLength(128)] public string Name { get; set; } = default!;
        
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Gross { get; set; } = default!;
    }
}