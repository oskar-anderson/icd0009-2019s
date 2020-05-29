﻿using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Category : Contracts.Domain.IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; } = default!;
        
        public ICollection<PizzaTemplate>? PizzaTemplates { get; set; }

    }
}