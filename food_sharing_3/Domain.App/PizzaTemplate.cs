using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.kaande.pitsariina.Domain.Base;

namespace Domain.App
{
    public class PizzaTemplate : DomainEntityIdMetadata
    {
        public Guid CategoryId { get; set; } = default!;
        public Category? Category { get; set; }

        [MaxLength(128)] public string Name { get; set; } = default!;
        
        [MaxLength(128)] public string? Picture { get; set; }

        [Range(0, 6)] public int? Modifications { get; set; }

        [Range(0, 8)] public int? Extras { get; set; }

        [MaxLength(128)] public string? Description { get; set; }
        
        [Range(1, 3)] public int VarietyState { get; set; } = default!;

        
        public ICollection<ComponentPizzaTemplate>? ComponentPizzaTemplates { get; set; }
        public ICollection<Pizza>? Pizzas { get; set; }
    }
}