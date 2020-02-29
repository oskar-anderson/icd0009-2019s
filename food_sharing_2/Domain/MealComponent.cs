﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 using DAL.Base;

 namespace Domain
{
    public class MealComponent : DomainEntityMetadata
    {
        [MaxLength(32)] public string ComponentId { get; set; } = default!;
        public virtual Component? Component { get; set; }

        [MaxLength(32)] public string MealId { get; set; } = default!;
        public virtual Meal? Meal { get; set; }

        [Range(1, 4, ErrorMessage = "Please enter an amount between 1 and 4")]
        public int Amount { get; set; } = default!;
    }
}