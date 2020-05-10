using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using Domain.Base.App.DTO.Identity;

namespace Domain.Base.App.DTO
{
    public class Person : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        
        public bool ThisIsMe { get; set; } = default!;
        
        public string FirstName { get; set; } = default!;
        
        public string LastName { get; set; } = default!;
        
        public string? NationalIdentificationNumber { get; set; }
        
        public DateTime? Since { get; set; }
        
        public DateTime? Until { get; set; }

        public string Phone { get; set; } = default!;

        public ICollection<Invoice>? Invoices { get; set; }

    }
}