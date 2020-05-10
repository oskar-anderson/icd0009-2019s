using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublicApi.DTO.v1
{
    public class InvoiceDTO
    {
        public Guid Id { get; set; }
        
        public Guid PersonId { get; set; } = default!;
        public PersonDTO? Person { get; set; }

        public Guid RestaurantId { get; set; } = default!;
        public RestaurantDTO? Restaurant { get; set; }

        public Guid PaymentMethodId { get; set; } = default!;
        public PaymentMethodDTO? PaymentMethod { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalNet { get; set; } = default!;

        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalTax { get; set; } = default!;
        
        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalGross { get; set; } = default!;

    }
}