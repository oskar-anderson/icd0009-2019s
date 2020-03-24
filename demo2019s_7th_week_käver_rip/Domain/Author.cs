using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Author : DomainEntity
    {
        [MaxLength(128)] [MinLength(1)] public string FirstName { get; set; } = default!;

        [MaxLength(128)] [MinLength(1)] public string LastName { get; set; } = default!;


        [MaxLength(36)] public string AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
    }
}