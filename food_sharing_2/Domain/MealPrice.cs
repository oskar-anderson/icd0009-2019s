﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 using DAL.Base;

 namespace Domain
{
    public class MealPrice : DomainEntityMetadata
    {
        [MaxLength(32)] public string MealId { get; set; } = default!;
        public virtual Meal? Meal { get; set; }

        [MaxLength(32)] public string RestaurantId { get; set; } = default!;
        public virtual Restaurant? Restaurant { get; set; }
        
        [MaxLength(32)] public string? ClientGroupId { get; set; }
        public virtual ClientGroup? ClientGroup { get; set; }

        [MaxLength(64, ErrorMessage = "Too long!")]
        public string Name { get; set; } = default!;

        [Column(TypeName = "decimal(18,4)")]
        public decimal Tax { get; set; } = default!;

        [Column(TypeName = "decimal(18,4)")]
        [Required]
        public decimal Gross { get; set; } = default!;

        public DateTime Since { get; set; } = default!;
        
        public DateTime Until { get; set; } = default!;
    }
}