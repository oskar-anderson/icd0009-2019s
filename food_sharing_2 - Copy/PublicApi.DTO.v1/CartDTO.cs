using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublicApi.DTO.v1
{
    public class CartDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(36)] public string AppUserId { get; set; } = default!;
        [MaxLength(36)] public string HandoverTypeId { get; set; } = default!;
        [MaxLength(36)] public string UserLocationId { get; set; } = default!;
        [MaxLength(36)] public string RestaurantId { get; set; } = default!;
        [Column(TypeName = "decimal(18,4)")] public decimal Total { get; set; } = default!;

        [Display(Name = "Ready by")]
        [DataType(DataType.Time)]
        public DateTime ReadyBy { get; set; } = default!;
        
        public virtual ICollection<CartMeal>? CartMeals { get; set; }    // https://stackoverflow.com/questions/46349747/create-direct-navigation-property-in-ef-core-many-to-many-relationship

        public IList<Meal> GetMeals(ICollection<CartMeal> cartMeals)
        {
            return cartMeals.Select(m => m.Meal).ToList();
        }
        
        public IList<Meal> Meals => CartMeals.Select(m => m.Meal).ToList();
        
        public ICollection<InvoiceLine>? InvoiceLines { get; set; }

    }
}