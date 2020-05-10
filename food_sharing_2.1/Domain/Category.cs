﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
 using DAL.Base;

 namespace Domain
{
    public class Category : DomainBaseMetadata
    {
        [MaxLength(64)] [MinLength(1)] public string Name { get; set; } = default!;

        public ICollection<Meal>? Meals { get; set; }
        public ICollection<PizzaTemplate>? PizzaTemplates { get; set; }
        
        /*
        Fish
        Meat
        Vegan
        Spicy
        Lego
        
        */
    }
}