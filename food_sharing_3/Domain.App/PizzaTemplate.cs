﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class PizzaTemplate : DomainEntityIdMetadata
    {
        public Guid CategoryId { get; set; } = default!;
        public Category? Category { get; set; }

        [MaxLength(128)] public string Name { get; set; } = default!;
        
        [MaxLength(128)] public string? Picture { get; set; }

        [Range(0, 6)] public int Modifications { get; set; } = default!;

        [Range(0, 8)] public int Extras { get; set; } = default!;

        [MaxLength(128)] [MinLength(4)] public string? Description { get; set; }

        
        public ICollection<ComponentPizzaTemplate>? ComponentPizzaTemplates { get; set; }
    }
}