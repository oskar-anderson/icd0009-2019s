using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain;

namespace PublicApi.DTO.v1
{
    public class InvoiceLineDTO
    {
        public Guid Id { get; set; }
        
        public Guid CartId { get; set; } = default!;
        public CartDTO? Cart { get; set; }

        public Guid InvoiceId { get; set; } = default!;
        public InvoiceDTO? Invoice { get; set; }

        [MaxLength(32)] [MinLength(1)] public string Name { get; set; } = default!;

        public int Quantity { get; set; } = default!;

        [Column(TypeName = "decimal(18,4)")] public decimal Net { get; set; } = default!;

        [Column(TypeName = "decimal(18,4)")] public decimal Tax { get; set; } = default!;

        [Column(TypeName = "decimal(18,4)")] public decimal Gross { get; set; } = default!;

    }
}