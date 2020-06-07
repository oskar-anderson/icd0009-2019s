using System;
using System.Collections.Generic;
using ee.itcollege.kaande.pitsariina.Contracts.Domain;

namespace BLL.App.DTO
{
    public class Category : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; } = default!;
        
        public ICollection<PizzaTemplate>? PizzaTemplates { get; set; }

    }
}