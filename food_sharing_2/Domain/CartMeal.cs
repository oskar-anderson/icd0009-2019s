﻿using System;
 using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 using DAL.Base;

 namespace Domain
{
    public class CartMeal : DomainEntity
    {
        public Guid CartId { get; set; } = default!;
        public Cart Cart { get; set; }

        public Guid? MealId { get; set; }
        public Meal Meal { get; set; }
        
        public Guid? PizzaFinalId { get; set; }
        public PizzaFinal PizzaFinal { get; set; }
    }
}