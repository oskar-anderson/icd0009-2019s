﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class MealPrice
    {
        [Required] public int MealPriceId { get; set; }
        
        [Required] public int MealId { get; set; }
        public virtual Meal Meal { get; set; }
        
        [Required] public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        
        public int? ClientGroupId { get; set; }
        public virtual ClientGroup? ClientGroup { get; set; }

        [Required]
        [MaxLength(64, ErrorMessage = "Too long!")]
        public string Name { get; set; }
        
        [Required] public decimal Tax { get; set; }
        
        [Required] public decimal Gross { get; set; }
        
        [Required]
        public DateTime Since { get; set; }
        
        [Required]
        public DateTime Until { get; set; }
    }
}