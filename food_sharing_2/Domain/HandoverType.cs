﻿using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class HandoverType
    {
        [Required] public int HandoverTypeId { get; set; }
        
        [Required] 
        [MaxLength(64)]
        public string Name { get; set; }
    }
}