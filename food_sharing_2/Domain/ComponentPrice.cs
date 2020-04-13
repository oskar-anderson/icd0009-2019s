﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 using DAL.Base;

 namespace Domain
{
    public class ComponentPrice : DomainEntity
    {
        public Guid ComponentId { get; set; } = default!;
        public Component? Component { get; set; }

        public Guid RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
        
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Gross { get; set; } = default!;

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Tax { get; set; } = default!;

        public DateTime Since { get; set; } = default!;

        public DateTime Until { get; set; } = default!;
    }
}