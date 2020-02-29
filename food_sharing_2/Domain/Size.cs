﻿using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
 using DAL.Base;

 namespace Domain
{
    public class Size : DomainEntityMetadata
    {
        [MaxLength(32)] [MinLength(1)] public string Name { get; set; } = default!;

        public virtual ICollection<Meal>? Meals { get; set; } 

        /*
        
        Väike
        Suur
        
        */
    }
}