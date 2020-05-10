﻿using System;
 using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
 using Domain.Base;

 namespace Domain
{
    public class PaymentMethod : DomainEntityIdMetadata
    {
        [MaxLength(64)] [MinLength(1)] public string Name { get; set; } = default!;
        
        public ICollection<Invoice>? Invoices { get; set; } 
        
        public DateTime Since { get; set; } = default!;
        public DateTime Until { get; set; } = default!;
        
        /*
        
        Swedbank
        Coop
        SEB
        LHV
        Luminor
        
        */
    }
}