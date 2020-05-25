using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Cart : DomainEntityIdMetadata
    {
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }

        public int State { get; set; } = default!;

        public Guid RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }

        public bool AsDelivery { get; set; }

        public Guid? UserLocationId { get; set; }
        public UserLocation? UserLocation { get; set; }
        
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")] 
        public decimal? Gross { get; set; }

        [MinLength(1)] [MaxLength(64)] public string? PaymentMethod { get; set; }

        [MinLength(1)] [MaxLength(128)] public string? FirstName { get; set; }
        [MinLength(1)] [MaxLength(128)] public string? LastName { get; set; }
        [MinLength(1)] [MaxLength(16)] public string? Phone { get; set; }

        
        [Display(Name = "Ready by")]
        [DataType(DataType.Time)]
        public DateTime? ReadyBy { get; set; }
        
        public ICollection<CartMeal>? CartMeals { get; set; }
    }
}