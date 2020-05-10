using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Item : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid SharingId { get; set; } = default!;
        public Sharing? Sharing { get; set; }

        public Guid InvoiceLineId { get; set; } = default!;
        public InvoiceLine? InvoiceLine { get; set; }
        
        public string Name { get; set; } = default!;
        
        public decimal Net { get; set; } = default!;
        
        public decimal Tax { get; set; } = default!;
        
        public decimal Gross { get; set; } = default!;

        public ICollection<SharingItem>? SharingItems { get; set; }

    }
}