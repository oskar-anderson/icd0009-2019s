using System;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class ComponentPrice : IDomainBaseEntity<Guid>
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