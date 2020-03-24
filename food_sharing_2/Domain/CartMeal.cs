﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 using DAL.Base;

 namespace Domain
{
    public class CartMeal : DomainEntity
    {
        [MaxLength(36)]
        public string CartId { get; set; } = default!;
        public virtual Cart? Cart { get; set; }

        [MaxLength(36)] public string MealId { get; set; } = default!;
        public virtual Meal? Meal { get; set; }
    }
}