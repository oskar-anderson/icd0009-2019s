﻿using System;
 using System.Buffers.Text;
 using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 using System.Linq;
 using Domain.Base;

 namespace Domain
{
    public class Meal : DomainEntityIdMetadata
    {
        public Guid CategoryId { get; set; } = default!;
        public Category? Category { get; set; }

        [MaxLength(128)] public string Name { get; set; } = default!;
        
        [MaxLength(128)] public string? Picture { get; set; }

        [MaxLength(256)] [MinLength(4)] public string? Description { get; set; }

        
        public ICollection<RestaurantFood>? RestaurantFoods { get; set; }
        public ICollection<CartMeal>? CartMeals { get; set; }
        
        /*
        public IList<Cart> GetCarts(ICollection<CartMeal> cartMeals)
        {
            return cartMeals.Select(m => m.Cart).ToList();
        }
        */
        
        
        /*

        Caesari Salat
        7.8€
        
        Coca-cola 0,5L
        1,5€
        
        Sprite 1L
        2,5€
        
        Vesi 0,5L
        1€
        
        */
    }
}