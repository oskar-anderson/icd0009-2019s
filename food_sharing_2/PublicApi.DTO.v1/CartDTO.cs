using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1
{
    public class CartDTO
    {
        public Guid Id { get; set; }

        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        
        public bool AsDelivery { get; set; } = default!;
        public Guid? UserLocationId { get; set; }
        public UserLocationDTO? UserLocation { get; set; }
        
        public Guid RestaurantId { get; set; } = default!;
        public RestaurantDTO? Restaurant { get; set; }

        [Column(TypeName = "decimal(18,4)")] public decimal Total { get; set; } = default!;
        
        [DataType(DataType.Time)]
        public DateTime ReadyBy { get; set; } = default!;
    }
}