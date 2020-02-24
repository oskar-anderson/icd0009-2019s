using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class MenuMeal
    {
        [Required] public int MenuMealId { get; set; }
        
        [Required] public int MealId { get; set; } 
        public virtual Meal Meal { get; set; }
        
        [Required] public int MenuId { get; set; }
        public virtual Menu Menu { get; set; }
    }
}