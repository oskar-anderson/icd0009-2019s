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
        
        public ICollection<PizzaTemplate>? PizzaTemplates { get; set; }

    }
}