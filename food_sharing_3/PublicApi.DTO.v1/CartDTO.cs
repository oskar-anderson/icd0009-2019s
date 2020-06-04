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

        public Guid RestaurantId { get; set; } = default!;
        public RestaurantDTO? Restaurant { get; set; }

        public bool AsDelivery { get; set; } = default!;

        public Guid? UserLocationId { get; set; }
        public UserLocationDTO? UserLocation { get; set; }

        [MinLength(1)] [MaxLength(64)] public string PaymentMethod { get; set; } = default!;

        [MinLength(1)] [MaxLength(128)] public string FirstName { get; set; } = default!;
        [MinLength(1)] [MaxLength(128)] public string LastName { get; set; } = default!;
        [MinLength(1)] [MaxLength(16)] public string Phone { get; set; } = default!;
    }
}