﻿using System.ComponentModel.DataAnnotations;
 using DAL.Base;

 namespace Domain
{
    public class MenuMeal : DomainEntityMetadata
    {
        [MaxLength(32)] public int MealId { get; set; } = default!; 
        public virtual Meal? Meal { get; set; }

        [MaxLength(32)] public int MenuId { get; set; } = default!;
        public virtual Menu? Menu { get; set; }
    }
}