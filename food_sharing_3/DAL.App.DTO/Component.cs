using System;
using System.Collections.Generic;
using ee.itcollege.kaande.pitsariina.Contracts.Domain;

namespace DAL.App.DTO
{

    public class Component : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; } = default!;
        
        public decimal Gross { get; set; } = default!;

        public ICollection<ComponentPizzaTemplate>? ComponentPizzaTemplate { get; set; }
    }
}