﻿using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Pizza : Contracts.Domain.IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid PizzaTemplateId { get; set; } = default!;
        public PizzaTemplate? PizzaTemplate { get; set; }
        
        public Guid SizeId { get; set; } = default!;
        public Size? Size { get; set; }

        public string Name { get; set; } = default!;
        
        public ICollection<PizzaFinal>? PizzaFinals { get; set; }
        public ICollection<RestaurantFood>? RestaurantFoods { get; set; }

    }
}