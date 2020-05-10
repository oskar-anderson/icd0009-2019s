using System;
using System.Collections.Generic;

namespace DAL.App.DTO.Identity
{
    public class AppUser : AppUser<Guid>
    {
        
    }
    
    public class AppUser<TKey>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Phone { get; set; } = default!;
        
        // public virtual ICollection<Sharing>? Sharings { get; set; }
        // public virtual ICollection<UserLocation>? UserLocations { get; set; }
        // public virtual ICollection<Cart>? Carts { get; set; }
        // public virtual ICollection<Person>? Persons { get; set; }

    }
}