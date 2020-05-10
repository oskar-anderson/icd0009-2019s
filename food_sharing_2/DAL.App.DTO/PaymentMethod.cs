using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace Domain.Base.App.DTO
{
    public class PaymentMethod : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; } = default!;
        
        public ICollection<Invoice>? Invoices { get; set; } 
        
        public DateTime Since { get; set; } = default!;
        public DateTime Until { get; set; } = default!;

    }
}