using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class Cart : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        
        public int State { get; set; } = default!;

        public Guid RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }

        public bool AsDelivery { get; set; } = default!;

        public Guid? UserLocationId { get; set; }
        public UserLocation? UserLocation { get; set; }

        
        public decimal? Gross { get; set; }
        
        public string? PaymentMethod { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        
        public DateTime? ReadyBy { get; set; }
        
        public ICollection<CartMeal>? CartMeals { get; set; }
    }
}