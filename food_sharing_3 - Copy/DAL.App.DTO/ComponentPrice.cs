using System;
using Contracts.DAL.Base;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class ComponentPrice : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid ComponentId { get; set; } = default!;
        public Component? Component { get; set; }

        public Guid RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
        
        public decimal Gross { get; set; } = default!;
        
        public decimal Tax { get; set; } = default!;

        public DateTime Since { get; set; } = default!;

        public DateTime Until { get; set; } = default!;
    }
}