﻿using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 using DAL.Base;

 namespace Domain
{
    public class Item : DomainEntityMetadata
    {
        [MaxLength(32)] public string SharingId { get; set; } = default!;
        public virtual Sharing? Sharing { get; set; }

        [MaxLength(32)] public string InvoiceLineId { get; set; } = default!;
        public virtual InvoiceLine? InvoiceLine { get; set; }

        [MaxLength(64)] [MinLength(1)] public string Name { get; set; } = default!;

        [Column(TypeName = "decimal(18,4)")] public decimal Gross { get; set; } = default!;

        public virtual ICollection<SharingItem>? SharingItems { get; set; }
    }
}