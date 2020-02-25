using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Domain
{
    public class Cart
    {
        [Required] public int CartId { get; set; }

        [Required] public int UserId { get; set; }
        public virtual User? User { get; set; }
        
        [Required] public int HandoverTypeId { get; set; }
        public virtual HandoverType HandoverType { get; set; }
        
        [Required] public int UserLocationId { get; set; } 
        public virtual UserLocation UserLocation { get; set; }
        
        [Required] public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        
        [Column(TypeName = "decimal(18,4)")]
        [Required] public decimal Total { get; set; }
        
        [Display(Name = "Ready by")]
        [DataType(DataType.Time)]
        [Required]
        public DateTime ReadyBy { get; set; }
        
        public virtual ICollection<CartMeal>? CartMeals { get; set; }    // https://stackoverflow.com/questions/46349747/create-direct-navigation-property-in-ef-core-many-to-many-relationship
        [NotMapped]
        public IList<Meal> Meals => CartMeals.Select(m => m.Meal).ToList();
        
    }
}