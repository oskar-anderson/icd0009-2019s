using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class PizzaTemplate : IDomainEntityId
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