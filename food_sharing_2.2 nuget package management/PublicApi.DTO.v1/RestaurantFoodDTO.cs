using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublicApi.DTO.v1
{
    public class RestaurantFoodDTO
    {
        public Guid Id { get; set; }
        
        public Guid? MealId { get; set; }
        public MealDTO? Meal { get; set; }
        
        public Guid? PizzaId { get; set; }
        public PizzaDTO? Pizza { get; set; }

        public Guid RestaurantId { get; set; } = default!;
        public RestaurantDTO? Restaurant { get; set; }

        [MaxLength(64, ErrorMessage = "Too long!")]
        public string Name { get; set; } = default!;

        [Column(TypeName = "decimal(18,4)")]
        public decimal Tax { get; set; } = default!;

        [Column(TypeName = "decimal(18,4)")]
        public decimal Gross { get; set; } = default!;

        public DateTime Since { get; set; } = default!;
        
        public DateTime Until { get; set; } = default!;
    }
}