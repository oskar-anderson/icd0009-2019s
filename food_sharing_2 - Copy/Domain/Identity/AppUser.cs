﻿﻿using System;
 using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        
        // add your own fields
        [MinLength(1)] [MaxLength(128)] public string FirstName { get; set; } = default!;

        [MinLength(1)] [MaxLength(128)] public string LastName { get; set; } = default!;

        [MinLength(1)] [MaxLength(16)] public string Phone { get; set; } = default!;
        
        public virtual ICollection<Friend>? Friends { get; set; }
        public virtual ICollection<Sharing>? Sharings { get; set; }
        public virtual ICollection<UserLocation>? UserLocations { get; set; }
        public virtual ICollection<Cart>? Carts { get; set; }
        public virtual ICollection<UserClientGroup>? UserClientGroups { get; set; }
        public virtual ICollection<Person>? Persons { get; set; }


    }
}