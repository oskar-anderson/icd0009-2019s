using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class Size : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; } = default!;

        public ICollection<Pizza>? Pizzas { get; set; }

    }
}