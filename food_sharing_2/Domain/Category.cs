﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Category
    {
        [Required] public int CategoryId { get; set; }
        
        public virtual ICollection<Meal> Meals { get; set; }
        
        [Required] 
        [MaxLength(64)]
        public string Name { get; set; }
        
        /*
        Fish
        Meat
        Vegan
        Spicy
        Lego
        
        */
    }
}