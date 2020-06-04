using System;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class CartMeal : Contracts.Domain.IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid CartId { get; set; } = default!;
        public Cart? Cart { get; set; }
        
        public Guid PizzaId { get; set; } = default!;
        public Pizza? Pizza { get; set; }
        
        public string Name { get; set; } = default!;
        
        public decimal PizzaGross { get; set; } = default!;
        
        public string? Changes { get; set; }
        
        public decimal? ComponentsGross { get; set; }
        
        public decimal TotalGross { get; set; } = default!;
    }
}