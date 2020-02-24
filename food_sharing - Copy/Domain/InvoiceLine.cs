using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InvoiceLine
    {
        [Required] public int InvoiceLineId { get; set; }

        [Required] public int CartId { get; set; }
        public Cart Cart { get; set; }

        [Required] public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        
        [Required]
        [MaxLength(32)]
        public string Name { get; set; }

        [Required]
        public int Quantity { get; set; } 
        
        [Required] public decimal Net { get; set; }
        
        [Required] public decimal Tax { get; set; }
        
        [Required] public decimal Gross { get; set; }
    }
}