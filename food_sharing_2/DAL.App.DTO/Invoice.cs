using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace Domain.Base.App.DTO
{
    public class Invoice : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid PersonId { get; set; } = default!;
        public Person? Person { get; set; }

        public Guid RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }

        public Guid PaymentMethodId { get; set; } = default!;
        public PaymentMethod? PaymentMethod { get; set; }
        
        public decimal TotalNet { get; set; } = default!;
        
        public decimal TotalTax { get; set; } = default!;
        
        public decimal TotalGross { get; set; } = default!;
        
        public ICollection<InvoiceLine>? InvoiceLines { get; set; }

    }
}