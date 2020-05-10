using System;
using Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class RestaurantFood : IDomainBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        
        public Guid? MealId { get; set; }
        public Meal? Meal { get; set; }
        
        public Guid? PizzaId { get; set; }
        public Pizza? Pizza { get; set; }

        public Guid RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
        
        public string Name { get; set; } = default!;
        
        public decimal Tax { get; set; } = default!;
        
        public decimal Gross { get; set; } = default!;

        public DateTime Since { get; set; } = default!;
        
        public DateTime Until { get; set; } = default!;

    }
}