﻿using System;
 using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 using Domain.Base;

 namespace Domain
{
    public class PizzaComponent : DomainEntityIdMetadata
    {
        public Guid ComponentId { get; set; } = default!;
        public Component? Component { get; set; }

        public Guid? PizzaFinalId { get; set; }
        public PizzaFinal? PizzaFinal { get; set; }
        
        public Guid? PizzaTemplateId { get; set; }
        public PizzaTemplate? PizzaTemplate { get; set; }

        [Range(1, 4, ErrorMessage = "Please enter an amount between 1 and 4")]
        public int Amount { get; set; } = default!;
    }
}