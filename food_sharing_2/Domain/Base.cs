﻿using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
 using DAL.Base;

 namespace Domain
{
    public class Base : DomainEntityMetadata
    {
        [MinLength(1)]
        [MaxLength(32)]
        public string Name { get; set; } = default!;
        
        public ICollection<Meal>? Meals { get; set; } 
    }
}