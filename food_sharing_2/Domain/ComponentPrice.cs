﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class ComponentPrice
    {
        [Required] public int ComponentPriceId { get; set; }
        
        [Required] public int ComponentId { get; set; }
        public virtual Component Component { get; set; }
        
        [Required] public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        
        [Column(TypeName = "decimal(18,4)")]
        [Required] public decimal Gross { get; set; }
        
        [Column(TypeName = "decimal(18,4)")]
        [Required] public decimal Tax { get; set; }
        
        [Required]
        public DateTime Since { get; set; }
        
        [Required]
        public DateTime Until { get; set; }
    }
}