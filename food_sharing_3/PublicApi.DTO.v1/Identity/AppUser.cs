using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.kaande.pitsariina.Contracts.Domain;

namespace PublicApi.DTO.v1.Identity
{
    public class AppUser : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;
        
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Phone { get; set; } = default!;

    }
}