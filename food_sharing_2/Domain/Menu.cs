﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Menu
    {
        [Required] public int MenuId { get; set; }
        
        [Required]
        [MaxLength(64, ErrorMessage = "Too long!")]
        public string Name { get; set; }

        public virtual ICollection<Restaurant> Restaurants { get; set; }
        public virtual ICollection<Meal> Meals { get; set; }
        
        
        /*
        
        Tiina Riina menüü nr 1
        
        */
    }
}