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
        [RegularExpression("([1-9][0-9]{0,1})", ErrorMessage = "Please enter valid Number")]
        public int Amount { get; set; }
    }
}