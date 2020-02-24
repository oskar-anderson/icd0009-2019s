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
        
        public int? PersonId { get; set; }
        public Person Person { get; set; }

        [Required]
        [MaxLength(128)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(128)]
        public string LastName { get; set; }
        
        [Required]
        [MaxLength(128)]
        public string Email { get; set; }

        [Required]
        [MaxLength(128)]
        public string Password { get; set; }
        
        
        public virtual ICollection<Friend> Friends { get; set; }
        public virtual ICollection<Sharing> Sharings { get; set; }
        public virtual ICollection<UserLocation> UserLocations { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        
    }
}