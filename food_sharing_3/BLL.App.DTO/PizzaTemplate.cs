using System;
using System.Collections.Generic;
using ee.itcollege.kaande.pitsariina.Contracts.Domain;

namespace BLL.App.DTO
{
    public class PizzaTemplate : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid CategoryId { get; set; } = default!;
        public Category? Category { get; set; }

        public string Name { get; set; } = default!;
        
        public string? Picture { get; set; }

        public int? Modifications { get; set; }

        public int? Extras { get; set; }

        public string? Description { get; set; }

        
        public int VarietyState { get; set; } = default!;

        public ICollection<ComponentPizzaTemplate>? ComponentPizzaTemplates { get; set; }
        public ICollection<Pizza>? Pizzas { get; set; }

    }
}