﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
 using System.Runtime.CompilerServices;
 using DAL.Base;
 using Domain.Identity;

 namespace Domain
{
    public class Cart : DomainEntity
    {
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }

        public bool AsDelivery { get; set; } = default!;

        public Guid? UserLocationId { get; set; }
        public UserLocation? UserLocation { get; set; }

        public Guid RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
        
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")] 
        public decimal Total { get; set; } = default!;

        [Display(Name = "Ready by")]
        [DataType(DataType.Time)]
        public DateTime ReadyBy { get; set; } = default!;
        
        public ICollection<CartMeal>? CartMeals { get; set; }    // https://stackoverflow.com/questions/46349747/create-direct-navigation-property-in-ef-core-many-to-many-relationship

        // public IList<Meal> Meals => CartMeals.Select(m => m.Meal).ToList();
        
        public ICollection<InvoiceLine>? InvoiceLines { get; set; }
        
    }
}