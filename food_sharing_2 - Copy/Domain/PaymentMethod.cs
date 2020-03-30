﻿using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
 using DAL.Base;

 namespace Domain
{
    public class PaymentMethod : DomainEntity
    {
        [MaxLength(64)] [MinLength(1)] public string Name { get; set; } = default!;
        
        public ICollection<Invoice>? Invoices { get; set; } 
        
        /*
        
        Swedbank
        Coop
        SEB
        LHV
        Luminor
        
        */
    }
}