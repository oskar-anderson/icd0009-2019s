﻿using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
 using DAL.Base;

 namespace Domain
{
    public class Size : DomainEntity
    {
        [MaxLength(32)] [MinLength(1)] public string Name { get; set; } = default!;

        public ICollection<Pizza>? Pizzas { get; set; } 

        /*
        
        Väike
        Suur
        
        */
    }
}