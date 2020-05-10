﻿using System;
 using System.Buffers.Text;
 using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 using System.Linq;
 using DAL.Base;

 namespace Domain
{
    public class PizzaTemplate : DomainBaseMetadata
    {
        public Guid CategoryId { get; set; } = default!;
        public Category? Category { get; set; }

        [MaxLength(128)] public string Name { get; set; } = default!;
        
        [MaxLength(128)] public string? Picture { get; set; }

        [Range(0, 6)] public int Modifications { get; set; } = default!;

        [Range(0, 8)] public int Extras { get; set; } = default!;

        [MaxLength(128)] [MinLength(4)] public string? Description { get; set; }

        
        public ICollection<PizzaComponent>? PizzaComponents { get; set; }



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
        
        */
    }
}