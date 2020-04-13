﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 using DAL.Base;

 namespace Domain
{
    public class RestaurantFood : DomainEntity
    {
        public Guid? MealId { get; set; }
        public Meal? Meal { get; set; }
        
        public Guid? PizzaId { get; set; }
        public Pizza? Pizza { get; set; }

        public Guid RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }

        [MaxLength(64, ErrorMessage = "Too long!")]
        public string Name { get; set; } = default!;
        
        [Range(1, 100), DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Tax { get; set; } = default!;

        [Range(1, 100), DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Gross { get; set; } = default!;

        public DateTime Since { get; set; } = default!;
        
        public DateTime Until { get; set; } = default!;
    }
}