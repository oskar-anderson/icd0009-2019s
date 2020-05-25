using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Category : DomainEntityIdMetadata
    {
        [MinLength(1)] [MaxLength(64)] public string Name { get; set; } = default!;

        public bool ForMeal { get; set; } = default!;
        
        public bool ForPizzaTemplate { get; set; } = default!;
        
        public ICollection<Meal>? Meals { get; set; }
        public ICollection<PizzaTemplate>? PizzaTemplates { get; set; }
        
    }
}