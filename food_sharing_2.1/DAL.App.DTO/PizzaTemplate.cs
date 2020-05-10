using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class PizzaTemplate : IDomainBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        
        public Guid CategoryId { get; set; } = default!;
        public Category? Category { get; set; }

        public string Name { get; set; } = default!;
        
        public string? Picture { get; set; }

        public int Modifications { get; set; } = default!;

        public int Extras { get; set; } = default!;

        public string? Description { get; set; }

        
        public ICollection<PizzaComponent>? PizzaComponents { get; set; }

    }
}