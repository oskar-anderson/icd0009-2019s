using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class MealComponent
    {
        [Required] public int MealComponentId { get; set; }
        
        [Required] public int ComponentId { get; set; }
        public virtual Component Component { get; set; }

        [Required] public int MealId { get; set; }
        public virtual Meal Meal { get; set; }
        
        [Required]
        [Range(1, 4, ErrorMessage = "Please enter an amount between 1 and 4")]
        public int Amount { get; set; }
    }
}