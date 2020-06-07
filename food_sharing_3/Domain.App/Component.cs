using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.kaande.pitsariina.Domain.Base;

namespace Domain.App
{
    public class Component : DomainEntityIdMetadata
    {
        [MaxLength(32)] public string Name { get; set; } = default!;

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Gross { get; set; } = default!;
        
        public ICollection<ComponentPizzaTemplate>? ComponentPizzaTemplate { get; set; }

    }
}