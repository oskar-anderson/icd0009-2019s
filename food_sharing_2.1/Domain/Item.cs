﻿using System;
 using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 using DAL.Base;

 namespace Domain
{
    public class Item : DomainBaseMetadata
    {
        public Guid SharingId { get; set; } = default!;
        public Sharing? Sharing { get; set; }

        public Guid InvoiceLineId { get; set; } = default!;
        public InvoiceLine? InvoiceLine { get; set; }

        [MaxLength(64)] [MinLength(1)] public string Name { get; set; } = default!;

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Net { get; set; } = default!;
        
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")] 
        public decimal Tax { get; set; } = default!;
        
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Gross { get; set; } = default!;

        public ICollection<SharingItem>? SharingItems { get; set; }
    }
}