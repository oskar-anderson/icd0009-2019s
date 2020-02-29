﻿using System.Buffers.Text;
 using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 using System.Linq;
 using DAL.Base;

 namespace Domain
{
    public class Meal : DomainEntityMetadata
    {
        [MaxLength(32)] public string CategoryId { get; set; } = default!;
        public Category? Category { get; set; }

        [MaxLength(32)] public string? SizeId { get; set; }
        public virtual Size? Size { get; set; }
        
        [MaxLength(32)] public string? BaseId { get; set; }
        public Base? Base { get; set; }

        [MaxLength(128)] public string Name { get; set; } = default!;
        
        [MaxLength(128)] public string? Picture { get; set; }

        public int Modifications { get; set; } = default!;

        public int Extras { get; set; } = default!;

        [MaxLength(128)] [MinLength(1)] public string Description { get; set; } = default!;

        
        public virtual ICollection<MealPrice>? MealPrices { get; set; }
        public virtual ICollection<MealComponent>? MealComponents { get; set; }
        public virtual ICollection<MenuMeal>? MenuMeals { get; set; }
        public IList<Menu> GetMenus(ICollection<MenuMeal> menuMeals)
        {
            return menuMeals.Select(m => m.Menu).ToList();
        }
        
        public virtual ICollection<CartMeal>? CartMeals { get; set; }
        public IList<Cart> GetCarts(ICollection<CartMeal> cartMeals)
        {
            return cartMeals.Select(m => m.Cart).ToList();
        }
        
        
        /*
        Americana
        5/8€
        juust, salaami, ananass, oliivid
        
        Bolognese
        6/9€
        hakkliha, sibul, küüslauk, mozarella, juust  
        
        Siciliana
        5/8€
        juust, sink, küüslauk, sibul
        
        Topolino
        5.5/8.2€
        juust, sink, šampinjonid, ananass
        
        Pepperoni
        5/8€
        juust, kaste, pepperoni, basiilik, mozzarella, oliivid
        
        Caesari Salat
        7.8€
        
        Coca-cola 0,5L
        1,5€
        
        Sprite 1L
        2,5€
        
        Vesi 0,5L
        1€
        
        */
    }
}