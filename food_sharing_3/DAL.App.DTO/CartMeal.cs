using System;
using Contracts.DAL.Base;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class CartMeal : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid CartId { get; set; } = default!;
        public Cart? Cart { get; set; }

        public Guid? PizzaId { get; set; }
        public Pizza? Pizza { get; set; }
        
        public Guid? PizzaUserId { get; set; }
        public PizzaUser? PizzaUser { get; set; }
        
        public string Name { get; set; } = default!;
        
        public decimal Gross { get; set; } = default!;
    }
}