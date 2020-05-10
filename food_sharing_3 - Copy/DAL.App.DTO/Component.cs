using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using Contracts.Domain;

namespace DAL.App.DTO
{

    public class Component : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; } = default!;

        public int Max { get; set; } = default!;

        public ICollection<ComponentPrice>? ComponentPrices { get; set; }
        public ICollection<PizzaComponent>? PizzaComponents { get; set; }

    }
}