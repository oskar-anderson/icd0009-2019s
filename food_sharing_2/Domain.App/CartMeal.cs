﻿using System;
 using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 using Contracts.DAL.Base;
 using Domain.Base;
 using Domain.Identity;

 namespace Domain
{
    public class CartMeal : DomainEntityIdMetadata
    {
        public Guid CartId { get; set; } = default!;
        public Cart Cart { get; set; }

        public Guid? MealId { get; set; }
        public Meal Meal { get; set; }
        
        public Guid? PizzaFinalId { get; set; }
        public PizzaFinal PizzaFinal { get; set; }
    }
}