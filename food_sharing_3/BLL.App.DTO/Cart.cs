﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
 using BLL.App.DTO.Identity;
using ee.itcollege.kaande.pitsariina.Contracts.Domain;

namespace BLL.App.DTO
{
    public class Cart : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }

        public Guid RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }

        public bool AsDelivery { get; set; } = default!;

        public Guid? UserLocationId { get; set; }
        public UserLocation? UserLocation { get; set; }

        public string PaymentMethod { get; set; } = default!;

        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Phone { get; set; } = default!;

        public ICollection<CartMeal>? CartMeals { get; set; }
    }
}