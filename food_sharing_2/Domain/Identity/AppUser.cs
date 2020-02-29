﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppUser : IdentityUser
    {
        // fix the pk lenght
        [MaxLength(36)] public override string Id { get; set; } = default!;
        
        // add your own fields
        [MinLength(1)] [MaxLength(128)] public string FirstName { get; set; } = default!;

        [MinLength(1)] [MaxLength(128)] public string LastName { get; set; } = default!;

        public virtual ICollection<Friend>? Friends { get; set; }
        public virtual ICollection<Sharing>? Sharings { get; set; }
        public virtual ICollection<UserLocation>? UserLocations { get; set; }
        public virtual ICollection<Cart>? Carts { get; set; }
        public virtual ICollection<UserClientGroup>? UserClientGroups { get; set; }
        public virtual ICollection<Person>? Persons { get; set; }


    }
}