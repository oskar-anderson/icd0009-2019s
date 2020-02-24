using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class CartMeal
    {
        [Required] public int CartMealId { get; set; }
        
        [Required]
        [ForeignKey(nameof(Domain.Cart))] 
        public int CartId { get; set; }
        public virtual Cart Cart { get; set; }

        [Required]
        [ForeignKey(nameof(Domain.Meal))] 
        public int MealId { get; set; }
        public virtual Meal Meal { get; set; }
    }
}