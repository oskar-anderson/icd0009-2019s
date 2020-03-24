﻿﻿using System;
 using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;
 using DAL.Base;
 using Domain.Identity;

 namespace Domain
{
    public class Person : DomainEntity
    {
        [MaxLength(32)] public string AppUserId { get; set; }
        public virtual AppUser? User { get; set; }
        
        public bool ThisIsMe { get; set; } = default!;
        
        [MaxLength(128)] [MinLength(1)] public string FirstName { get; set; } = default!;

        [MinLength(1)] [MaxLength(128)] public string LastName { get; set; } = default!;
        
        [MaxLength(32)] public string? NationalIdentificationNumber { get; set; }
        
        [Display(Name = "Day of birth")]
        public DateTime? Since { get; set; }
        
        [Display(Name = "Day of death")]
        public DateTime? Until { get; set; }

        [MinLength(1)] [MaxLength(16)] public string Phone { get; set; } = default!;

        public virtual ICollection<Invoice>? Invoices { get; set; }
    }
}