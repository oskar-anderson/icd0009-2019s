﻿using System;
 using System.Buffers.Text;
 using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 using System.Linq;
 using DAL.Base;

 namespace Domain
{
    public class Pizza : DomainEntity
    {
        public Guid PizzaTemplateId { get; set; } = default!;
        public PizzaTemplate? PizzaTemplate { get; set; }
        
        public Guid SizeId { get; set; } = default!;
        public Size? Size { get; set; }

        [MinLength(4), MaxLength(128)] public string Name { get; set; } = default!;
        
        public ICollection<PizzaFinal>? PizzaFinals { get; set; }
        public ICollection<RestaurantFood>? RestaurantFoods { get; set; }
        
        /*
        Väike Americana
        5€
        juust, salaami, ananass, oliivid
        
        Väike Bolognese
        6€
        hakkliha, sibul, küüslauk, mozarella, juust  
        
        Väike Siciliana
        5€
        juust, sink, küüslauk, sibul
        
        Väike Topolino
        5.5€
        juust, sink, šampinjonid, ananass
        
        Väike Pepperoni
        5€
        juust, kaste, pepperoni, basiilik, mozzarella, oliivid
        
        Keskmine Americana
        7.7€
        juust, salaami, ananass, oliivid
        
        Keskmine Bolognese
        7.7€
        hakkliha, sibul, küüslauk, mozarella, juust  
        
        Keskmine Siciliana
        6.7€
        juust, sink, küüslauk, sibul
        
        Keskmine Topolino
        7€
        juust, sink, šampinjonid, ananass
        
        Keskmine Pepperoni
        6.7€
        juust, kaste, pepperoni, basiilik, mozzarella, oliivid
        
        Suur Americana
        8€
        juust, salaami, ananass, oliivid
        
        Suur Bolognese
        9€
        hakkliha, sibul, küüslauk, mozarella, juust  
        
        Suur Siciliana
        8€
        juust, sink, küüslauk, sibul
        
        Suur Topolino
        8.2€
        juust, sink, šampinjonid, ananass
        
        Suur Pepperoni
        8€
        juust, kaste, pepperoni, basiilik, mozzarella, oliivid
        
        */
    }
}