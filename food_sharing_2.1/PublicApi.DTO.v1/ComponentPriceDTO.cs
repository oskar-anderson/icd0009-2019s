using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain;

namespace PublicApi.DTO.v1
{
    public class ComponentPriceDTO
    {
        public Guid Id { get; set; }
        
        public Guid ComponentId { get; set; } = default!;
        public ComponentDTO? Component { get; set; }

        public Guid RestaurantId { get; set; } = default!;
        public RestaurantDTO? Restaurant { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Gross { get; set; } = default!;

        [Column(TypeName = "decimal(18,4)")]
        public decimal Tax { get; set; } = default!;

        public DateTime Since { get; set; } = default!;

        public DateTime Until { get; set; } = default!;
    }
}