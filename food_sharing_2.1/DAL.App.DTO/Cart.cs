﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class Cart : IDomainBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }

        public bool AsDelivery { get; set; } = default!;

        public Guid? UserLocationId { get; set; }
        public UserLocation? UserLocation { get; set; }

        public Guid RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
        
        public decimal Total { get; set; } = default!;
        
        public DateTime ReadyBy { get; set; } = default!;
        
        public ICollection<CartMeal>? CartMeals { get; set; }    // https://stackoverflow.com/questions/46349747/create-direct-navigation-property-in-ef-core-many-to-many-relationship

        // public IList<Meal> Meals => CartMeals.Select(m => m.Meal).ToList();
        
        public ICollection<InvoiceLine>? InvoiceLines { get; set; }

    }
}