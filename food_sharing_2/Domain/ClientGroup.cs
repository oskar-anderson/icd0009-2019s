﻿using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
 using DAL.Base;

 namespace Domain
{
    public class ClientGroup : DomainEntityMetadata
    {
        [MinLength(1)] [MaxLength(64)] public string Name { get; set; } = default!;

        [MinLength(1)] [MaxLength(4024)] public string Description { get; set; } = default!;
        
        public virtual ICollection<UserClientGroup>? UserClientGroups { get; set; }
        
        public virtual ICollection<MealPrice>? MealPrices { get; set; }
    }
}