using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublicApi.DTO.v1
{
    public class PizzaFinalDTO
    {
        public Guid Id { get; set; }
        
        public Guid PizzaId { get; set; } = default!;
        public PizzaDTO? Pizza { get; set; }
        
        [Column(TypeName = "decimal(18,4)")]
        public decimal Tax { get; set; } = default!;

        [Column(TypeName = "decimal(18,4)")]
        public decimal Gross { get; set; } = default!;

        [MaxLength(256)] public string Changes { get; set; } = default!;

        public Dictionary<string, int> DifferenceWithTemplate = new Dictionary<string, int>();

    }
}