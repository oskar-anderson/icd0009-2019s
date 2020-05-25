using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class Category : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; } = default!;
        
        public bool ForMeal { get; set; } = default!;
        
        public bool ForPizzaTemplate { get; set; } = default!;
        

        public ICollection<Meal>? Meals { get; set; }
        public ICollection<PizzaTemplate>? PizzaTemplates { get; set; }

    }
}