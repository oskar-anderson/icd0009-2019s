﻿using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Size
    {
        [Required] public int SizeId { get; set; }
        
        [MaxLength(32)]
        [Required]
        public string Name { get; set; }
        
        
        /*
        
        Väike
        Suur
        
        */
    }
}