using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.kaande.pitsariina.Domain.Base;

namespace Domain.App
{
    public class RestaurantFood : DomainEntityIdMetadata
    {
        public Guid PizzaId { get; set; } = default!;
        public Pizza? Pizza { get; set; }

        public Guid RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }

        [Range(1, 100), DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Gross { get; set; } = default!;
    }
}