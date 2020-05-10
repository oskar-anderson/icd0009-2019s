﻿using System;
 using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 using Domain.Base;

 namespace Domain
{
    public class InvoiceLine : DomainEntityIdMetadata
    {
        public Guid CartId { get; set; } = default!;
        public Cart? Cart { get; set; }

        public Guid InvoiceId { get; set; } = default!;
        public Invoice? Invoice { get; set; }

        [MaxLength(32)] [MinLength(1)] public string Name { get; set; } = default!;

        public int Quantity { get; set; } = default!;

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Net { get; set; } = default!;

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Tax { get; set; } = default!;

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Gross { get; set; } = default!;
        
        public ICollection<Item>? Items { get; set; }
    }
}