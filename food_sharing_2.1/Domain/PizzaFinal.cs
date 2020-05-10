﻿using System;
 using System.Buffers.Text;
 using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 using System.Linq;
 using DAL.Base;

 namespace Domain
{
    public class PizzaFinal : DomainBaseMetadata
    {
        public Guid PizzaId { get; set; } = default!;
        public Pizza? Pizza { get; set; }
        
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Tax { get; set; } = default!;

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Gross { get; set; } = default!;

        [MaxLength(256)] public string Changes { get; set; } = default!;

        [NotMapped] public Dictionary<string, int> DifferenceWithTemplate = new Dictionary<string, int>();
        
        public ICollection<PizzaComponent>? PizzaComponents { get; set; }
        public ICollection<CartMeal>? CartMeals { get; set; }
    }
}