﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Item
    {
        [Required] public int ItemId { get; set; }
        
        [Required] public int SharingId { get; set; }
        public virtual Sharing Sharing { get; set; } 
        
        [Required] public int InvoiceLineId { get; set; }
        public virtual InvoiceLine InvoiceLine { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
        
        [Column(TypeName = "decimal(18,4)")]
        [Required]
        public float Gross { get; set; }
        
        
    }
}