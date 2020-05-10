﻿using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
 using Domain.Base;

 namespace Domain
{
    public class Size : DomainEntityIdMetadata
    {
        [MaxLength(32)] [MinLength(1)] public string Name { get; set; } = default!;

        public ICollection<Pizza>? Pizzas { get; set; } 

        /*
        
        Väike
        Suur
        
        */
    }
}