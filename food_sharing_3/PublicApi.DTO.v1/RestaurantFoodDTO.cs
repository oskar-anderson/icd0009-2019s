using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublicApi.DTO.v1
{
    public class RestaurantFoodDTO
    {
        public Guid Id { get; set; }

        public Guid PizzaId { get; set; } = default!;
        public PizzaDTO? Pizza { get; set; }

        public Guid RestaurantId { get; set; } = default!;
        public RestaurantDTO? Restaurant { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Gross { get; set; } = default!;
    }
}