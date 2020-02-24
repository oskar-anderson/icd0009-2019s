using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Domain
{
    public class User
    {
        [Required] public int UserId { get; set; }
        
        [Required]
        [MaxLength(128)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(128)]
        public string LastName { get; set; }
        
        [Required]
        [MaxLength(128)]
        public string Email { get; set; }

        public virtual ICollection<UserFriend> Friends { get; set; }
        public virtual ICollection<Sharing> GroupDistributionManager { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<UserFav> UserFavourites { get; set; }    // https://stackoverflow.com/questions/46349747/create-direct-navigation-property-in-ef-core-many-to-many-relationship
        [NotMapped]
        public IList<Meal> UserFavouriteMeals => UserFavourites.Select(m => m.Meal).ToList();
    }
}