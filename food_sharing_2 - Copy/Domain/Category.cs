﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
 using DAL.Base;

 namespace Domain
{
    public class Category : DomainEntity
    {
        [MaxLength(64)] [MinLength(1)] public string Name { get; set; } = default!;

        public virtual ICollection<Meal>? Meals { get; set; }
        
        /*
        Fish
        Meat
        Vegan
        Spicy
        Lego
        
        */
    }
}