using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class Category : IDomainBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; } = default!;

        public ICollection<Meal>? Meals { get; set; }
        public ICollection<PizzaTemplate>? PizzaTemplates { get; set; }

    }
}