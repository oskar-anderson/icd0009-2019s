using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.App.Identity;
using ee.itcollege.kaande.pitsariina.Domain.Base;

namespace Domain.App
{
    public class Cart : DomainEntityIdMetadata
    {
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }

        public Guid RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }

        public bool AsDelivery { get; set; } = default!;

        public Guid? UserLocationId { get; set; }
        public UserLocation? UserLocation { get; set; }
        
        [MinLength(1)] [MaxLength(64)] public string PaymentMethod { get; set; } = default!;

        [MinLength(1)] [MaxLength(128)] public string FirstName { get; set; } = default!;
        [MinLength(1)] [MaxLength(128)] public string LastName { get; set; } = default!;
        [MinLength(1)] [MaxLength(16)] public string Phone { get; set; } = default!;

        public ICollection<CartMeal>? CartMeals { get; set; }
    }
}