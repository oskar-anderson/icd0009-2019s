using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{

    public class Component : Contracts.Domain.IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; } = default!;

        public int Max { get; set; } = default!;

        public ICollection<ComponentPrice>? ComponentPrices { get; set; }
        public ICollection<PizzaComponent>? PizzaComponents { get; set; }

    }
}