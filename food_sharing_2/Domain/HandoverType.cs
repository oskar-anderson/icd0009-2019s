﻿using System.Collections;
 using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
 using DAL.Base;

 namespace Domain
{
    public class HandoverType : DomainEntityMetadata
    {
        [MaxLength(64)] [MinLength(1)] public string Name { get; set; } = default!;
        
        public virtual ICollection<Cart>? Carts { get; set; }
    }
}