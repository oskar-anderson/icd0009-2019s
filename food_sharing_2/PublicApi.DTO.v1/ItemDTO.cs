using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain;

namespace PublicApi.DTO.v1
{
    public class ItemDTO
    {
        public Guid Id { get; set; }
        
        public Guid SharingId { get; set; } = default!;
        public SharingDTO? Sharing { get; set; }

        public Guid InvoiceLineId { get; set; } = default!;
        public InvoiceLineDTO? InvoiceLine { get; set; }

        [MaxLength(64)] [MinLength(1)] public string Name { get; set; } = default!;

        [Column(TypeName = "decimal(18,4)")] public decimal Net { get; set; } = default!;
        [Column(TypeName = "decimal(18,4)")] public decimal Tax { get; set; } = default!;
        [Column(TypeName = "decimal(18,4)")] public decimal Gross { get; set; } = default!;

    }
}