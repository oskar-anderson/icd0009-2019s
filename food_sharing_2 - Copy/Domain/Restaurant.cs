﻿using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
 using DAL.Base;

 namespace Domain
{
    public class Restaurant : DomainEntity
    {
        public string ? MenuId { get; set; }
        public virtual Menu ? Menu { get; set; }


        [MinLength(1)] [MaxLength(64)] public string Name { get; set; } = default!;

        [MinLength(1)] [MaxLength(64)] public string Location { get; set; } = default!;

        [MinLength(1)] [MaxLength(64)] public string Telephone { get; set; } = default!;

        [MinLength(1)] [MaxLength(64)] public string OpenTime { get; set; } = default!;

        [MinLength(1)] [MaxLength(64)] public string OpenNotification { get; set; } = default!;

        public virtual ICollection<Cart>? Carts { get; set; } 
        public virtual ICollection<Invoice>? Invoices { get; set; } 
        public virtual ICollection<MealPrice>? MealPrices { get; set; } 
        public virtual ICollection<ComponentPrice>? ComponentPrices { get; set; } 
        
        /*
        Pitsa Riina
        Endla 76
        5724871
        E-P, 12:00-23:00
        10., 12. aprill, 1., 31. mai
        */
    }
}