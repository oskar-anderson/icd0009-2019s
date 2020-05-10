using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Category : DomainEntityIdMetadata
    {
        [MaxLength(64)] [MinLength(1)] public string Name { get; set; } = default!;

        public ICollection<Meal>? Meals { get; set; }
        public ICollection<PizzaTemplate>? PizzaTemplates { get; set; }
        
        /*
        Fish
        Meat
        Vegan
        Spicy
        Lego
        
        */
    }
}