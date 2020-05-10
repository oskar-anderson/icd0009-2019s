using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class PizzaFinal : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid PizzaId { get; set; } = default!;
        public Pizza? Pizza { get; set; }
        
        public decimal Tax { get; set; } = default!;
        
        public decimal Gross { get; set; } = default!;
        
        public string Changes { get; set; } = default!;

        public Dictionary<string, int> DifferenceWithTemplate = new Dictionary<string, int>();
        
        public ICollection<PizzaComponent>? PizzaComponents { get; set; }
        public ICollection<CartMeal>? CartMeals { get; set; }

    }
}