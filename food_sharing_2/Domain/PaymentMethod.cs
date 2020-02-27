﻿using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class PaymentMethod
    {
        [Required] public int PaymentMethodId { get; set; }
        
        [Required] 
        [MaxLength(64)]
        public string Name { get; set; }

        /*
        
        Swedbank
        Coop
        SEB
        LHV
        Luminor
        
        */
    }
}