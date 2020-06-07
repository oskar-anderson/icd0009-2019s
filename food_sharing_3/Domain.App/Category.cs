using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.kaande.pitsariina.Domain.Base;

namespace Domain.App
{
    public class Category : DomainEntityIdMetadata
    {
        [MinLength(1)] [MaxLength(64)] public string Name { get; set; } = default!;

        public ICollection<PizzaTemplate>? PizzaTemplates { get; set; }
        
    }
}