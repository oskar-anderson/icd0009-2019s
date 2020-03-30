﻿using System;
 using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 using DAL.Base;

 namespace Domain
{
    public class InvoiceLine : DomainEntity
    {
        public string CartId { get; set; } = default!;
        public Cart? Cart { get; set; }

        public string InvoiceId { get; set; } = default!;
        public Invoice? Invoice { get; set; }

        [MaxLength(32)] [MinLength(1)] public string Name { get; set; } = default!;

        public int Quantity { get; set; } = default!;

        [Column(TypeName = "decimal(18,4)")]
        public decimal Net { get; set; } = default!;

        [Column(TypeName = "decimal(18,4)")] public decimal Tax { get; set; } = default!;

        [Column(TypeName = "decimal(18,4)")] public decimal Gross { get; set; } = default!;
        
        public virtual ICollection<Item>? Items { get; set; }
    }
}