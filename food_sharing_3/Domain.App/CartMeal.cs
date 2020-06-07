using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.kaande.pitsariina.Domain.Base;

namespace Domain.App
{
    public class CartMeal : DomainEntityIdMetadata
    {
        public Guid CartId { get; set; } = default!;
        public Cart? Cart { get; set; }

        public Guid PizzaId { get; set; } = default!;
        public Pizza? Pizza { get; set; }
        
        
        [MinLength(1)] [MaxLength(128)] public string Name { get; set; } = default!;
        
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PizzaGross { get; set; } = default!;
        
        [MaxLength(256)] public string? Changes { get; set; }
        
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ComponentsGross { get; set; }
        
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalGross { get; set; } = default!;
    }
}