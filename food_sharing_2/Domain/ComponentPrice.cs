﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 using DAL.Base;

 namespace Domain
{
    public class ComponentPrice : DomainEntityMetadata
    {
        [MaxLength(32)] public string ComponentId { get; set; } = default!;
        public virtual Component? Component { get; set; }

        [MaxLength(32)] public string RestaurantId { get; set; } = default!;
        public virtual Restaurant? Restaurant { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Gross { get; set; } = default!;

        [Column(TypeName = "decimal(18,4)")]
        public decimal Tax { get; set; } = default!;

        public DateTime Since { get; set; } = default!;

        public DateTime Until { get; set; } = default!;
    }
}