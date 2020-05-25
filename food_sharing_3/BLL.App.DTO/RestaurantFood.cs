using System;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class RestaurantFood : Contracts.Domain.IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid? MealId { get; set; }
        public Meal? Meal { get; set; }
        
        public Guid? PizzaId { get; set; }
        public Pizza? Pizza { get; set; }

        public Guid RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }

        public decimal Gross { get; set; } = default!;

        public DateTime Since { get; set; } = default!;
        
        public DateTime Until { get; set; } = default!;

    }
}