using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class InvoiceLine : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid CartId { get; set; } = default!;
        public Cart? Cart { get; set; }

        public Guid InvoiceId { get; set; } = default!;
        public Invoice? Invoice { get; set; }

        public string Name { get; set; } = default!;

        public int Quantity { get; set; } = default!;
        
        public decimal Net { get; set; } = default!;
        
        public decimal Tax { get; set; } = default!;
        
        public decimal Gross { get; set; } = default!;
        
        public ICollection<Item>? Items { get; set; }
    }
}