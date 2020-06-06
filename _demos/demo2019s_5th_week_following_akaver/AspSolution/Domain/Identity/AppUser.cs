using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppUser : IdentityUser
    {
        // fix the pk lenght
        [MaxLength(36)]
        public override string Id { get; set; }
        
        // add your own fields
        [MaxLength(128)] [MinLength(1)] public string FirstName { get; set; } = default!;
        
        [MaxLength(128)] [MinLength(1)] public string LastName { get; set; } = default!;

        public ICollection<Author>? Authors { get; set; }
    }
}