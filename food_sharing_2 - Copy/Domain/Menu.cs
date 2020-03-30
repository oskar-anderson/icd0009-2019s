﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
 using DAL.Base;

 namespace Domain
{
    public class Menu : DomainEntity
    {
        [MinLength(1)]
        [MaxLength(64, ErrorMessage = "Too long!")]
        public string Name { get; set; } = default!;

        public virtual ICollection<Restaurant>? Restaurants { get; set; }
        public virtual ICollection<MenuMeal>? MenuMeals { get; set; }
        
        
        /*
        
        Tiina Riina menüü nr 1
        
        */
    }
}